using System.Collections.Generic;
using UnityEngine;

// State and puzzle logic are independent from the generated scene geometry.
public class GardenGame : MonoBehaviour
{
    public const string Author="Molka Boughanmi";
    public enum View { Menu, Help, Credits, Play, Puzzle, Pause, Win, Lose }
    public View State { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }
    public int Seeds { get; private set; }
    public int Steps { get; private set; }
    public int Score { get { return completedScore+stageScore; } }
    public int Stars { get { return mistakes==0&&!HintUsed?3:mistakes<=1?2:1; } }
    public int Unlocked { get { return Mathf.Clamp(PlayerPrefs.GetInt("EcoExpedition.Unlocked",0),0,2); } }
    public int Best { get { return PlayerPrefs.GetInt("EcoExpedition.Best."+Stage,0); } }
    public float Seconds { get; private set; }
    public bool HintOpen { get; private set; }
    public bool HintUsed { get; private set; }
    public bool JournalFound { get; private set; }
    public bool Sound=true;
    public string Message { get; private set; }
    public readonly bool[] Sources=new bool[3];
    public readonly int[] Valves=new int[3];
    public readonly List<int> Sequence=new List<int>();
    public bool NearConsole { get { return Mathf.Abs(cell.x-consoleCell.x)+Mathf.Abs(cell.y-consoleCell.y)<=1; } }

    readonly HashSet<Vector2Int> walkable=new HashSet<Vector2Int>();
    readonly HashSet<Vector2Int> hazards=new HashSet<Vector2Int>();
    readonly Dictionary<Vector2Int,GameObject> pickups=new Dictionary<Vector2Int,GameObject>();
    readonly List<Material> materials=new List<Material>();
    readonly List<Transform> foliage=new List<Transform>();
    Camera cam;
    Transform world, robot, flower;
    Vector2Int cell, spawn, consoleCell;
    GameObject notebook, beacon;
    Material pathMat, soilMat, rockMat, leafMat, mintMat, amberMat, redMat, whiteMat, darkMat;
    AudioSource speaker;
    AudioClip stepSound, seedSound, errorSound, winSound;
    int completedScore,stageScore,mistakes;
    View beforePause, beforeHelp;
    float winGrowth;
    Vector3 targetPosition;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Boot() { if(Object.FindObjectOfType<GardenGame>()==null) new GameObject("EcoLab Expedition").AddComponent<GardenGame>(); }
    void Awake()
    {
        Application.targetFrameRate=60;
        pathMat=Mat(new Color(.51f,.65f,.51f)); soilMat=Mat(new Color(.29f,.24f,.20f)); rockMat=Mat(new Color(.25f,.34f,.34f));
        leafMat=Mat(new Color(.16f,.40f,.28f)); mintMat=Mat(new Color(.36f,.9f,.7f)); amberMat=Mat(new Color(1f,.70f,.30f));
        redMat=Mat(new Color(.90f,.34f,.29f)); whiteMat=Mat(new Color(.90f,.91f,.78f)); darkMat=Mat(new Color(.07f,.16f,.19f));
        cam=Camera.main;
        if(cam==null) cam=new GameObject("Main Camera",typeof(Camera)).GetComponent<Camera>();
        cam.tag="MainCamera"; cam.orthographic=true; cam.clearFlags=CameraClearFlags.SolidColor;
        cam.backgroundColor=new Color(.06f,.12f,.14f);
        if(Object.FindObjectOfType<AudioListener>()==null) cam.gameObject.AddComponent<AudioListener>();
        var lamp=new GameObject("Afternoon sunlight").AddComponent<Light>(); lamp.type=LightType.Directional; lamp.intensity=1.05f;
        lamp.transform.rotation=Quaternion.Euler(50,-30,0); RenderSettings.ambientLight=new Color(.60f,.66f,.63f);
        speaker=gameObject.AddComponent<AudioSource>();
        stepSound=Tone(230,.035f); seedSound=Tone(620,.12f); errorSound=Tone(130,.14f); winSound=Tone(880,.4f);
        gameObject.AddComponent<GardenUI>();
        BuildLevel(0); State=View.Menu;
    }
    Material Mat(Color color)
    {
        var m=new Material(Resources.Load<Material>("RuntimeMaterial")); m.color=color;
        if(color==new Color(.36f,.9f,.7f)||color==new Color(1f,.70f,.30f)) { m.EnableKeyword("_EMISSION"); m.SetColor("_EmissionColor",color*.25f); }
        materials.Add(m); return m;
    }
    GameObject Shape(string label,PrimitiveType type,Vector3 p,Vector3 size,Material material,Transform parent=null)
    {
        var g=GameObject.CreatePrimitive(type); g.name=label; g.transform.SetParent(parent==null?world:parent,false);
        g.transform.localPosition=p; g.transform.localScale=size; g.GetComponent<Renderer>().sharedMaterial=material; return g;
    }
    void BuildLevel(int stage)
    {
        if(world!=null) { world.gameObject.SetActive(false); Destroy(world.gameObject); }
        Stage=stage; Lives=3; Seeds=0; Steps=0; Seconds=0; stageScore=0; mistakes=0; HintUsed=false; HintOpen=false; JournalFound=false; winGrowth=0;
        Sequence.Clear(); for(int i=0;i<3;i++) { Sources[i]=false; Valves[i]=0; }
        walkable.Clear(); hazards.Clear(); pickups.Clear(); foliage.Clear(); notebook=null;
        world=new GameObject("Zone "+(stage+1)).transform;
        var map=ExpeditionMaps.Levels[stage];
        Shape("Floating earth",PrimitiveType.Cube,new Vector3(0,-.65f,0),new Vector3(9.2f,1.1f,9.2f),soilMat);
        for(int y=0;y<map.Length;y++) for(int x=0;x<map[y].Length;x++)
        {
            var c=new Vector2Int(x,y); Vector3 p=ExpeditionMaps.World(c); char ch=map[y][x];
            Shape("Garden tile",PrimitiveType.Cube,p+Vector3.down*.1f,new Vector3(.97f,.20f,.97f),pathMat);
            if(ch=='#')
            {
                Shape("Rock",PrimitiveType.Cube,p+Vector3.up*.25f,new Vector3(.9f,.55f,.9f),rockMat);
                if((x+y)%3==0)
                {
                    Shape("Tree trunk",PrimitiveType.Cylinder,p+Vector3.up*.55f,new Vector3(.14f,.4f,.14f),soilMat);
                    var crown=Shape("Tree crown",PrimitiveType.Sphere,p+Vector3.up*1.05f,new Vector3(.85f,.95f,.85f),leafMat); foliage.Add(crown.transform);
                }
                continue;
            }
            walkable.Add(c);
            if(ch=='@') spawn=c;
            if(ch=='*')
            {
                var seed=Shape("Seed",PrimitiveType.Sphere,p+Vector3.up*.42f,new Vector3(.34f,.46f,.34f),amberMat);
                Shape("Seed leaf",PrimitiveType.Sphere,new Vector3(.15f,.25f,0),new Vector3(.5f,.2f,.3f),mintMat,seed.transform);
                pickups[c]=seed;
            }
            if(ch=='!') { hazards.Add(c); Shape("Bramble patch",PrimitiveType.Cube,p+Vector3.up*.03f,new Vector3(.82f,.09f,.82f),redMat); }
            if(ch=='?') { notebook=Shape("Field notebook",PrimitiveType.Cube,p+Vector3.up*.2f,new Vector3(.45f,.15f,.55f),whiteMat); }
            if(ch=='C')
            {
                consoleCell=c;
                Shape("Restoration console",PrimitiveType.Cylinder,p+Vector3.up*.2f,new Vector3(.8f,.25f,.8f),darkMat);
                beacon=Shape("Console beacon",PrimitiveType.Sphere,p+Vector3.up*.75f,new Vector3(.35f,.35f,.35f),amberMat);
                flower=new GameObject("Restoration bloom").transform; flower.SetParent(world); flower.position=p+Vector3.up*.5f;
                for(int j=0;j<5;j++)
                {
                    float a=j*Mathf.PI*2/5;
                    Shape("Petal",PrimitiveType.Sphere,new Vector3(Mathf.Cos(a)*.4f,.4f,Mathf.Sin(a)*.4f),new Vector3(.5f,.2f,.5f),mintMat,flower);
                }
                flower.localScale=Vector3.zero;
            }
        }
        cell=spawn; targetPosition=ExpeditionMaps.World(cell);
        robot=new GameObject("Nori - garden keeper").transform; robot.SetParent(world); robot.position=targetPosition;
        Shape("Body",PrimitiveType.Cylinder,new Vector3(0,.36f,0),new Vector3(.46f,.30f,.46f),whiteMat,robot);
        Shape("Head",PrimitiveType.Cube,new Vector3(0,.78f,0),new Vector3(.55f,.38f,.45f),mintMat,robot);
        Shape("Visor",PrimitiveType.Cube,new Vector3(0,.79f,-.23f),new Vector3(.39f,.16f,.04f),darkMat,robot);
        Shape("Eye L",PrimitiveType.Sphere,new Vector3(-.105f,.80f,-.26f),Vector3.one*.065f,amberMat,robot);
        Shape("Eye R",PrimitiveType.Sphere,new Vector3(.105f,.80f,-.26f),Vector3.one*.065f,amberMat,robot);
        Shape("Antenna",PrimitiveType.Sphere,new Vector3(.18f,1.10f,0),Vector3.one*.14f,amberMat,robot);
        cam.transform.position=new Vector3(0,13,-9); cam.transform.LookAt(Vector3.zero);
        Message="Ramasse les 3 graines dorees. Marche sur le carnet blanc pour trouver un indice.";
    }
    void Update()
    {
        cam.rect=new Rect(0,0,.68f,1);
        float aspect=Mathf.Max(.1f,Screen.width*.68f/Screen.height); cam.orthographicSize=Mathf.Max(5.1f,5.3f/aspect);
        bool active=State==View.Play||State==View.Puzzle;
        if(active) Seconds+=Time.deltaTime;
        if(robot!=null)
        {
            robot.position=Vector3.MoveTowards(robot.position,targetPosition,Time.deltaTime*7);
            if(State==View.Play) robot.localScale=new Vector3(1,1+Mathf.Sin(Time.time*3)*.025f,1);
        }
        if(active) foreach(var p in pickups) { var v=p.Value.transform; Vector3 basePos=ExpeditionMaps.World(p.Key); v.position=basePos+Vector3.up*(.45f+Mathf.Sin(Time.time*2.6f+p.Key.x)*.08f); v.Rotate(0,Time.deltaTime*40,0); }
        if(beacon!=null) beacon.transform.localScale=Vector3.one*(.35f+Mathf.Sin(Time.time*3)*.025f);
        if(State==View.Win) { winGrowth=Mathf.MoveTowards(winGrowth,1,Time.deltaTime); flower.localScale=Vector3.one*winGrowth; }
    }
    public void StartStage(int index) { if(State==View.Menu) completedScore=0; BuildLevel(Mathf.Clamp(index,0,2)); State=View.Play; }
    public void Move(Vector2Int dir)
    {
        if(State!=View.Play||Vector3.Distance(robot.position,targetPosition)>.01f) return;
        var next=cell+dir; if(!walkable.Contains(next)) { Message="Le rocher bloque le chemin. Cherche un autre passage."; return; }
        Steps++;
        if(hazards.Contains(next))
        {
            mistakes++; Lives--; Play(errorSound); Message="Des ronces ! Tu perds un coeur. Contourne les cases rouges.";
            if(Lives<=0) State=View.Lose;
            return;
        }
        cell=next; targetPosition=ExpeditionMaps.World(cell); Play(stepSound);
        if(pickups.ContainsKey(cell))
        {
            Destroy(pickups[cell]); pickups.Remove(cell); Seeds++; stageScore+=100; Play(seedSound);
            Message=Seeds==3?"Les 3 graines sont reunies ! Rejoins la borne lumineuse, puis appuie sur E.":"Graine trouvee ! Encore "+(3-Seeds)+" a recuperer.";
            if(Seeds==3) beacon.GetComponent<Renderer>().sharedMaterial=mintMat;
        }
        if(notebook!=null)
        {
            Vector3 delta=notebook.transform.position-ExpeditionMaps.World(cell); delta.y=0;
            if(delta.sqrMagnitude<.1f) { JournalFound=true; Message=ExpeditionMaps.Journals[Stage]; Destroy(notebook); notebook=null; Play(seedSound); }
        }
        if(NearConsole) Message=Seeds<3?"Cette borne demande 3 graines. Continue l'exploration.":"Borne prete ! Appuie sur E ou sur OUVRIR LA BORNE.";
    }
    public void Interact()
    {
        if(State!=View.Play) return;
        if(!NearConsole) { Message="Approche-toi de la borne ronde lumineuse."; return; }
        if(Seeds<3) { Message="Il manque "+(3-Seeds)+" graine(s) pour activer la borne."; return; }
        State=View.Puzzle; Message="Observe l'indice et prepare ta reponse.";
    }
    public void Activate(int i)
    {
        if(State!=View.Puzzle||i<0||i>2) return;
        Play(stepSound);
        if(Stage==0) Sources[i]=!Sources[i]; else if(Stage==1) Valves[i]=(Valves[i]+1)%4;
        else { if(Sequence.Count<3) Sequence.Add(i); if(Sequence.Count==3) Verify(); }
    }
    public void Verify()
    {
        if(State!=View.Puzzle) return;
        bool ok=Stage==0?GardenRules.Light(Sources[0],Sources[1],Sources[2]):Stage==1?GardenRules.Water(Valves[0],Valves[1],Valves[2]):GardenRules.Cycle(Sequence.ToArray());
        if(ok)
        {
            stageScore+=500+Lives*50+Mathf.Max(0,180-Mathf.FloorToInt(Seconds));
            PlayerPrefs.SetInt("EcoExpedition.Unlocked",Mathf.Max(Unlocked,Mathf.Min(2,Stage+1)));
            PlayerPrefs.SetInt("EcoExpedition.Best."+Stage,Mathf.Max(Best,stageScore)); PlayerPrefs.Save();
            State=View.Win; Message="La nature reprend sa place."; Play(winSound);
        }
        else
        {
            mistakes++; Lives--; Play(errorSound); Message="Pas encore. Relis l'indice et ajuste tes choix.";
            Sequence.Clear(); if(Lives<=0) State=View.Lose;
        }
    }
    public void ClearSequence() { if(State==View.Puzzle) Sequence.Clear(); }
    public void LeavePuzzle() { if(State==View.Puzzle) State=View.Play; }
    public void Hint() { HintOpen=!HintOpen; if(HintOpen) HintUsed=true; }
    public void Pause() { if(State==View.Play||State==View.Puzzle) { beforePause=State; State=View.Pause; } else if(State==View.Pause) State=beforePause; }
    public void Help() { beforeHelp=State; State=View.Help; }
    public void Back() { State=beforeHelp; }
    public void Credits() { State=View.Credits; }
    public void Menu() { State=View.Menu; }
    public void Next() { completedScore+=stageScore; if(Stage<2) { BuildLevel(Stage+1); State=View.Play; } else Menu(); }
    public void Replay() { BuildLevel(Stage); State=View.Play; }
    void Play(AudioClip clip) { if(Sound) speaker.PlayOneShot(clip,.24f); }
    AudioClip Tone(float hz,float duration)
    {
        int n=(int)(22050*duration); var data=new float[n];
        for(int i=0;i<n;i++) data[i]=Mathf.Sin(2*Mathf.PI*hz*i/22050f)*(.4f*(1f-(float)i/n));
        var clip=AudioClip.Create("Garden tone",n,1,22050,false); clip.SetData(data,0); return clip;
    }
    void OnDestroy() { foreach(var m in materials) Destroy(m); Destroy(stepSound); Destroy(seedSound); Destroy(errorSound); Destroy(winSound); }
}

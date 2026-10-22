using UnityEngine;

// UI uses a fitted canvas. Gameplay input is captured only by the Game view.
public class GardenUI : MonoBehaviour
{
    GardenGame game;
    GUIStyle title, heading, text, small, tag, button, white;
    Texture2D normal, hover;
    readonly Color paper=new Color(.92f,.92f,.85f), ink=new Color(.10f,.23f,.21f), muted=new Color(.33f,.44f,.39f), green=new Color(.12f,.35f,.28f), gold=new Color(.93f,.65f,.25f);
    readonly string[] lightNames={"SOLEIL","EAU","FUMEE"}, valveNames={"GAUCHE","MILIEU","DROITE"}, cycleNames={"SOLEIL","NUAGE","PLUIE"};
    void Awake() { game=GetComponent<GardenGame>(); }
    Texture2D Pixel(Color c) { var t=new Texture2D(1,1); t.SetPixel(0,0,c); t.Apply(); return t; }
    void Styles()
    {
        if(title!=null) return;
        title=new GUIStyle(GUI.skin.label) {fontSize=76,fontStyle=FontStyle.Bold,wordWrap=true};
        heading=new GUIStyle(title) {fontSize=30}; text=new GUIStyle(GUI.skin.label) {fontSize=22,wordWrap=true}; small=new GUIStyle(text) {fontSize=17};
        tag=new GUIStyle(small) {fontStyle=FontStyle.Bold}; white=new GUIStyle(text) {fontSize=18};
        title.normal.textColor=ink; heading.normal.textColor=ink; text.normal.textColor=ink; small.normal.textColor=muted; tag.normal.textColor=green; white.normal.textColor=Color.white;
        normal=Pixel(green); hover=Pixel(new Color(.21f,.47f,.37f));
        button=new GUIStyle(GUI.skin.button) {fontSize=20,fontStyle=FontStyle.Bold,wordWrap=true};
        button.normal.background=normal; button.hover.background=hover; button.active.background=hover;
        button.normal.textColor=paper; button.hover.textColor=Color.white; button.active.textColor=Color.yellow;
    }
    void Fill(float x,float y,float w,float h,Color color) { var old=GUI.color; GUI.color=color; GUI.DrawTexture(new Rect(x,y,w,h),Texture2D.whiteTexture); GUI.color=old; }
    void Label(float x,float y,float w,float h,string s,GUIStyle style) { GUI.Label(new Rect(x,y,w,h),s,style); }
    bool Button(float x,float y,float w,float h,string s) { return GUI.Button(new Rect(x,y,w,h),s,button); }
    void OnGUI()
    {
        Styles(); Keys();
        var old=GUI.matrix; float scale=Mathf.Min(Screen.width/1440f,Screen.height/900f);
        GUI.matrix=Matrix4x4.TRS(new Vector3((Screen.width-1440*scale)*.5f,(Screen.height-900*scale)*.5f,0),Quaternion.identity,new Vector3(scale,scale,1));
        switch(game.State)
        {
            case GardenGame.View.Menu: Menu(); break;
            case GardenGame.View.Help: Help(); break;
            case GardenGame.View.Credits: Credits(); break;
            case GardenGame.View.Play: Hud(); break;
            case GardenGame.View.Puzzle: GUI.enabled=false; Hud(); GUI.enabled=true; Puzzle(); break;
            default: GUI.enabled=false; Hud(); GUI.enabled=true; Overlay(); break;
        }
        GUI.matrix=old;
    }
    void Background()
    {
        Fill(-5000,-5000,11000,11000,paper); Fill(0,0,18,900,green);
        Label(70,43,1200,30,"CARNET D'EXPEDITION     /     MOLKA BOUGHANMI",tag);
        Label(70,850,1250,30,"EcoLab Expedition  /  Une aventure pour apprendre a prendre soin du vivant.",small);
    }
    void Menu()
    {
        Background();
        Label(70,145,760,110,"EcoLab",title); Label(75,255,700,60,"EXPEDITION",heading);
        Label(75,355,650,125,"Une graine trouvee.\nUn jardin qui recommence.",heading);
        Label(75,490,630,100,"Guide Nori, le petit gardien de la serre. Explore trois jardins, retrouve les graines et remets leurs bornes en marche.",text);
        if(Button(75,625,430,65,"PARTIR EN EXPEDITION")) game.StartStage(0);
        if(Button(75,705,210,52,"COMMENT JOUER")) game.Help();
        if(Button(300,705,205,52,"CREDITS")) game.Credits();
        if(Button(75,774,430,45,"QUITTER")) {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;
#endif
        }
        Fill(835,150,525,660,new Color(.85f,.88f,.79f));
        Label(875,185,440,50,"LES TROIS JARDINS",heading);
        for(int i=0;i<3;i++)
        {
            GUI.enabled=i<=game.Unlocked;
            if(Button(875,280+i*125,440,80,"0"+(i+1)+"  /  "+ExpeditionMaps.Titles[i])) game.StartStage(i);
            Label(880,365+i*125,430,30,i<=game.Unlocked?"EXPEDITION DISPONIBLE":"A DEBLOQUER",small);
        }
        GUI.enabled=true;
        Label(875,695,440,85,"Explore, observe, resous.\nVise les 3 etoiles sans erreur ni indice.",text);
    }
    void Help()
    {
        Background(); Label(70,125,1260,100,"LE GUIDE DE NORI",title);
        Label(75,260,1190,110,"Dans chaque jardin : ramasse les 3 graines dorees, trouve le carnet blanc, puis approche-toi de la borne lumineuse pour resoudre son enigme.",text);
        Label(75,405,1190,180,"FLECHES / WASD / ZQSD : avancer d'une case\nE : ouvrir la borne quand tu es juste a cote\nECHAP : mettre en pause ou reprendre\nSOURIS : boutons directionnels et commandes des enigmes",text);
        Label(75,610,1190,105,"Les rochers bloquent le passage. Les ronces rouges retirent un coeur : contourne-les. Tu as 3 coeurs par jardin. Un indice aide a reussir mais empeche d'obtenir 3 etoiles.",text);
        if(Button(75,760,380,58,"C'EST PARTI / RETOUR")) game.Back();
    }
    void Credits()
    {
        Background(); Label(70,140,1200,100,"LE CARNET",title);
        Label(75,295,1150,65,GardenGame.Author,heading);
        Label(75,400,1160,200,"EcoLab Expedition\nUne aventure educative autour de l'eau et du vivant.\nUnity / C# - exploration, enigmes et progression.\nNori et son jardin sont construits avec des formes 3D.\nProjet individuel / 2026.",text);
        if(Button(75,755,380,58,"RETOUR AU MENU")) game.Menu();
    }
    void Hud()
    {
        Fill(980,-1000,460,3000,paper);
        Label(1008,34,400,30,"JARDIN 0"+(game.Stage+1)+" / 03",tag);
        Label(1008,90,400,100,ExpeditionMaps.Titles[game.Stage],heading);
        Label(1008,215,400,40,"GRAINES    "+game.Seeds+" / 3",text);
        Fill(1008,265,390,10,new Color(.78f,.81f,.71f)); Fill(1008,265,390*game.Seeds/3f,10,green);
        Label(1008,310,400,40,"COEURS    "+game.Lives+" / 3",text);
        Label(1008,365,400,40,"SCORE    "+game.Score,text);
        Label(1008,420,400,40,"PAS    "+game.Steps+"     /     "+Mathf.FloorToInt(game.Seconds)+" s",small);
        Label(1008,485,400,90,game.Seeds<3?"1. Retrouve les graines\n2. Lis le carnet blanc\n3. Active la borne":"Graines reunies !\nRejoins la borne lumineuse.",text);
        if(Button(1008,610,390,65,"OUVRIR LA BORNE  [E]")) game.Interact();
        if(Button(1008,695,185,48,"PAUSE")) game.Pause();
        if(Button(1207,695,191,48,"RECOMMENCER")) game.Replay();
        Label(1008,778,395,70,"Record jardin : "+game.Best+" pts\n"+GardenGame.Author,small);
        Fill(25,20,925,55,new Color(.04f,.12f,.13f,.9f)); Label(45,33,885,40,"NORI  /  RECUPERE LES GRAINES ET RESTAURE LE JARDIN",white);
        Fill(300,735,650,140,new Color(.04f,.12f,.13f,.93f)); Label(323,757,604,107,game.Message,white);
        if(Button(112,718,70,48,"^")) game.Move(Vector2Int.down);
        if(Button(30,777,70,48,"<")) game.Move(Vector2Int.left);
        if(Button(112,777,70,48,"v")) game.Move(Vector2Int.up);
        if(Button(194,777,70,48,">")) game.Move(Vector2Int.right);
    }
    void Puzzle()
    {
        Fill(-5000,-5000,11000,11000,new Color(.015f,.06f,.05f,.72f));
        Fill(255,100,930,710,paper);
        Label(300,135,840,80,GardenRules.Names[game.Stage],heading);
        string clue=game.JournalFound?ExpeditionMaps.Journals[game.Stage]:GardenRules.Clues[game.Stage];
        Label(300,230,840,120,clue,text);
        Label(300,365,840,35,"COEURS : "+game.Lives+"   /   TROIS ETOILES : SANS ERREUR NI INDICE",small);
        for(int i=0;i<3;i++)
        {
            string s=game.Stage==0?lightNames[i]+"\n"+(game.Sources[i]?"ON":"OFF"):game.Stage==1?valveNames[i]+"\n"+game.Valves[i]+" L":cycleNames[i];
            if(Button(300+i*285,425,270,80,s)) game.Activate(i);
        }
        if(game.Stage==2)
        {
            string seq=""; foreach(int i in game.Sequence) seq+=(seq.Length>0?" > ":"")+cycleNames[i];
            Label(300,525,840,40,"Sequence : "+seq,text);
            if(Button(300,580,400,55,"EFFACER")) game.ClearSequence();
        }
        else if(Button(300,580,400,55,"VERIFIER")) game.Verify();
        if(Button(720,580,195,55,"INDICE")) game.Hint();
        if(Button(930,580,195,55,"FERMER")) game.LeavePuzzle();
        Label(300,665,840,110,game.HintOpen?GardenRules.Hints[game.Stage]:game.Message,small);
    }
    void Overlay()
    {
        bool won=game.State==GardenGame.View.Win, lost=game.State==GardenGame.View.Lose;
        Fill(-5000,-5000,11000,11000,new Color(.015f,.06f,.05f,.60f)); Fill(345,155,750,590,paper);
        Label(390,200,660,75,won?(game.Stage==2?"LE JARDIN EST SAUVE !":"UN JARDIN RENAIT"):lost?"MISSION INTERROMPUE":"PAUSE",heading);
        if(won)
        {
            string stars=new string('*',game.Stars)+new string('-',3-game.Stars);
            Label(390,300,660,50,"ETOILES   "+stars+"    /    SCORE   "+game.Score,heading);
            Label(390,370,660,85,game.Stars==3?"Exploration parfaite ! Aucune erreur et aucun indice utilise.":"Mission reussie. Rejoue pour gagner plus d'etoiles et ameliorer ton record.",text);
            for(int i=0;i<24;i++)
            {
                float x=360+(i*79)%700, y=165+(Time.unscaledTime*45+i*47)%500;
                Fill(x,y,5,9,i%2==0?gold:green);
            }
        }
        else Label(390,305,660,130,lost?"Tes trois coeurs sont epuises. Recommence ce jardin, contourne les ronces et prends le temps de lire les indices.":"Tu peux reprendre ton expedition ou consulter les commandes.",text);
        if(Button(390,490,660,65,won?(game.Stage==2?"TERMINER / MENU":"JARDIN SUIVANT"):lost?"REESSAYER":"REPRENDRE")) { if(won) game.Next(); else if(lost) game.Replay(); else game.Pause(); }
        if(Button(390,575,210,50,"MENU")) game.Menu();
        if(Button(615,575,210,50,won?"REJOUER":"COMMANDES")) { if(won) game.Replay(); else game.Help(); }
        if(Button(840,575,210,50,"SON : "+(game.Sound?"ON":"OFF"))) game.Sound=!game.Sound;
        Label(390,675,660,35,GardenGame.Author+" / EcoLab Expedition",small);
    }
    void Keys()
    {
        Event e=Event.current; if(e.type!=EventType.KeyDown) return;
        if(e.keyCode==KeyCode.Escape) { game.Pause(); e.Use(); return; }
        if(game.State!=GardenGame.View.Play) return;
        switch(e.keyCode)
        {
            case KeyCode.UpArrow: case KeyCode.W: case KeyCode.Z: game.Move(Vector2Int.down); break;
            case KeyCode.DownArrow: case KeyCode.S: game.Move(Vector2Int.up); break;
            case KeyCode.LeftArrow: case KeyCode.A: case KeyCode.Q: game.Move(Vector2Int.left); break;
            case KeyCode.RightArrow: case KeyCode.D: game.Move(Vector2Int.right); break;
            case KeyCode.E: game.Interact(); break;
            default:return;
        }
        e.Use();
    }
    void OnDestroy() { if(normal!=null) Destroy(normal); if(hover!=null) Destroy(hover); }
}

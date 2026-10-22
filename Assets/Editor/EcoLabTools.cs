using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class EcoLabTools
{
    [MenuItem("EcoLab Expedition/Ouvrir le jeu")]
    public static void Open()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }
    [MenuItem("EcoLab Expedition/Preparer le build")]
    public static void Prepare()
    {
        EditorBuildSettings.scenes=new[] { new EditorBuildSettingsScene("Assets/Scenes/Main.unity",true) };
        PlayerSettings.companyName=GardenGame.Author; PlayerSettings.productName="EcoLab Expedition";
        PlayerSettings.defaultScreenWidth=1440; PlayerSettings.defaultScreenHeight=900;
        PlayerSettings.resizableWindow=true;
        Debug.Log("EcoLab : scene et nom configures. File > Build Settings pour exporter sur PC ou WebGL.");
    }
    [MenuItem("EcoLab Expedition/Verifier les regles")]
    public static void Check()
    {
        int light=0, water=0, cycle=0;
        for(int i=0;i<8;i++) if(GardenRules.Light((i&1)!=0,(i&2)!=0,(i&4)!=0)) light++;
        for(int a=0;a<4;a++) for(int b=0;b<4;b++) for(int c=0;c<4;c++) if(GardenRules.Water(a,b,c)) water++;
        for(int a=0;a<3;a++) for(int b=0;b<3;b++) for(int c=0;c<3;c++) if(GardenRules.Cycle(new[]{a,b,c})) cycle++;
        if(light!=1||water!=1||cycle!=1) throw new Exception("Une regle d'enigme est invalide.");
        if(!GardenRules.Light(true,true,false)||!GardenRules.Water(1,2,3)||!GardenRules.Cycle(new[]{0,1,2})) throw new Exception("Solution incorrecte.");
        Debug.Log("EcoLab : 8 combinaisons lumiere, 64 reglages eau et 27 sequences verifies. Une solution par enigme.");
    }
}

using UnityEngine;
using System.Collections;
using com.ihaiu;
using Games;

/** Game Facade */
public class Game
{
    #region gameframe
	public static GameObject 			go;
	public static MainThreadManager 	mainThread;
    public static AssetManager 			asset;
	public static ConfigManager 		config;
    public static int menu;
    public static int pool;
    public static int proto;
    public static int audio;
    #endregion


    #region user
    public static int user;
    #endregion

	public static IEnumerator Install(GameObject go)
    {
		Game.go 			= go;
		Game.mainThread		= go.AddComponent<MainThreadManager>();
		Game.config 		= new ConfigManager();

        yield break;
    }
}

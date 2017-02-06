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
	public static MenuManager 			menu;
	public static ModuleManager			module;
	public static LoadManager			loader;
    public static int pool;
    public static int proto;
    public static int audio;
    #endregion

	#region global other
	public static GameCamera 	camera 		= new GameCamera();
	public static GameCanvas 	canvas		= new GameCanvas();
	public static UILayer 		uiLayer 	= new UILayer();
    public static GameCircle    cricle      = new GameCircle();
	#endregion


    #region user
	public static UserData user = new UserData();
    #endregion

	public static IEnumerator Install(GameObject go)
    {
		Game.go 			= go;
		Game.mainThread		= go.AddComponent<MainThreadManager>();
		Game.config 		= new ConfigManager();
		Game.menu			= new MenuManager();
		Game.module			= new ModuleManager();

        yield break;
    }
}

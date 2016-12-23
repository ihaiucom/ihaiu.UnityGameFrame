using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScene 
{
	public static string Launch 	= "Launch";
	public static string War		= "War";

	public static Scene CurrentScene
	{
		get
		{
			return SceneManager.GetActiveScene();
		}
	}

	public static string CurrentName
	{
		get
		{
			return CurrentScene.name;
		}
	}

	public static bool IsLaunch
	{
		get
		{
			return CurrentName == Launch;
		}
	}

	public static bool IsWar
	{
		get
		{
			return CurrentName == War;
		}
	}



}

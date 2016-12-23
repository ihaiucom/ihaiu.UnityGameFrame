using UnityEngine;
using System.Collections;

public class GameCanvas 
{
	public enum CanvaType
	{
		Main
	}


	private Canvas _main;
	public Canvas main
	{
		get
		{
			if(_main == null)
			{
				GameObject go = GameObject.Find("Canvas");
				_main = go.GetComponent<Canvas>();
			}
			return _main;
		}
	}
}

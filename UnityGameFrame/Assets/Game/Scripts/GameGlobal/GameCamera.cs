using UnityEngine;
using System.Collections;

public class GameCamera 
{
	public enum CameraType
	{
		Main,
		UI
	}

	private Camera _main;
	public Camera main
	{
		get
		{
			if(_main == null)
			{
				GameObject go = GameObject.Find("Camera-Main");
				_main = go.GetComponent<Camera>();
			}
			return _main;
		}
	}

	private Camera _ui;
	public Camera ui
	{
		get
		{
			if(_ui == null)
			{
				GameObject go = GameObject.Find("Camera-UI");
				_ui = go.GetComponent<Camera>();
			}
			return _ui;
		}
	}

	private Camera _war;
	public Camera war
	{
		get
		{
			if(_war == null)
			{
				_war = main;
			}
			return _war;
		}
	}

}

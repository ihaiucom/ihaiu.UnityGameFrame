using UnityEngine;
using System.Collections;


namespace Games
{
	public class UILayer
	{
		public enum Layer 
		{
			/** 预安装 */
			Layer_PreInstance,
			/** 默认背景 */
			Layer_DefaultBG = 1,
			/** 主场景 */
			Layer_Home = 2,
			/** 战斗UI */
			Layer_War = 3,
			/** 模块 */
			Layer_Module = 4,
			/** 主UI */
			Layer_MainUI = 5,
			/** 对话框 */
			Layer_Dialog = 6,
			/** 引导 */
			Layer_Guide = 7,
			/** 加载面板 */
			Layer_Loader = 8,
			/** 浮动消息 */
			Layer_FloatMsg = 9,
		}


		private RectTransform _preInstance;
		public RectTransform preInstance
		{
			get
			{
				if(_preInstance == null)
				{
					GameObject go = GameObject.Find("Layer-PreInstance");
					_preInstance = (RectTransform) go.transform;
				}
				return _preInstance;
			}
		}

		private RectTransform _bg;
		public RectTransform bg
		{
			get
			{
				if(_bg == null)
				{
					GameObject go = GameObject.Find("Layer-BG");
					_bg = (RectTransform) go.transform;
				}
				return _bg;
			}
		}

		private RectTransform _home;
		public RectTransform home
		{
			get
			{
				if(_home == null)
				{
					GameObject go = GameObject.Find("Layer-Home");
					_home = (RectTransform) go.transform;
				}
				return _home;
			}
		}

		private RectTransform _war;
		public RectTransform war
		{
			get
			{
				if(_war == null)
				{
					GameObject go = GameObject.Find("Layer-War");
					_war = (RectTransform) go.transform;
				}
				return _war;
			}
		}

		private RectTransform _module;
		public RectTransform module
		{
			get
			{
				if(_module == null)
				{
					GameObject go = GameObject.Find("Layer-Module");
					_module = (RectTransform) go.transform;
				}
				return _module;
			}
		}

		private RectTransform _mainUI;
		public RectTransform mainUI
		{
			get
			{
				if(_mainUI == null)
				{
					GameObject go = GameObject.Find("Layer-MainUI");
					_mainUI = (RectTransform) go.transform;
				}
				return _mainUI;
			}
		}

		private RectTransform _dialog;
		public RectTransform dialog
		{
			get
			{
				if(_dialog == null)
				{
					GameObject go = GameObject.Find("Layer-Dialog");
					_dialog = (RectTransform) go.transform;
				}
				return _dialog;
			}
		}

		private RectTransform _guide;
		public RectTransform guide
		{
			get
			{
				if(_guide == null)
				{
					GameObject go = GameObject.Find("Layer-Guide");
					_guide = (RectTransform) go.transform;
				}
				return _guide;
			}
		}

		private RectTransform _loader;
		public RectTransform loader
		{
			get
			{
				if(_loader == null)
				{
					GameObject go = GameObject.Find("Layer-Loader");
					_loader = (RectTransform) go.transform;
				}
				return _loader;
			}
		}



		public RectTransform GetLayer(Layer layer)
		{
			switch(layer)
			{
			case Layer.Layer_PreInstance:
				return preInstance;

			case Layer.Layer_DefaultBG:
				return bg;

			case Layer.Layer_Home:
				return home;

			case Layer.Layer_War:
				return war;

			case Layer.Layer_Module:
				return module;

			case Layer.Layer_MainUI:
				return mainUI;

			case Layer.Layer_Dialog:
				return dialog;

			case Layer.Layer_Guide:
				return guide;

			case Layer.Layer_Loader:
				return loader;
			}
			return null;
		}
	}
}
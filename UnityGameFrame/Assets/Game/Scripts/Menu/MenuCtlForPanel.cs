using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Games
{
	public class MenuCtlForPanel : MenuCtl
	{

		public AbstractWindow       window;
		private bool                isPreinstall    = false;

		public string windowPath
		{
			get
			{
				return config.path;
			}
		}


		override public void Destory()
		{
			if (window != null)
			{
				window.Destory();
			}
		}

		override public void OnDestory()
		{
		}


		override public void Open(bool isPreinstall = false)
		{
			this.isPreinstall = isPreinstall;
			if (window == null)
			{
				Load();
			}
			else
			{
				if (!isPreinstall)
				{
					SetWindowShow();
				}
			}

			cacheTime = 0;
		}

		override public void Close()
		{
			if (window != null)
			{
				window.Hide();
			}


			state       = StateType.Closed;
			cacheTime   = 0;
		}



		override protected void OnLoadAssetsComplete()
		{
			LoadWindow();
		}

		// load window
		private void LoadWindow()
		{
			Game.asset.Load(windowPath, OnLoadWindow);
		}


		private void OnLoadWindow(string filename, object obj)
		{
			if (obj == null)
			{
				Debug.LogErrorFormat("MenuItem OnLoadWindow obj=null, menuId={0}, menuName={1}, filename={2}", menuId, config.name, filename);
				return;
			}

			GameObject go = GameObject.Instantiate((GameObject)obj);
			window = go.GetComponent<AbstractWindow>();

			if (window != null)
			{
				window.module   = module;
				window.menuCtl  = this;

				if (isPreinstall)
				{
					window.rectTransform.SetParent(Game.uiLayer.GetLayer(UILayer.Layer.Layer_PreInstance), false);
					go.SetActive(false);
				}
				else
				{
					if (state != StateType.Closed)
					{
						SetWindowShow();
					}
				}
			}
		}

		private void SetWindowShow()
		{
			window.rectTransform.SetParent(Game.uiLayer.GetLayer(config.layer), false);
			window.rectTransform.localScale = Vector3.one;

			switch(config.layout)
			{
			case MenuLayout.ScreenSize:
				window.rectTransform.offsetMin = Vector2.zero;
				window.rectTransform.offsetMax = Vector2.zero;
				break;

			case MenuLayout.PositionZero:
				window.rectTransform.anchoredPosition = Vector2.zero;
				break;
			}


			window.Show();
			window.rectTransform.SetAsLastSibling();

			CloseOther();
			state = StateType.Opened;
		}






	}
}

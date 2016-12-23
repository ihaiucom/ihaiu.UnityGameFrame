using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games
{
	public abstract class AbstractModule 
	{
		public int menuId;


		public void OnGamePreInstall()
		{

		}

		public void OnGameInstall()
		{
			
		}


		/** 打开模块时，获取需要预加载的资源 */
		public List<string> GetLoadAssets()
		{
			return null;
		}

		public void Open()
		{
			Game.menu.Open(menuId);
		}

		public void Close()
		{
			Game.menu.Close(menuId);
		}

        public void Back()
        {
            
        }

        public void SetBackMenu(int menuId)
        {
            
        }

	}
}

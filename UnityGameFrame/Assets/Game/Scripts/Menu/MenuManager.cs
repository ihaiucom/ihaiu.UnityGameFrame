using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games
{
	public class MenuManager 
	{
        private Dictionary<int, MenuCtl>    dict = new Dictionary<int, MenuCtl>();
        private List<MenuCtl>               list = new List<MenuCtl>();

        public MenuManager()
        {
            Game.mainThread.unityUpdate += OnUpdate;
        }


        private MenuCtl GetMenuCtl(int menuId)
        {
            if (dict.ContainsKey(menuId))
            {
                return dict[menuId];
            }

            return null;
        }


        public List<MenuCtl> GetMenuCtlList()
        {
            List<MenuCtl> list = new List<MenuCtl>();
            list.AddRange(this.list);
            return list;
        }

		public void Open(int menuId)
		{
            MenuCtl menuCtl = GetMenuCtl(menuId);
            if (menuCtl == null)
            {
                MenuConfig menuConfig = Game.config.menu.GetConfig(menuId);
                if (menuConfig == null)
                {
                    Debug.LogErrorFormat("MenuManager Open menuConfig=null menuId={0}", menuId);
                    return;
                }

                menuCtl = new MenuCtl();
                menuCtl.menuId = menuId;
                menuCtl.config = menuConfig;
                menuCtl.module = Game.module.GetModule(menuId);
                dict.Add(menuCtl.menuId, menuCtl);
                list.Add(menuCtl);
            }

            menuCtl.Open();
		}

		public void Close(int menuId)
		{

            MenuCtl menuCtl = GetMenuCtl(menuId);
            if (menuCtl != null)
            {
                menuCtl.Close();
            }
		}


        void OnUpdate()
        {
            for(int i = 0; i < list.Count; i ++)
            {
                if (list[i].state == MenuCtl.StateType.Closed)
                {
                    list[i].cacheTime += Time.deltaTime;

                    if (list[i].config.cacheTime >= 0 && list[i].cacheTime > list[i].config.cacheTime)
                    {
                        list[i].Destory();
                    }
                }
            }
        }


	}
}

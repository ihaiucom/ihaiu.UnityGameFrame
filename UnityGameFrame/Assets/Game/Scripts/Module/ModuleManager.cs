using UnityEngine;
using System.Collections;
using Games.Modules;
using System.Collections.Generic;


namespace Games
{
	public partial class ModuleManager 
	{
		

		public List<AbstractModule> 				list = new List<AbstractModule>();
		private Dictionary<int, AbstractModule> 	dict = new Dictionary<int, AbstractModule>();

		public AbstractModule GetModule(int menuId)
		{
			if(dict.ContainsKey(menuId))
			{
				return dict[menuId];
			}

			return null;
		}



		public void PreInstallModules()
		{
			for(int i = 0; i < list.Count; i ++)
			{
				list[i].OnGamePreInstall();
			}
		}

		public void InstallModules()
		{
			for(int i = 0; i < list.Count; i ++)
			{
				list[i].OnGameInstall();
			}
		}
	}
}

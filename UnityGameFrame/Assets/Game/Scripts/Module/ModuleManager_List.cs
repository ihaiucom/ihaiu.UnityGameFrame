using System;
using System.Collections;
using System.Collections.Generic;
using Games.Modules;
namespace Games
{
	public partial class ModuleManager
	{

		public LoginModule	login;
		public HomeModule	home;


		public void CreateModules()
		{

			login = new LoginModule ();
			login.menuId = MenuId.Login;
			list.Add(login);
			dict.Add(login.menuId, login);


			home = new HomeModule ();
			home.menuId = MenuId.Home;
			list.Add(home);
			dict.Add(home.menuId, home);



		}
	}
}

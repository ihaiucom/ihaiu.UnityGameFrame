using System;
using System.Collections;
using System.Collections.Generic;
using com.ihaiu;
using Games;
namespace Games
{
	public partial class ConfigManager
	{

		public MsgConfigReader	msg	= new MsgConfigReader();
		public MenuConfigReader	menu	= new MenuConfigReader();
		public LoadConfigReader	load	= new LoadConfigReader();


		private List<IConfigReader> _l;
		public List<IConfigReader> readerList
		{
			get
			{
				if(_l == null)
				{
					_l = new List<IConfigReader>();
					_l.Add(msg);
					_l.Add(menu);
					_l.Add(load);

				}
				return _l;
			}
		}
	}
}

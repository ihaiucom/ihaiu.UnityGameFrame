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


		private List<IConfigReader> _l;
		public List<IConfigReader> readerList
		{
			get
			{
				if(_l == null)
				{
					_l.Add(msg);
					_l.Add(menu);

				}
				return _l;
			}
		}
	}
}

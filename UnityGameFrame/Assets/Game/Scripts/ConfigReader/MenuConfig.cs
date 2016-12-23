using UnityEngine;
using System.Collections;

namespace Games
{
	public class MenuConfig
	{
		public int 		            id;
		public string 	            name;
        public string               path;
        public MenuType             type;
        public UILayer.Layer        layer;
        public MenuLayout           layout;
        public MenuCloseOtherType   closeOtherType;
        public float                cacheTime = -1;

	}
}

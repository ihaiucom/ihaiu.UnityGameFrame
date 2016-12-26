using UnityEngine;
using System.Collections;

namespace Games
{
	public class MenuConfig
	{
		/** id */
		public int 		            id;
		/** 名称 */
		public string 	            name;
		/** 预设路径 或者 场景名称 */
        public string               path;
		/** 类型 */
        public MenuType             type;
		/** UI层级 */
        public UILayer.Layer        layer;
		/** 布局方式 */
        public MenuLayout           layout;
		/** 关闭其他面板包含哪些 */
        public MenuCloseOtherType   closeOtherType;
		/** 关闭面板后缓存多长时间销毁（-1永久不销毁 0下一帧销毁 大于0会缓存时间秒） */
        public float                cacheTime = -1;

	}
}

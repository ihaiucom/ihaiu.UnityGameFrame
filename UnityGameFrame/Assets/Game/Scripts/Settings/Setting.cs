using UnityEngine;
using System.Collections;

namespace Games
{
	public class Setting 
	{
		public static AppSettingConfig 			app 		= new AppSettingConfig();
		public static VersionSettingConfig 		version 	= new VersionSettingConfig();
		public static UrlSettingConfig 			url 		= new UrlSettingConfig();
	}


	[System.Serializable]
	public class AppSettingConfig
	{
		/** 应用程序名称 */
		public string    appName = "ihaiu";  

		/** 应用程序前缀 */
		public string    appPrefix = "ihaiu_";
	}

	[System.Serializable]
	public class VersionSettingConfig
	{
		/** 运行模式*/
		public enum RunModel
		{
			/** 开发 */
			Develop			= 0,
			/** 测试资源版本 */
			TestVersion		= 1,
			/** 发布 */
			Release 		= 2
		}

		/** 更新时--是否可以修改运营商 */
		public bool updateEnableChangeCenterName = false;

		/** 运营商 */
		public string 		centerName 		= "Official";
		/** 语言 */
		public string 		language   		= "ZH_CN";
		/** 版本号 */
		public string 		ver 			= "0.0.0";
		/** 运行模式 */
		public RunModel 	model			= RunModel.Develop;
	}

	[System.Serializable]
	public class UrlSettingConfig
	{
		public bool WebUrlIsDevelop
		{
			get
			{
				if(PlayerPrefsUtil.HasKey(PlayerPrefsKey.Test_WebUrl_IsDevelop, false))
				{
					return PlayerPrefsUtil.GetBool(PlayerPrefsKey.Test_WebUrl_IsDevelop, false);
				}
				return false;
			}

			set
			{
				PlayerPrefsUtil.SetBool(PlayerPrefsKey.Test_WebUrl_IsDevelop, value, false);
			}
		}

		/** 服务器版本文件 */
		public string    WebUrl
		{
			get
			{
				if (WebUrlIsDevelop)
				{
					return WebUrl_Develop;
				}
				return WebUrl_Release;
			}
		}

		/** 服务器版本文件--发布 */
		public string 	WebUrl_Release  = "http://www.ihaiu.com/";
		/** 服务器版本文件--调试 */
		public string   WebUrl_Develop  = "http://localhost/";


	}
}

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Games;

namespace com.ihaiu
{
	public class CenterSwitcherWindow : EditorWindow
	{

	    public CenterSwitcher centerSwitcher = new CenterSwitcher();
	    void OnGUI() 
	    {
	        centerSwitcher.OnGUI();
	    }

	    void OnFocus()
	    {
	        centerSwitcher.OnFocus();
	    }

	}


	public class CenterSwitcher
	{

		public class CenterItem{
			public string name;
			public int typeID;
			public string bundleId;
			public string pathName;
			public string productName;
			public string[] androidFileList;
			public string[] iosFileList;
			public string[]	platformCommonFileList;

	        public GitId    gitId;
	        public bool     gitToggle = true;
	        public string   gitTag;
	        public int      gitTagMaxIndex;
	        public string   gitTagUse;
	        public VersionInfo versionInfo;

	        public string gitTagLast
	        {
	            get
	            {
	                return gitTagMaxIndex > 0 ? gitTag + "-" + gitTagMaxIndex : gitTag;
	            }
	        }

			public CenterItem(GitId gitId, string name, string bundleId, int typeID, string pathName, string productName, 
				string[] androidFileList = null, string[] iosFileList = null, string[] platformCommonFileList = null){
				this.name = name;
				this.typeID = typeID;
				this.bundleId = bundleId;
				this.pathName = pathName;
				this.productName = productName;
				this.androidFileList = androidFileList;
				this.iosFileList = iosFileList;
				this.platformCommonFileList = platformCommonFileList;
			}
		}


		
	    public static CenterItem[] centerItemList = {
			
			new CenterItem(GitId.CN, "Official", "com.mb.kcj.official", 1, "Official", "小小空城计", androidFileList:new string[]{
	            "bin/nocenter.jar",
	            "AndroidManifest.xml",
			}),

	        new CenterItem(GitId.CN, "M4399", "com.mb.kcj.m4399", 2, "4399", "小小空城计", androidFileList:new string[]{
				"res",
				"libs",
				"bin/4399.jar",
				"AndroidManifest.xml"
			}),

	        new CenterItem(GitId.CN, "Xiaomi", "com.mb.kcj.mi", 3, "Mi", "小小空城计", androidFileList:new string[]{
	//			"res",
				"libs",
				"bin/mi.jar",
				"AndroidManifest.xml",
				"assets",
			}),

	        new CenterItem(GitId.CN, "Guopan", "com.mb.sgcr.guopan", 4, "guopanSDK", "小小空城计", androidFileList:new string[]{
	           // "res",
	            "libs",
	            "bin/guopan.jar",
	            "AndroidManifest.xml",
	            "assets"
	        }),

	        new CenterItem(GitId.CN, "Facebook", "com.gamelune.kcj", 5, "facebook", "小小空城计", androidFileList:new string[]{
				"AppsFlyer.cs",
				"NativeOfficialUnityWrap.cs",
				"AndroidManifest.xml",
				"AF-Android-SDK.jar",
				"AppsFlyerAndroidPlugin.jar"
			}),

	        new CenterItem(GitId.CN, "Shouyougu", "com.mb.kcj.syg", 6, "shouyougu/shouyougu", "小小空城计", androidFileList:new string[]{
				"AndroidManifest.xml",
				"bin/shouyougu.jar",
				"libs",
				"res/anim",
				"res/color",
				"res/drawable/data.bin",
				"res/drawable-hdpi",
				"res/drawable-ldpi",
				"res/drawable-mdpi",
				"res/drawable-xhdpi",
				"res/layout",
				"res/values",
				"assets"
			}),

			new CenterItem(GitId.CN, "OfficialS0", "com.mb.kcj.test", 7, "OfficialS0", "小小空城计", new string[]{
				"bin/nocenter.jar",
	            "AndroidManifest.xml",
			}),

	        new CenterItem(GitId.CN, "Uc", "com.mb.kcj.uc", 8, "uc", "小小空城计", new string[]{
				"AndroidManifest.xml",
				"assets",
				"libs",
				"LitJson",
				"NativeOfficialUnityWrap.cs",
				"UC"
			}),

	        new CenterItem(GitId.CN, "Qihu360", "com.mb.kcj.qihoo", 9, "360", "小小空城计", new string[]{
	            "res",
	            "libs",
	            "bin/qihu360.jar",
	            "AndroidManifest.xml",
	            "assets",
	        }),

	        new CenterItem(GitId.CN, "Huawei", "com.mb.kcj.huawei", 10, "huawei", "小小空城计", new string[]{
				"res",
				"libs",
				"bin/huawei.jar",
				"AndroidManifest.xml",
			}),

	        new CenterItem(GitId.CN, "Yyb", "com.tencent.tmgp.kcjyyb", 11, "yyb/yyb", "小小空城计", new string[]{
				"libs",
				"res",
				"bin/yyb.jar",
				"AndroidManifest.xml",
				"assets",
			}),

	        new CenterItem(GitId.CN, "Ucube", "com.ucube.kirin.kcg", 12, "ucube", "小小空城计", new string[]{
				"libs",
				"res",
				"bin/ucube.jar",
				"spec/AndroidManifest.xml",
				"NativeOfficialUnityWrap.cs",
				"Facebook.Unity.Android.dll",
				"AFInAppEvents.cs",
				"AppsFlyer.cs",
				"AppsFlyerTrackerCallbacks.cs",
				"assets",
			},
			null,
			new string[]{
				"platformCommon",
			}),

	        new CenterItem(GitId.CN, "Quick", "com.mb.com.quick", 13, "quick", "小小空城计", new string[]{
	            "libs",
	            "res",
	            "assets",
	            "AndroidManifest.xml",
	            "bin/quick.jar",
	        }),

			new CenterItem(GitId.CN, "Oppo", "com.mb.kcj.nearme.gamecenter", 14, "oppo", "小小空城计", new string[]{
				"libs",
				"res",
				"assets",
				"AndroidManifest.xml",
				"bin/oppo.jar",
			}),

			new CenterItem(GitId.CN, "Lyw", "com.mb.kcj.ly", 15, "lyw", "小小空城计", new string[]{
				"libs",
				"res",
				"assets",
				"AndroidManifest.xml",
				"bin/lyw.jar",
			})
	    };

	    private CenterItem defaultItem = new CenterItem(GitId.CN, "Default", "", -1, "Default", "", new string[]{
			"libs"
		},
		new string[]{
			"framework"
		});

	    private CenterItem commonItem = new CenterItem(GitId.CN, "Common", "", -1, "Common", "", new string[]{
	        "libs",
	        "bin/common.jar"
		});

		private int _centerIndex = 0;
		private SettingConfig _settingConfig;

	    public bool foldout = false;

		public void OnGUI() 
		{

	        GUILayout.BeginVertical(HGUILayout.boxMPStyle);
			EditorGUILayout.Space();
	        GUILayout.BeginHorizontal();
	        EditorGUILayout.LabelField("选择发行商", HGUILayout.labelCenterStyle, GUILayout.Width(150));

			string[] list = new string[centerItemList.Length];
			for (int i=0; i<list.Length; i++)
			{
				list[i] = centerItemList[i].name;
			}
			_centerIndex = EditorGUILayout.Popup(_centerIndex, list);

	        GUILayout.EndHorizontal();

	        GUILayout.Space(20);

	        HGUILayout.BeginCenterHorizontal();


	        if (GUILayout.Button("切换发行商", GUILayout.MinHeight(30), GUILayout.MaxWidth(200)))
			{
				DoSwitch(_centerIndex);
	        }
	        HGUILayout.EndCenterHorizontal();

	        GUILayout.EndVertical();
		}

		public void DoSwitch(int idx)
		{
//			if(Directory.Exists("Assets/Plugins/Android")) Directory.Delete("Assets/Plugins/Android", true);
//			Directory.CreateDirectory("Assets/Plugins/Android");
//
//			if(Directory.Exists("Assets/Plugins/iOS")) Directory.Delete("Assets/Plugins/iOS", true);
//			Directory.CreateDirectory("Assets/Plugins/iOS");
//
//			if(Directory.Exists("Assets/Plugins/PlatformCommon")) Directory.Delete("Assets/Plugins/PlatformCommon", true);
//			Directory.CreateDirectory("Assets/Plugins/PlatformCommon");

			var centerItem = centerItemList[idx];
//			CopyCenterFile(centerItem);
//			CopyCenterFile(defaultItem);
//			CopyCenterFile(commonItem);
//			CopyIconSplash(centerItem);

			PlayerSettings.bundleIdentifier = centerItem.bundleId;
			PlayerSettings.productName = centerItem.productName;
			SettingConfig SettingConfig = SettingConfig.Load();
			SettingConfig.version.centerName = centerItem.name;
			SettingConfig.Save();
		}

		public static void CopyDir(string fromDir, List<string> allFileList)
		{
			if (!Directory.Exists(fromDir))
				return;

			string[] files = Directory.GetFiles(fromDir);
			foreach (string fromFileName in files)
			{
				if (!fromFileName.Contains(".DS_Store") && !fromFileName.Contains("classes"))
				{
					allFileList.Add(fromFileName);
				}
			}
			string[] fromDirs = Directory.GetDirectories(fromDir);
			foreach (string fromDirName in fromDirs)
			{
				CopyDir(fromDirName, allFileList);
			}
		}

		void FileList(string fileList, string pathName, List<string> outputFileList)
		{
			var dict = new DirectoryInfo("CenterProj/" + pathName);

			if (fileList != null)
			{
				for (int k=0; k<fileList.Length; k++)
				{
					var path = dict.FullName + "/" + fileList[k];
					if (Directory.Exists(path))
					{
						CopyDir(path, outputFileList);
					}
					else
					{
						outputFileList.Add(path);
					}
				}
			}
		}

		void CopyCenterFile(string[] fileList, string pathName, string targetPath)
		{
			var allFileList = new List<string>();
			var dict = new DirectoryInfo("CenterProj/" + pathName);

			for (int k=0; k<fileList.Length; k++)
			{
				var path = dict.FullName + "/" + fileList[k];
				if (Directory.Exists(path))
				{
					CopyDir(path, allFileList);
				}
				else
				{
					allFileList.Add(path);
				}
			}

			foreach(string str in allFileList)
			{
				FileInfo f = new FileInfo(str);
				var pluginDir = new DirectoryInfo(targetPath);
				var centerProjPath = f.DirectoryName.Replace("\\", "/");
				var split = centerProjPath.Split(new string[]{"CenterProj/" + pathName + "/"}, System.StringSplitOptions.None);
				if (split.Length > 1)
				{
					var dest = split[1];
					//将bin或者spe（在android中一般是jar文件和AndroidManifest文件）复制到根目录下，而不是bin/或者spec/下
					if (dest == "bin" || dest == "spec")
					{
						File.Copy(str, pluginDir.FullName + f.Name);
					}
					else
					{
						var destsub = pluginDir.FullName + dest;
						if (!Directory.Exists(destsub))
						{
							Directory.CreateDirectory(destsub);
						}
						File.Copy(str, destsub + "/" + f.Name);
					}
				}
				else
				{
					File.Copy(str, pluginDir.FullName + f.Name);
				}
			}
		}

		void CopyCenterFile(CenterItem centerItem)
		{
			if (centerItem.androidFileList != null)
			{
				CopyCenterFile(centerItem.androidFileList, centerItem.pathName, "Assets/Plugins/Android/");
			}
			if (centerItem.iosFileList != null)
			{
				CopyCenterFile(centerItem.iosFileList, centerItem.pathName, "Assets/Plugins/iOS/");
			}
			if (centerItem.platformCommonFileList != null)
			{
				CopyCenterFile(centerItem.platformCommonFileList, centerItem.pathName, "Assets/Plugins/PlatformCommon/");
			}
		}

		void CopyIconSplash(CenterItem centerItem)
		{
			var dict = new DirectoryInfo("CenterProj/" + centerItem.pathName);
			var settingPath = "Assets/Game/PlayerSettings";
			var filePath = dict.FullName + "/icon.png";
			if (File.Exists(filePath))
			{
				File.Copy(filePath, settingPath + "/icon.png", true);
			}
			filePath = dict.FullName + "/splash.jpg";
			if (File.Exists(filePath))
			{
				File.Copy(filePath, settingPath + "/splash.jpg", true);
			}
		}

		public void OnFocus()
		{

			_settingConfig = SettingConfig.Load();

			foreach (CenterItem item in centerItemList)
			{
				if (item.name == _settingConfig.version.centerName)
				{
					_centerIndex = item.typeID-1;
					break;
				}
			}
		}
	}
}
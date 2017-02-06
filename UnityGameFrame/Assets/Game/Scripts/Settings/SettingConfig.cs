using UnityEngine;
using System.Collections;
using System.IO;
using com.ihaiu;


namespace Games
{
	[System.Serializable]
	public class SettingConfig 
	{
		[SerializeField]
		public AppSettingConfig 			app 		= new AppSettingConfig();
		[SerializeField]
		public VersionSettingConfig 		version 	= new VersionSettingConfig();
		[SerializeField]
		public UrlSettingConfig 			url 		= new UrlSettingConfig();
		[SerializeField]
		public AssetSettingConfig 			asset 		= new AssetSettingConfig();

		public void Set()
		{
			Setting.app 			= app;
			Setting.version 		= version;
			Setting.url 			= url;
			asset.Set();


			#if UNITY_EDITOR
			AssetManagerSetting.TestVersionMode               = version.model == VersionSettingConfig.RunModel.TestVersion;
			AssetManagerSetting.EditorSimulateConfig          = version.model == VersionSettingConfig.RunModel.Develop;
			AssetManagerSetting.EditorSimulateAssetBundle     = version.model == VersionSettingConfig.RunModel.Develop;
			#endif
		}


		public void Save(string path)
		{
			string dir = Path.GetDirectoryName(path);
			if(!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			
			string json = JsonUtility.ToJson(this, true);
			File.WriteAllText(path, json);
		}

		public static SettingConfig Load(string path)
		{
			if(!File.Exists(path))
			{
				return new SettingConfig();
			}
			string json = File.ReadAllText(path);
			return JsonUtility.FromJson<SettingConfig>(json);
		}


		#if UNITY_EDITOR
		public static SettingConfig last;
		public static SettingConfig Load()
		{
			string path = AssetManagerSetting.StreamingFilePath.SettingConfig;
			return last = Load(path);
		}


		public void Save()
		{
			last = this;
			string path = AssetManagerSetting.StreamingFilePath.SettingConfig;
			Save(path);
			UnityEditor.AssetDatabase.Refresh();
			Debug.Log("[SettingConfig]" + path);
		}
		#endif


	}
}

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;
using com.ihaiu;

namespace Games
{
	public class GameMenuEditor
	{
		[MenuItem("Game/Generate SettingConfig.json")]
		public static void GenerateSettingConfig()
		{
			SettingConfig config = SettingConfig.Load();
			config.Save();
		}


		[MenuItem("Game/Tool/xlsx -> csv")]
		public static void GenerateCSV()
		{
			GenerateConfig.Generate();
		}


		[MenuItem("Game/Tool/Generate ConfigManager_List.cs")]
		public static void GenerateConfigManager()
		{
			ConfigManagerEditor.Generate();
			AssetDatabase.Refresh();
		}

		[MenuItem("Game/Tool/Generate ModuleManager_List.cs")]
		public static void GenerateModuleManager()
		{
			ModuleManagerEditor.Generate();
			AssetDatabase.Refresh();
		}
	}

}
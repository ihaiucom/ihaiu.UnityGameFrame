using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;

namespace Games
{
	public class GameMenuEditor
	{
		[MenuItem("Game/Tool/xlsx -> csv")]
		public static void GenerateCSV()
		{
			GenerateConfig.Generate();
		}


		[MenuItem("Game/Tool/Generate ConfigManager_List.cs")]
		public static void GenerateConfigManager()
		{
			ConfigManagerEditor.GenerateConfigManager();
		}
	}

}
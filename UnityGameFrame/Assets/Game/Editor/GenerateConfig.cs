using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;
using com.ihaiu;
using System.IO;
using System.Linq;

public class GenerateConfig
{
	public static void Generate()
	{
		ProcessStartInfo start = new ProcessStartInfo("sh");
		start.Arguments = Application.dataPath + "/../../svn/config/out_game.sh";
		start.CreateNoWindow = false;
		start.ErrorDialog = true;
		start.UseShellExecute = true;
		start.RedirectStandardOutput = false;
		start.RedirectStandardError = false;
		start.RedirectStandardInput = false;

		Process p = Process.Start(start);
		p.WaitForExit();
		p.Close();

		RemoveTmp();

		AssetDatabase.Refresh();
	}

	public static void RemoveTmp()
	{
		string dir = AssetManagerSetting.EditorRoot.Config;
		if(Directory.Exists(dir))
		{
			string[] files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
				.Where(s => Path.GetFileName(s).StartsWith("~$")).ToArray();


			for(int i = 0; i < files.Length; i ++)
			{
				File.Delete(files[i]);
			}
		}
	}
}

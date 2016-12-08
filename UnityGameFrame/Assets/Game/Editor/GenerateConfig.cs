using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Diagnostics;

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
	}
}

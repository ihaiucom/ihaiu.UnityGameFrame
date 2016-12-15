using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.ihaiu;

namespace Games
{
	public partial class ConfigManager 
	{
		public IEnumerator Load()
		{
			int count = readerList.Count;
			for(int i = 0; i < count; i ++)
			{
				readerList[i].Load();
				yield return 0;
			}

			OnGameConfigLoaded();
		}

		public IEnumerator Reload()
		{
			int count = readerList.Count;
			for(int i = 0; i < count; i ++)
			{
				readerList[i].Reload();
				yield return 0;
			}

			OnGameConfigLoaded();
		}

		public void OnGameConfigLoaded()
		{
			int count = readerList.Count;
			for(int i = 0; i < count; i ++)
			{
				readerList[i].OnGameConfigLoaded();
			}
		}
	}
}
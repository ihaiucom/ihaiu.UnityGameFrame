using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace Games
{
	public class MenuCtlForScene : MenuCtl 
    {

		public string sceneName
		{
			get
			{
				return config.path;
			}
		}

		override public void Destory()
		{

		}

		override public void OnDestory()
		{
		}


		override public void Open(bool isPreinstall = false)
		{
			Load();
		}

		override public void Close()
		{
			
		}



		override protected void OnLoadAssetsComplete()
		{
			Game.mainThread.StartCoroutine(LoadScene());
			InitScene();
		}

		protected IEnumerator LoadScene()
		{
			AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			async.allowSceneActivation = false;
			while(!async.isDone && async.progress < 0.8f)
			{
				yield return async;
			}

			async.allowSceneActivation = true;
		}

		virtual protected void InitScene()
		{
			
		}






    }
}

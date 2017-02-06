using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Games;


namespace com.ihaiu
{
    public class VersionManager : MonoBehaviour
    {
        public Action<string>       errorCallback;
        public Action<string>       stateCallback;

        public Action<string>       updateFailedCallback;
        public Action<float>        updateProgressCallback;

        public Action               updateEnterCallback;
        public Action               updateEndCallback;


		public Action               serverCloseCallback;
        public Action<string>       needDownAppCallback;
        public Action               needHostUpdateCallback;


        public bool yieldbreak = false;
        public VersionCheckState state = VersionCheckState.Normal;
        private Version appVer = new Version();
        private Version curVer = new Version();
        private Version serverVer = new Version();

        public VersionInfo serverVersionInfo;

		private SettingConfig appSettingConfig;
		private SettingConfig curSettingConfig;


        #region Event
        void OnError(string txt)
        {
            if (errorCallback != null)
                errorCallback(txt);
        }

        void OnState(string txt)
        {
            if (stateCallback != null)
                stateCallback(txt);
        }

        void OnUpdateEnter()
        {
            if (updateEnterCallback != null)
                updateEnterCallback();
        }

        void OnUpdateEnd()
        {
            if (updateEndCallback != null)
                updateEndCallback();
        }

        void OnUpdateFailed(string url)
        {
            if (updateFailedCallback != null)
                updateFailedCallback(url);
        }

        void OnUpdateProgress(float progress)
        {
            if (updateProgressCallback != null)
                updateProgressCallback(progress);
        }

		void OnServerClose()
		{
			if (serverCloseCallback != null)
				serverCloseCallback();
		}

        void OnNeedDownApp(string url)
        {
            if (needDownAppCallback != null)
                needDownAppCallback(url);
        }


        void OnNeedHostUpdate()
        {
            if (needHostUpdateCallback != null)
                needHostUpdateCallback();
        }


        private bool IsContinueHostUpdate = false;
        public void SetContinueHostUpdate()
        {
            IsContinueHostUpdate = true;
        }
        #endregion

        public IEnumerator CheckFirst()
        {
            yield return StartCoroutine(ReadGameConst_Streaming());

            #if UNITY_EDITOR
            appSettingConfig.Set();

			if (appSettingConfig.version.model == VersionSettingConfig.RunModel.Develop)
            {
                yield break;
            }

            if(!AssetManagerSetting.TestVersionMode)
            {
                appSettingConfig.Set();
                yield break;
            }
            #endif

			appVer.Parse(appSettingConfig.version.ver);
			VersionLocalInfo.Install.streamVersion.Parse(appSettingConfig.version.ver);
            if (VersionLocalInfo.Install.IsResetAppRes)
            {
                appSettingConfig.Set();
                curVer.Copy(appVer);
                yield return StartCoroutine(InitData());
            }
            else
            {
                yield return StartCoroutine(ReadGameConst_Persistent());

                curSettingConfig.Set();
				curVer.Parse(curSettingConfig.version.ver);
            }

            if (VersionLocalInfo.Install.IsNewApp)
            {
				VersionLocalInfo.Install.localVersion.Parse(appSettingConfig.version.ver);
                VersionLocalInfo.Install.Save();
            }



//            yield return StartCoroutine(ReadGameConst_Persistent());
//
//            appVer.Parse(appSettingConfig.Version);
//
//            bool needInitData = false;
//            if (curSettingConfig == null)
//            {
//                appSettingConfig.Set();
//                needInitData = true;
//            }
//            else
//            {
//                curSettingConfig.Set();
//                curVer.Parse(curSettingConfig.Version);
//                needInitData = VersionCheck.CheckNeedCopy(curVer, appVer);
//            }
//
//
//
//            if (needInitData)
//            {
//                yield return StartCoroutine(InitData());
//                curVer.Copy(appVer);
//            }
//
        }


        public IEnumerator CheckVersion()
        {
            #if UNITY_EDITOR
			if (appSettingConfig.version.model == VersionSettingConfig.RunModel.Develop)
            {
                yield break;
            }

            if(!AssetManagerSetting.TestVersionMode)
            {
                yield break;
            }
            #endif



            yield return (ReadServerVersionInfo());
            Debug.Log("serverVersionInfo=" + serverVersionInfo);

            if (serverVersionInfo != null)
            {
				if (serverVersionInfo.isClose > 0)
				{
					OnServerClose();
					yieldbreak = true;
				}
				else
				{
	                serverVer.Parse(serverVersionInfo.version);

	                state = VersionCheck.CheckState(curVer, serverVer);

                    if (state != VersionCheckState.DownApp && serverVersionInfo.zipPanelStarShow)
                    {
                        VersionLocalInfo.Install.SetServerInfo(serverVersionInfo);
//                        yield return Coo.downloadZip.CheckExeCoroutine();
                    }

	                switch(state)
	                {
	                    case VersionCheckState.DownApp:
	                        OnNeedDownApp(serverVersionInfo.downLoadUrl);
	                        yieldbreak = true;
	                        break;

	                    case VersionCheckState.HotUpdate:
	                        IsContinueHostUpdate = false;
	                        OnNeedHostUpdate();

	                        while (!IsContinueHostUpdate)
	                        {
	                            yield return new WaitForSeconds(0.25f);
	                        }

	                        yield return StartCoroutine(UpdateResource(serverVersionInfo.updateLoadUrl));
	                        yield return StartCoroutine(ReadGameConst_Persistent());
	                        curSettingConfig.Set();
	                        break;
	                    default:
	                        break;
	                }
				}

                if (!yieldbreak && !serverVersionInfo.zipPanelStarShow)
                {
                    VersionLocalInfo.Install.SetServerInfo(serverVersionInfo).CheckZipExe();
                }
            }
            else
            {
                Debug.Log("zj OnFinal");
            }
        }

        IEnumerator InitData()
        {
            Caching.CleanCache();
            yield return InitAssetList();

            List<string> list = new List<string>();
            list.Add(AssetManagerSetting.FileName.SettingConfig);



            string infile = "";
            string outfile = "";
            int count = list.Count;
            for (int i = 0; i < count; i ++)
            {
                string file = list[i];
               

                file = string.Format(file, Platform.PlatformDirectory);
                infile = AssetManagerSetting.RootPathStreaming + file; 
                outfile = AssetManagerSetting.RootPathPersistent + file;

                AssetManagerSetting.persistentAssetFileList.Add(file, "");

                PathUtil.CheckPath(outfile);

                if (Application.platform == RuntimePlatform.Android)
                {
                    WWW www = new WWW(infile);
                    yield return www;

                    if (www.isDone)
                    {
                        File.WriteAllBytes(outfile, www.bytes);
                    }

                    www.Dispose();
                    www = null;
                    yield return 0;
                }
                else
                {
                    File.Copy(infile, outfile, true);
                }

                yield return new WaitForEndOfFrame();
            }




            AssetManagerSetting.persistentAssetFileList.Save();

        }

        IEnumerator InitAssetList()
        {
            string url = AssetManagerSetting.StreamingFileURL.AssetListApp;
            WWW www = new WWW(url);
            yield return www;


            if(string.IsNullOrEmpty(www.error))
            {
                AssetFileList appAssetFileList = AssetFileList.Deserialize(www.text);
                AssetFile item;
                AssetFile verItem;
                for(int i = 0; i < appAssetFileList.list.Count; i ++)
                {
                    item = appAssetFileList.list[i];
                    verItem = AssetManagerSetting.versionAssetFileList.Get(item.path);
                    if (verItem == null || verItem.IsEnableCover(appVer))
                    {
                        AssetManagerSetting.versionAssetFileList.Add(item).SetVer(appVer);
                        AssetManagerSetting.persistentAssetFileList.Remove(AssetManagerSetting.GetPlatformPath(item.path));
                    }
                }
            }

            AssetManagerSetting.versionAssetFileList.Save();
        }


        /** 获取服务器版本号 */
        IEnumerator ReadServerVersionInfo()
        {

            int isUpdateTest = PlayerPrefsUtil.GetIntSimple(PlayerPrefsKey.Setting_Update);//测试更新
			string centerName = Setting.version.centerName;



			string url = AssetManagerSetting.ServerURL.GetServerVersionInfoURL(Setting.url.WebUrl, centerName, isUpdateTest == 1) ;
            Debug.Log("VersionURL=" + url);
            WWW www = new WWW(url);
            yield return www;
            if(!string.IsNullOrEmpty(www.error))
            {
                OnError("获取服务器版本号出错");
                Debug.LogErrorFormat("获取服务器版本号出错 url={0}, www.error={1}", url, www.error);
                www.Dispose();
                www = null;

                yield break;
            }

            Debug.Log(www.text);

            serverVersionInfo = JsonUtility.FromJson<VersionInfo>(www.text);
            serverVersionInfo.Set();
        }


        /** 读取streaming下只读配置  */
        IEnumerator ReadGameConst_Streaming()
        {   
            OnState("读取streaming SettingConfig.json");
            string url = AssetManagerSetting.StreamingFileURL.SettingConfig;
            WWW www = new WWW(url);
            yield return www;
            if(string.IsNullOrEmpty(www.error))
            {
                Debug.Log(url);
                Debug.Log(www.text);

				appSettingConfig = JsonUtility.FromJson<SettingConfig>(www.text);
            }
            else
            {       
				OnError("读取Streaming下SettingConfig.json失败");
				Debug.LogErrorFormat("读取SettingConfig.json失败 ReadGameConst_Streaming url={0} error={1}", url, www.error);
            }

            www.Dispose();
            www = null;
        }


        IEnumerator ReadGameConst_Persistent()
        {   
			OnState("读取persistent SettingConfig.json");
            string url = AssetManagerSetting.PersistentFileURL.SettingConfig;
            WWW www = new WWW(url);
            yield return www;
            if(string.IsNullOrEmpty(www.error))
            {
                Debug.Log(url);
                Debug.Log(www.text);

				curSettingConfig = JsonUtility.FromJson<SettingConfig>(www.text);
            }
            else
            {       
                Debug.LogFormat("读取SettingConfig.json失败 ReadGameConst_Persistent url={0} error={1}", url, www.error);
            }

            www.Dispose();
            www = null;
        }


        IEnumerator UpdateResource(string rootUrl)
        {
            // rootUrl = "http://www.ihaiu.com/StreamingAssets/"
            OnUpdateEnter();

            //获取服务器端的file.csv

            OnState("获取服务器端的file.csv");
            string updateAssetListUrl = AssetManagerSetting.ServerURL.GetUpdateCsvURL(rootUrl);
            Debug.Log("UpdateAssetList URL: " + updateAssetListUrl);
            WWW www = new WWW(updateAssetListUrl);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                OnError("更新资源读取资源列表失败");
                Debug.LogErrorFormat("更新资源读取资源列表失败 updateAssetListUrl={0}, www.error={1}", updateAssetListUrl,  www.error);
                www.Dispose();
                www = null;
                yield break;
            }

            AssetFileList updateAssetList = AssetFileList.Deserialize(www.text);
            www.Dispose();
            www = null;
            //Debug.Log("count: " + files.Length + " text: " + filesText);



            List<AssetFile> diffs = AssetFileList.Diff(AssetManagerSetting.versionAssetFileList, updateAssetList);


            string path;
            //更新
            int count = diffs.Count;
            for (int i = 0; i < count; i++)
            {
                AssetFile item = diffs[i];
                path = AssetManagerSetting.GetPlatformPath(item.path);

                OnState("更新" + path);
                OnUpdateProgress((float)(i + 1) / (float)count);

                string url = rootUrl + path;
                www = new WWW(url);

                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    OnUpdateFailed(url);

                    www.Dispose();
                    www = null;
                    continue;
                }

                string localPath = AssetManagerSetting.RootPathPersistent + path;
                PathUtil.CheckPath(localPath, true);
                File.WriteAllBytes(localPath, www.bytes);

                www.Dispose();
                www = null;

                AssetManagerSetting.versionAssetFileList.Add(item).SetVer(serverVer);

                AssetManagerSetting.persistentAssetFileList.Add(path, item.md5);
            }
            yield return new WaitForEndOfFrame();



            www = new WWW(AssetManagerSetting.PersistentFileURL.SettingConfig);
            yield return www;
            if(string.IsNullOrEmpty(www.error))
            {
                Debug.Log(AssetManagerSetting.PersistentFileURL.SettingConfig);
                Debug.Log(www.text);

                bool isSave = false;
				SettingConfig SettingConfig = JsonUtility.FromJson<SettingConfig>(www.text);
				if (!SettingConfig.version.updateEnableChangeCenterName)
                {
                    if (curSettingConfig != null)
                    {
						SettingConfig.version.centerName = curSettingConfig.version.centerName;
                    }
                    else if(appSettingConfig != null)
                    {
						SettingConfig.version.centerName = appSettingConfig.version.centerName;
                    }
                    isSave = true;
                }

                if (serverVersionInfo.isChangeGameConstVersion)
                {
					SettingConfig.version.ver = serverVersionInfo.version;
                    isSave = true;
                }

                if (isSave)
                {
                    SettingConfig.Save(AssetManagerSetting.RootPathPersistent + AssetManagerSetting.FileName.SettingConfig);
                }
            }
            else
            {
                Debug.LogFormat("2 读取SettingConfig.json失败 Persistent url={0} error={1}", AssetManagerSetting.PersistentFileURL.SettingConfig, www.error);
            }

            www.Dispose();
            www = null;





            AssetManagerSetting.versionAssetFileList.Save();
            AssetManagerSetting.persistentAssetFileList.Save();


            // 更新完成
            OnUpdateEnd();
        }
    }
}
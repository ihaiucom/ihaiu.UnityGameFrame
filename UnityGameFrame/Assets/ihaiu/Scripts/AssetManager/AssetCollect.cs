using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace com.ihaiu
{
    /// <summary>
    /// 资源加载情况收集
    /// </summary>
    [System.Serializable]
    public class AssetCollect 
    {
        #region internal
        public Dictionary<string, AssetCollectInternalItem> internalDict = new Dictionary<string, AssetCollectInternalItem>();
        [SerializeField]
        public List<AssetCollectInternalItem>               internalList = new List<AssetCollectInternalItem>();

        public void OnLoadInternal(string assetBundleName)
        {
            if (!AssetManagerSetting.IsCollect)
                return;
            
            if (internalDict.ContainsKey(assetBundleName))
            {
                internalDict[assetBundleName].loadNum++;
            }
            else
            {
                AssetCollectInternalItem item = new AssetCollectInternalItem();
                item.url = assetBundleName;
                item.loadNum = 1;
                internalDict.Add(item.url, item);
                internalList.Add(item);
            }
        }

        public void OnUnloadInteral(string assetBundleName)
        {
            if (!AssetManagerSetting.IsCollect)
                return;

            
            if (internalDict.ContainsKey(assetBundleName))
            {
                internalDict[assetBundleName].unloadNum++;
            }
            else
            {
                AssetCollectInternalItem item = new AssetCollectInternalItem();
                item.url = assetBundleName;
                item.unloadNum = 1;
                internalDict.Add(item.url, item);
                internalList.Add(item);
            }
        }

        public void InternalListToDict()
        {
            for(int i = 0; i < internalList.Count; i ++)
            {
                if (!internalDict.ContainsKey(internalList[i].url))
                {
                    internalDict.Add(internalList[i].url, internalList[i]);
                }
            }
        }
        #endregion



        public void Save(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                #if UNITY_EDITOR
                path = AssetManagerSetting.EditorRoot.WorkspaceAssetCollect + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")  + ".json";
                #else
                path = AssetManagerSetting.RootPathPersistent + "AssetCollect.json";
                #endif
            }

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(path, json);
        }


        public static AssetCollect Load(string path = null)
        {

            if (string.IsNullOrEmpty(path))
            {
                path = AssetManagerSetting.RootPathPersistent + "AssetCollect.json";
            }

            if (!File.Exists(path))
            {
                return null;
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<AssetCollect>(json);
        }


    }

    [System.Serializable]
    public class AssetCollectInternalItem
    {
        [SerializeField]
        public string   url;

        [SerializeField]
        public int      loadNum;

        [SerializeField]
        public int      unloadNum;
    }
}
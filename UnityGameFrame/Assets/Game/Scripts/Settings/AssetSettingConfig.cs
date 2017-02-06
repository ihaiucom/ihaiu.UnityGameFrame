using UnityEngine;
using System.Collections;
using com.ihaiu;

namespace Games
{
	[System.Serializable]
	public class AssetSettingConfig
	{

		/** 强制异步加载,等待一帧(Resource.AsyLoad) */
		public bool ForcedResourceAsynLoadWaitFrame 	= true;
		/** 是否缓存Resouces加载的对象 */
		public bool IsCacheResourceAsset 				= false;
		/** 是否缓存AssetBundle加载的对象 */
		public bool IsCacheAssetBundleAsset 			= false;
		/** 检测缓存时间间隔（秒） */
		public float CheckCacheAssetRate 				= 10;
		/** 缓存时间（秒） */
		public float CacheAssetTime 					= 60;
		/** 加载超时(秒) */
		public int LoadTimeOut           				= 20;
		
		public void Set ()
		{
			AssetManagerSetting.ForcedResourceAsynLoadWaitFrame     = ForcedResourceAsynLoadWaitFrame;
			AssetManagerSetting.IsCacheResourceAsset                = IsCacheResourceAsset;
			AssetManagerSetting.IsCacheAssetBundleAsset             = IsCacheAssetBundleAsset;
			AssetManagerSetting.CheckCacheAssetRate                 = CheckCacheAssetRate;
			AssetManagerSetting.CacheAssetTime                      = CacheAssetTime;
			AssetManagerSetting.UseCacheAssetTime                   = CacheAssetTime > 0;
			AssetManagerSetting.LoadTimeOut 						= LoadTimeOut;
		}
	}
}

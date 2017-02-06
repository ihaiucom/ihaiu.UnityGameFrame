using UnityEngine;
using System.Collections;
using com.ihaiu;
using System.Security.Cryptography;
using System.Text;

namespace Games
{
    public class GameLaunch : MonoBehaviour 
    {
		void Awake()
		{
			StartCoroutine(Install());
		}

		IEnumerator Install()
		{

			VersionManager versionManager = gameObject.AddComponent<VersionManager>();
			yield return versionManager.CheckFirst();

			// 初始化AssetManager，为了启动就能用它来加载资源
			Game.asset = gameObject.AddComponent<AssetManager>();
			yield return Game.asset.Initialize();
			Game.cricle.Show();

			yield return versionManager.CheckVersion();
			if(versionManager.yieldbreak)
				yield break;

			// 如果热更新过资源，重新加载AssetManager信息。manifest,AssetManagerSetting.LoadAssetListURL,AssetManagerSetting.DontUnloadAssetListURL
			if(versionManager.state == VersionCheckState.HotUpdate)
			{
				yield return Game.asset.Reinitialize();
			}

			// 初始化管理器
			yield return Game.Install(gameObject);

			// 读取Config AssetBundle
			#if UNITY_EDITOR
			if (Setting.version.model != VersionSettingConfig.RunModel.Develop)
			#endif
			{
				yield return InitConfig();
			}

			// 读取配置
			yield return Game.config.Load();
//			Game.cricle.Hide();
		}

		/** 读取Config AssetBundle */
		IEnumerator InitConfig()
		{
			string path = AssetManagerSetting.FileURL.AssetBundleConfig;
			WWW www = new WWW(path);
			yield return www;

			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.LogErrorFormat("[GameLaunch.InitConfig] path={0}, www.error={1}", path, www.error);
				yield break;
			}


			byte[] bytes = www.bytes;
			www.Dispose();
			www = null;

			bytes = DecryptBytes(bytes);

			AssetBundleCreateRequest assetBundleCreateRequest = LoadFromMemoryAsync(bytes);
			yield return assetBundleCreateRequest;
			AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;
			Game.asset.configAssetBundle = assetBundle;
		}

		AssetBundleCreateRequest LoadFromMemoryAsync(byte[] bytes)
		{
			#if UNITY_5_2
			return AssetBundle.CreateFromMemory(bytes);
			#else
			return AssetBundle.LoadFromMemoryAsync(bytes);
			#endif
		}

		/** 解密：故意设成私有方法，为了稍微安全点 */
		private byte[] DecryptBytes(byte[] data, string sKey = null)
		{
			if (sKey == null) sKey = "ihaiu";

			DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
			DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
			DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
			ICryptoTransform desencrypt = DES.CreateDecryptor();
			byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
			return result;
		}

    }
}
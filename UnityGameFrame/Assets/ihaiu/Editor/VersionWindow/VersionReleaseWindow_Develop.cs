using UnityEngine;
using System.Collections;
using UnityEditor;
using Games;


namespace com.ihaiu
{
    public partial class VersionReleaseWindow
    {

        /** 开发 */
        void OnGUI_Develop()
        {
            HGUILayout.BeginCenterHorizontal();
            if (GUILayout.Button("生成版本信息", GUILayout.MinHeight(50), GUILayout.MaxWidth(200)))
            {
				CenterSwitcher centerSwitcher = new CenterSwitcher();
				centerSwitcher.DoSwitch(0);
				
                if (currentDvancedSettingData.GetValue(DvancedSettingType.SettingConfig))
                {
                    SettingConfig config = SettingConfig.Load();
					config.version.model = VersionSettingConfig.RunModel.Develop;
                    config.Save();
                }


//                if (currentDvancedSettingData.GetValue(DvancedSettingType.Clear_AssetBundleName))
//                {
//                    AssetBundleEditor.ClearAssetBundleNames();
//                    AssetDatabase.RemoveUnusedAssetBundleNames();
//                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.Set_AssetBundleName))
                {
                    AssetBundleEditor.SetNames_Develop();
                }

                if (currentDvancedSettingData.GetValue(DvancedSettingType.GeneratorLoadAssetListCsv))
                {
                    AssetListCsvLoadMap.Generator();
                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.GenerateResZip))
                {
                    ResZipEditor.Install.Generator();
                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.CopyWorkspaceStreamToStreamingAssets_UnResZip))
                {
                    ResZipEditor.Install.CopyToStreaming_UnZip();
                }


                if (currentDvancedSettingData.GetValue(DvancedSettingType.CopyWorkspaceStreamToStreamingAssets_All))
                {
                    ResZipEditor.Install.CopyToStreaming_All();
                }

            }
            HGUILayout.EndCenterHorizontal();

        }


    }
}

using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Games
{
    public class GameLayer 
    {
        #region Unity Default Lock
        public const int Default            = 1 << 0;
        public const int TransparentFX      = 1 << 1;
        public const int IgnoreRaycast      = 1 << 2;
        public const int Layer3             = 1 << 3;
        public const int Water              = 1 << 4;
        public const int UI                 = 1 << 5;
        public const int Layer6             = 1 << 6;
        public const int Layer7             = 1 << 7;
        #endregion


        #region Custom War 
        public const int War_Terrain            = 1 << 8;
        public const int War_Unit               = 1 << 9;
        public const int War_Obstacle           = 1 << 10;
        #endregion





        #if UNITY_EDITOR
        public static int           customBeginIndex = 8;
        public static string[]      customLayers = {"War_Terrain", "War_Unit", "War_Obstacle"};


        [MenuItem("Edit/Game/SetEditorLayer")]
        public static void SetEditorTag ()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if(it.name == "layers")
                {
                    int end = Mathf.Min(customBeginIndex + customLayers.Length, it.arraySize);
                    for (int i = customBeginIndex; i < end; i++) 
                    {
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        dataPoint.stringValue = customLayers[i - customBeginIndex];
                    }

                    tagManager.ApplyModifiedProperties();
                    if (customBeginIndex + customLayers.Length > it.arraySize)
                    {
                        Debug.LogFormat("<color=red>请在Edit/Project Settings/Tags And Layers编辑器Layers列表添加 {0} 个空位</color>", customBeginIndex + customLayers.Length - it.arraySize);
                    }
                    break;
                }
            }
        }
        #endif
    }
}

using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Games
{
    public class GameTag 
    {
        #region Unity Default Lock
        public const string Untagged        = "Untagged";
        public const string Respawn         = "Respawn";
        public const string Finish          = "Finish";
        public const string EditorOnly      = "EditorOnly";
        public const string MainCamera      = "MainCamera";
        public const string Player          = "Player";
        public const string GameController  = "GameController";
        #endregion

        public const string UICamera            = "UICamera";
        public const string War_Terrain         = "War_Terrain";
        public const string War_Unit            = "War_Unit";











        #if UNITY_EDITOR
        public static int           customBeginIndex = 0;
        public static string[]      customTags = {UICamera, War_Terrain, War_Unit};


        [MenuItem("Edit/Game/SetEditorTag")]
        public static void SetEditorTag ()
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if(it.name == "tags")
                {
                    int end = Mathf.Min(customBeginIndex + customTags.Length, it.arraySize);
                    for (int i = customBeginIndex; i < end; i++) 
                    {
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        dataPoint.stringValue = customTags[i - customBeginIndex];
                    }

                    tagManager.ApplyModifiedProperties();
                    if (customBeginIndex + customTags.Length > it.arraySize)
                    {
                        Debug.LogFormat("<color=red>请在Edit/Project Settings/Tags And Layers编辑器Tags列表添加 {0} 个空位</color>", customBeginIndex + customTags.Length - it.arraySize);
                    }
                    break;
                }
            }
        }
        #endif
    }
}

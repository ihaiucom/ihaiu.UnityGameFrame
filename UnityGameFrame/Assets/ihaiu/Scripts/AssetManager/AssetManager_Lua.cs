//using UnityEngine;
//using System.Collections;
//using System;
//using System.Collections.Generic;
//using LuaInterface;
//using com.ihaiu.Utils;
//
//namespace com.ihaiu
//{
//    public partial class AssetManager 
//    {
//
//
//        /// <summary>
//        /// Lua加载
//        /// </summary>
//        /// <param name="table">模块Table.</param>
//        /// <param name="filename">文件名.</param>
//        /// <param name="callback">回调函数(文件名, 资源).</param>
//        public void LuaLoad(LuaTable table, string filename, LuaFunction callback)
//        {
//
//            if(string.IsNullOrEmpty(filename))
//            {
//                Debug.Log(string.Format("<color=red>AssetManager.LuaLoad filename={0}</color>", filename));
//            }
//
//            LuaCallback luaCB = new LuaCallback(table, callback);
//            Load(filename,  luaCB.AssetLoadCallback);
//        }
//
//
//
//    }
//}
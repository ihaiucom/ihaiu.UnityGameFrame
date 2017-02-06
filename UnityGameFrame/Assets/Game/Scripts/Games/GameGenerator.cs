﻿using UnityEngine;
using System.Collections;

namespace Games
{
    /// <summary>
    /// 全局构造器，每个场景里都有，所以每个场景都会初始化一遍，也会初始化游戏管理器一次
    /// 如果游戏管理器已经存在了，就跳过了，否则创建游戏管理器，来保证游戏里只有一个GameManager
    /// </summary>
    public class GameGenerator : MonoBehaviour 
    {
        void Awake() 
        {
            InitGameMangager();
        }

        /// <summary>
        /// 实例化游戏管理器
        /// </summary>
        public void InitGameMangager() 
        {
            string name = "GameManagers";
            GameObject manager = GameObject.Find(name);
            if (manager == null) 
            {
                manager = new GameObject(name);
                manager.name = name;
                manager.AddComponent<GameLaunch>();
            }

			GameObject.DontDestroyOnLoad(manager);
        }
    }
}
using UnityEngine;
using System.Collections;
using Games;
using System;


namespace com.ihaiu
{
    public class ConfigSetting 
    {


        public static void Load(string path, Action<string, object> call)
        {
            Game.asset.Load(path, call);
        }
    }
}

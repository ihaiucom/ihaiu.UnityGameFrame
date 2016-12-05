using UnityEngine;
using System.Collections;
using com.ihaiu;

namespace Games
{
    /** Game Facade */
    public class Game
    {
        #region gameframe
        public static AssetManager asset;
        public static int config;
        public static int menu;
        public static int pool;
        public static int proto;
        public static int audio;
        #endregion


        #region user
        public static int user;
        #endregion

        public static IEnumerator Install()
        {
            yield break;
        }
    }
}

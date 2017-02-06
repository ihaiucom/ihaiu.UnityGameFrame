using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Games;

namespace com.ihaiu
{
    [Serializable]
    public class NoticeInfo
    {
        public string qqGroupKey = "0POfj73j3PTMfn2w6XAeAHvVVa6fwvXK";
        public string qqNumber = "344021892";
        public string content = "<size=50>尊敬的主公:</size>\n加入核心玩家群344021892<color=#0000ffff><i>（点击加入）</i></color>, 加群即可领取礼包：100元宝+500银币。";
    }

    public class VersionInfo 
    {
        /** App版本 */
        public string version       = "0.0.0";
        /** Zip版本 */
        public string zipVersion    = "0.0.0";
        /** Zip MD5码 */
        public string zipMD5        = "";
        /** Zip 版本号检测前几位,默认2 */
        public int    zipCheckDigit = 2;
        /** Zip 加载面板是否一开始就显示 */
        public bool   zipPanelStarShow = false;
        /** Zip 文件网址 */
        public string zipLoadUrl        = "http://192.168.31.202/git/mbcr/GameCR/Workspace/Android/res.zip";

        public string downLoadUrl       = "https://beta.bugly.qq.com/t4cg";
        public string updateLoadUrl     = "http://112.126.75.68:8080/StreamingAssets/";

        public string noteAll = "服务器维护<color=red>预计13:00开服</color>";
        public string noteLite = "需要更新才可继续游戏，更新包大小为<color=red>2KB</color>，建议使用<color=red>Wi-Fi</color>网络下载";

		public int isClose = 0;
		public string noteClose = "关服公告";

        public NoticeInfo noticeBoard = new NoticeInfo();
        public string UploadVideo = "http://101.200.195.25:8000/";
        public int isShareOpen = 0;
        public string downloadShareLink = "http://qqpard.com/kcj_download?id=%s;%s";
        public string loginAddress = "120.92.132.240:2100";
        public string crashReportLink = "http://120.92.132.240:8080/file_upload";
         //public string appkeyIM = "pvxdm17jxmt1r";
        //public string appValueIm = "GIYRSSPXoVR";
        public string urlIm = "http://api.cn.ronghub.com/";
        public int dnum = 0;
        public bool isChangeGameConstVersion = true;

        #if UNITY_EDITOR
        public static VersionInfo Load(string path)
        {

            var f = new FileInfo(path);
            if (f.Exists)
            {
                var sr = f.OpenText();
                var str = sr.ReadToEnd();
                sr.Close();

                VersionInfo config = JsonUtility.FromJson<VersionInfo>(str);
                return config;
            }
            else
            {
                return new VersionInfo();
            }
        }


        public void Save(string filesPath)
        {
            string str = JsonUtility.ToJson(this, true);

            PathUtil.CheckPath(filesPath, true);
            if (File.Exists(filesPath)) File.Delete(filesPath);

            FileStream fs = new FileStream(filesPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(str);
            sw.Close(); fs.Close();
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log("[VersionInfoJsonGenerator]" + filesPath);
        }
        #endif

        public override string ToString()
        {
            return string.Format("[VersionInfo] version={0}, downLoadUrl={1}, updateLoadUrl={2}", version, downLoadUrl, updateLoadUrl);
        }

        public void Set()
        {
//            Debug.Log("config from server UploadVideo:" +  UploadVideo + " im:" + urlIm);
//            Debug.Log("config from server downloadShareLink:" + downloadShareLink + " loginAddress:" + loginAddress);
//            Debug.Log("config from server crashReportLink:" + crashReportLink);
//            GameConst.noticeBoard = noticeBoard;
//            GameConst.Host_Upload_Release = UploadVideo;
//            GameConst.isShareOpen = isShareOpen;
//            //GameConst.appkeyIM = appkeyIM;
//            //GameConst.appValueIm = appValueIm;
//            GameConst.urlIm = urlIm;
//            GameConst.downloadShareLink = downloadShareLink;
//            GameConst.loginAddress = loginAddress;
//            GameConst.crashReportLink = crashReportLink;
//
//            if (dnum > 0)
//                Coo.D(dnum);
        }
    }
}
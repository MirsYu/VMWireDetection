using Newtonsoft.Json;
using System;
using System.IO;
using VisionDesigner.GlobalInit;

namespace VMWireDetection
{
    public class Global
    {
        public static Form_Main _Main;

        public static string strProjectPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Project\\";

        public static UserCameraControl _Cam = new UserCameraControl();

        public static UserControl_Light _Light = new UserControl_Light();

        public static Form_RTU _RTU = new Form_RTU();

        public static Flow_Wire _Wire = new Flow_Wire();

        public static Form_WireParam _WireParam = new Form_WireParam();

        public static Flow_Casing _Casing = new Flow_Casing();

        public static Form_CasingParam _CasingParam = new Form_CasingParam();

        public static Form_LiveCam _LiveCam = new Form_LiveCam();

        public static CMVDGlobalInitTool tool = new CMVDGlobalInitTool();

        public struct GlobalData
        {
            public int SaveRenderInOK;
            public int SaveRenderInNG;
            public int SaveOrgInOK;
            public int SaveOrgInNG;

            public float LineTime;
            public int lineOKNum;
            public int lineNGNum;
            public int lineTotalNum;

            public float ShellTime;
            public float ShellOKNum;
            public float ShellNGNum;
            public float ShellTotalNum;
        }

        public static GlobalData _GlobalData = new GlobalData();

        public static void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var jsonData = JsonConvert.DeserializeObject<GlobalData>(json);
                _GlobalData = jsonData;
            }
            else
            {
                _GlobalData = new GlobalData();
                SaveJson(path);
            }
        }

        public static void SaveJson(string path)
        {
            var json = JsonConvert.SerializeObject(_GlobalData, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}

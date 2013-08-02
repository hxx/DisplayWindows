using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayWindows
{
    class VideoPropertiesClass : ControlPropertiesClass
    {
        /*摄像头属性默认值*/
        public string Name = "摄像头";
        public string Manufacturer = "海康威视";
        public string User = "admin";
        public string Password = "12345";
        public string IP = "192.168.1.110";
        public string Port = "8000";
        public string Stream = "主码流";
        public string Protocol = "TCP协议";
        public string Channel = "2";
        public string Remark = "192.168.1.110|111";
        public int videoCount = 0;
        public string Type = "video";
        public int Save = 0;
        public int Delete = 0;
        public int Update = 0;
    }
}

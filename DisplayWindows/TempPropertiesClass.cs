using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayWindows
{
    class TempPropertiesClass : ControlPropertiesClass
    {
        /*温湿度属性默认值*/
        public string Name = "温湿度";
        public string Manufacturer = "奥松电子";
        public int tempCount = 0;
        public string Type = "temp";
        public string Remark = null;
        public int Save = 0;
        public int Delete = 0;
        public int Update = 0;
    }
}

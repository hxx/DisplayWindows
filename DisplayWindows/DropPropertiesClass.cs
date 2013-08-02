using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayWindows
{
    class DropPropertiesClass : ControlPropertiesClass
    {
        /*水浸属性默认值*/
        public string Name = "水浸";
        public string Manufacturer = "矢量科技";
        public int dropCount = 0;
        public string Type = "drop";
        public string Remark = null;
        public int Save = 0;
        public int Delete = 0;
        public int Update = 0;
    }
}

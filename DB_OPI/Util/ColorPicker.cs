using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DB_OPI.Util
{
    class ColorPicker
    {
        public static Color ReheatingColor()
        {
            return Color.Yellow;
        }

        public static Color ReheatDone()
        {
            return Color.White;
        }

        public static Color Expired()
        {
            return Color.Red;
        }

        public static Color LifeTimeEnd()
        {
            return Color.Red;
        }

        public static Color WillLifeEnd()
        {
            return Color.Yellow;
        }
    }
}

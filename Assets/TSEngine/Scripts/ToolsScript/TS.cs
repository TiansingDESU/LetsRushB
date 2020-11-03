using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class TS
    {
        public static void log(string str)
        {
            Debug.Log(str);
        }

        public static void error(string str)
        {
            Debug.LogError(str);
        }

        public static void warn(string str)
        {
            Debug.LogWarning(str);
        }

        public static void errorFormat(string str)
        {
            Debug.LogErrorFormat(str);
        }
    }
}

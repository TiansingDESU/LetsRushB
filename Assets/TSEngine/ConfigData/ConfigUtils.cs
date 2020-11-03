using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class ConfigUtils
    {
        public static T ReadJson<T>(string jsonStr)
        {
            return JsonUtility.FromJson<T>(jsonStr);
        }
    }
}

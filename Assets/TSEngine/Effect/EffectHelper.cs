using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class EffectHelper
    {

        static GameObject EffectManager;
        static void CheckEffectManager()
        {
            if (EffectManager == null)
                EffectManager = GameObject.Find("Main/EffectManager");
        }

        public enum ShakeForce
        {
            Strong,
            Normal,
            Weak,
            None,
        }

        private static float strongForce = 90;
        private static float normalForce = 60;
        private static float weakForce = 30;

        private static Vector3 GetShakeForce(ShakeForce force)
        {
            switch (force)
            {
                case ShakeForce.Strong:
                    return Vector3.one * strongForce;
                case ShakeForce.Normal:
                    return Vector3.one * normalForce;
                case ShakeForce.Weak:
                    return Vector3.one * weakForce;
                case ShakeForce.None:
                    return Vector3.zero;
            }
            return Vector3.zero;
        }

        public static void ShakeCamera(ShakeForce force)
        {
            ShakeCamera(GetShakeForce(force));
        }

        public static void ShakeCamera(Vector2 vector)
        {
            TS.log("震动摄像机");
            CheckEffectManager();
        }

        public static void DoEffectAction(int actionSn)
        {
            if(actionSn == (int)EffectAction.None)
            {
                return;
            }
            if(actionSn == (int)EffectAction.Shake)
            {
                EffectHelper.ShakeCamera(ShakeForce.Normal);
                return;
            }
            if (actionSn == (int)EffectAction.ToDark)
            {
                TSScene.Instance.FadeIn(2f);
                return;
            }
            if (actionSn == (int)EffectAction.ToBright)
            {
                TSScene.Instance.FadeOut(2f);
                return;
            }
        }

        public enum EffectAction
        {
            None = 0,
            Shake = 1,
            ToDark = 2,
            ToBright = 3,
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using Assets.Def;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Assets
{
    public class UIManager : MonoBehaviour
    {

        private static UIBase comp;

        public static void ShowUI(string UIDef, System.Object obj = null)
        {
            
            Transform UIRoot = GameObject.Find(Def.UIDef.UIRoot).transform;
            
            //Debug.Log("UIRoot = " + Def.UIDef.UIRoot);
            
            GameObject UI;
            string UI_Base = "Assets." + UIDef + "Logic";
            Type t = Type.GetType(UI_Base, true, true);
            //UIBase comp;
            if (!UIRoot.Find(UIDef+"(Clone)"))
            {
                GameObject Prefab = (GameObject)Resources.Load("UI_Prefab/" + UIDef);
                //Debug.Log("PrefabResourcePath = "+ "UI_Prefab/" + UIDef);
                //Debug.Log("Prefab Name =" + Prefab.name);
                
                UI = Instantiate(Prefab, UIRoot);
                
                if (t != null)
                {
                    comp = (UIBase)UI.AddComponent(t);
                    TSEngine.Instance.ExecuteOnNextUpdate(delegate {
                        comp.OnInit();
                    });
                }
                else
                {
                    Debug.LogError("UI_base类型找不到");
                    comp = null;
                }     
            }
            else
            {
                UI = UIRoot.Find(UIDef+"(Clone)").gameObject;
                comp = (UIBase)UI.GetComponent(t);
            }
            UI.SetActive(true);
            //等一帧执行OnShow
            TSEngine.Instance.ExecuteOnNextUpdate(delegate { 
                comp.OnShow(obj);  
            });
        }

        public static void HideUI(string UIDef)
        {
            Transform UIRoot = GameObject.Find(Def.UIDef.UIRoot).transform;
            if (!UIRoot.Find(UIDef+"(Clone)"))
            {
                Debug.LogError("要关闭的界面不存在:"+ Def.UIDef.UIRoot+" : " + UIDef);
                return;
            }

            GameObject UI = UIRoot.Find(UIDef + "(Clone)").gameObject;
            string UI_Base = "Assets." + UIDef + "Logic";
            Type t = Type.GetType(UI_Base, true, true);
            UIBase comp = (UIBase)UI.GetComponent(t);
            comp.OnHide();
            UI.SetActive(false);

        }

        public static void ShowUIPop(string msg)
        {
            Transform UIRoot = GameObject.Find(Def.UIDef.UIRoot).transform;
            GameObject UI;
            string UI_Base = "Assets." + Def.UIDef.UI_Pop + "Logic";
            Type t = Type.GetType(UI_Base, true, true);
            GameObject Prefab = (GameObject)Resources.Load("UI_Prefab/" + Def.UIDef.UI_Pop);

            UI = Instantiate(Prefab, UIRoot);

            if (t != null)
            {
                comp = (UIBase)UI.AddComponent(t);
                comp.OnInit();
            }
            else
            {
                Debug.LogError("UI_base类型找不到");
                comp = null;
            }

            UI.SetActive(true);
            //等一帧执行OnShow
            TSEngine.Instance.ExecuteOnNextUpdate(delegate {
                comp.OnShow(msg);
            });
        }
    }
}

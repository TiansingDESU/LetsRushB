using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets
{
    class UIHelper
    {
        #region EventTrigger
        public static void AddClickCallBack(Button btn, Action action)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate { action?.Invoke(); });
        }

        public static void RemoveAllClick(Button btn)
        {
            btn.onClick.RemoveAllListeners();
        }

        public static void AddClickCallBack(GameObject go, Action action)
        {
            RemoveAllClick(go);
            EventTrigger et = GetEventTrigger(go);
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            UnityEngine.Events.UnityAction<BaseEventData> callback = new UnityEngine.Events.UnityAction<BaseEventData>(delegate { action?.Invoke(); });
            entry.callback.AddListener(callback);
            et.triggers.Add(entry);
        }

        public static void RemoveAllClick(GameObject go)
        {
            EventTrigger et = GetEventTrigger(go);
            for(int i= et.triggers.Count-1; i >= 0; i--)
            {
                if(et.triggers[i].eventID == EventTriggerType.PointerClick)
                {
                    et.triggers.RemoveAt(i);
                }
            }
        }

        public static EventTrigger GetEventTrigger(GameObject go)
        {
            EventTrigger et = go.GetComponent<EventTrigger>();
            if (et == null)
                et = go.AddComponent<EventTrigger>();
            return et;
        }

        #endregion

        #region ImgSprite

        //精灵图缓存
        static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

        public static void SetSprite(Image Img, string path)
        {
            Texture2D texture = Resources.Load(path) as Texture2D;
            Sprite spr;
            if (spriteCache.ContainsKey(path))
            {
                spr = spriteCache[path];
            }
            else
            {
                spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                spriteCache.Add(path, spr);
            }
            Img.sprite = spr;
        }

        #endregion

        #region Template
        public static int GetTemplateChildCount(GameObject grid)
        {
            Transform trans = grid.transform;
            if (!trans.Find("Template(Clone)"))
            {
                return 0;
            }
            int j = 0;
            for (int i = 0; i < trans.childCount; i++)
            {
                GameObject go = trans.GetChild(i).gameObject;
                if (go.name == "Template(Clone)")
                {
                    j++;
                }
            }
            return j;
        }

        public static GameObject GetTemplateChild(GameObject grid, int Idx)
        {
            Transform trans = grid.transform;
            if (!trans.Find("Template(Clone)"))
            {
                TS.error("No TemplateChild Found");
                return null;
            }
            for(int i = 0, j = 0; i < trans.childCount; i++)
            {
                GameObject go = trans.GetChild(i).gameObject;
                if (go.name == "Template(Clone)")
                {
                    if (j == Idx)
                        return go;
                    j++;
                }
            }
            TS.error("No TemplateChild Found");
            return null;
        }

        public static GameObject GetLastTemplateChild(GameObject grid)
        {
            Transform trans = grid.transform;
            if (!trans.Find("Template(Clone)"))
            {
                TS.error("No TemplateChild Found");
                return null;
            }
            GameObject go = null;
            for (int i = 0; i < trans.childCount; i++)
            {
                GameObject temp = trans.GetChild(i).gameObject;
                if (temp.name == "Template(Clone)")
                {
                    go = temp;
                }
            }
            return go;
        }

        public static void AddTemplateChild(GameObject grid, Action<GameObject> callback)
        {
            Transform trans = grid.transform;
            if (!trans.Find("Template"))
            {
                Debug.LogError("此物体下缺少Template,请检查Prefab");
                return;
            }
            GameObject template = trans.Find("Template").gameObject;
            if (template.activeSelf)
            {
                template.SetActive(false);
            }
            GameObject go = GameObject.Instantiate(template, trans);
            go.SetActive(true);
            callback?.Invoke(go);
        }

        public static void ClearTemplateChild(GameObject grid)
        {
            Transform trans = grid.transform;
            if (!trans.Find("Template(Clone)"))
            {
                return;
            }
            List<GameObject> destroyList = new List<GameObject>();
            for (int i = 0; i < trans.childCount; i++)
            {
                if (trans.GetChild(i).gameObject.name == "Template(Clone)")
                    destroyList.Add(trans.GetChild(i).gameObject);
            }
            for (int i = 0; i < destroyList.Count; i++)
            {
                GameObject.Destroy(destroyList[i]);
            }
        }
        #endregion
    }
}
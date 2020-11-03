using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class VFScene
    {

        static GameObject BackGround;

        static void CheckBackGround()
        {
            if (BackGround == null)
                BackGround = GameObject.Find("Main/BackGround");
        }

        //精灵图缓存
        static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

        public static void SetBackGround(string spriteName)
        {
            CheckBackGround();
            SpriteRenderer sr = BackGround.GetComponent<SpriteRenderer>();

            string path = "BGImg/";
            Texture2D texture = Resources.Load(path+spriteName) as Texture2D;
            Sprite spr;
            if (spriteCache.ContainsKey(spriteName))
            {
                spr = spriteCache[spriteName];
            }
            else
            {
                spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                spriteCache.Add(spriteName, spr);
            }
            sr.sprite = spr;
        }
    }
}

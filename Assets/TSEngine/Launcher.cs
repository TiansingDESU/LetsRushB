using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class Launcher : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            //让Main和里面的所有物体在切换场景时不被释放
            Object[] initsObjects = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (Object go in initsObjects)
            {
                DontDestroyOnLoad(go);
            }

            //加载模块 new Module();


            //模块初始化 .OnInit();

            //加载场景
            SceneManager.LoadScene(Def.SceneDef.MainScene);

            //加载主界面UI
            UIManager.ShowUI(Def.UIDef.UI_Connect);
        }
    }

}

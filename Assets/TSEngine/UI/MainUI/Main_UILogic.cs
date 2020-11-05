using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    class Main_UILogic : Main_UIBase
    {
        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnShow(System.Object Param)
        {
            base.OnShow(Param);
        }

        public override void OnHide()
        {
            base.OnHide();
        }

        public override void OnButtonClicked(GameObject go)
        {
            UIAudio.PlayUISound?.Invoke();
            if (go == this.m_Btn_exit_Btn.gameObject)
            {
                Application.Quit();
            }
            else if (go == this.m_Btn_test_Btn.gameObject)
            {
                UIManager.HideUI(Def.UIDef.UI_Main);
                SceneManager.LoadScene("TestLevel");
            }
            else if(go== this.m_Btn_load_Btn.gameObject)
            {
                UIManager.ShowUI(Def.UIDef.UI_Credits);
                UIManager.HideUI(Def.UIDef.UI_Main);
            }
            else if(go== this.m_Btn_new_Btn.gameObject)
            {
                UIManager.ShowUI(Def.UIDef.UI_Lobby);
                UIManager.HideUI(Def.UIDef.UI_Main);
            }
        }
    }
}

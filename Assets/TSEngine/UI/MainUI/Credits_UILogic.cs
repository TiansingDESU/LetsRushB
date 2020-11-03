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
    class Credits_UILogic : Credits_UIBase
    {
        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnShow(System.Object Param)
        {
            base.OnShow(Param);
            StartCoroutine(OnShowCor());
        }

        IEnumerator OnShowCor()
        {
            yield return 1;
            Debug.Log("OnShow");
        }


        public override void OnHide()
        {
            base.OnHide();
            Debug.Log("OnHide");
        }

        public override void OnButtonClicked(GameObject go)
        {
            UIAudio.PlayUISound?.Invoke();
            if (go == this.m_Btn_back_Btn.gameObject)
            {
                UIManager.HideUI(Def.UIDef.UI_Credits);
                UIManager.ShowUI(Def.UIDef.UI_Main);
            }
        }
    }
}


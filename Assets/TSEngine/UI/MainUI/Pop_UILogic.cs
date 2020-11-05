using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class Pop_UILogic : Pop_UIBase
    {
        public override void OnInit()
        {
            base.OnInit();
            Debug.Log("OnInit");
        }

        private string message;

        public override void OnShow(System.Object Param)
        {
            base.OnShow(Param);
            message = (String)Param;
            StartCoroutine(OnShowCor());
        }

        IEnumerator OnShowCor()
        {
            yield return 1;
            m_txt_Pop_Txt.text = message;
            StartCoroutine(WaitAndFadeAway(1f, 1f));
        }

        IEnumerator WaitAndFadeAway(float waitTime,float fadeTime)
        {
            yield return new WaitForSeconds(waitTime);
            //fade method
            CanvasGroup canvasGroup = this.transform.Find("WholePanel").GetComponent<CanvasGroup>();
            float fadeParam = 1f;
            while(fadeParam>0)
            {
                canvasGroup.alpha = fadeParam;
                Vector2 tempScale = this.gameObject.GetComponent<RectTransform>().localScale;
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector2(tempScale.x - 0.03f, tempScale.y - 0.03f);
                fadeParam -=0.03f;
                yield return new WaitForFixedUpdate();
            }
            
            Destroy(this.gameObject);
        }

        public override void OnHide()
        {
            base.OnHide();
            Debug.Log("OnHide");
        }

        public override void OnButtonClicked(GameObject go)
        {
            base.OnButtonClicked(go);
        }
    }
}

using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class Connect_UILogic : Connect_UIBase
    {
        public override void OnShow(object param)
        {
            base.OnShow(param);
            ActionOnShow.RegAction(ServerManager.instance.OnConnectSuccess, OnConnectSuccess);
            ActionOnShow.RegAction(ServerManager.instance.OnDisconnect, OnDisconnect);
            m_Txt_connecte_Txt.text = string.Empty;
            m_Input1_IptField.gameObject.SetActive(true);
            m_Btn_go2_Btn.gameObject.SetActive(true);
        }

        public override void OnButtonClicked(GameObject go)
        {
            if (go == this.m_Btn_go2_Btn.gameObject)
            {
                m_Input1_IptField.gameObject.SetActive(false);
                m_Btn_go2_Btn.gameObject.SetActive(false);
                GameSetting.instance.PlayerNickName = m_Input1_IptField.text;
                ServerManager.instance.StartConnection();
                m_Txt_connecte_Txt.text = "Connecting to server";
            }
        }

        private void OnConnectSuccess()
        {
            m_Txt_connecte_Txt.text = "Connection Success";
            //Enter the menu after 2 seconds
            TimeDelay.SetTimeout(() => {
                UIManager.HideUI(Def.UIDef.UI_Connect);
                UIManager.ShowUI(Def.UIDef.UI_Main);
            }, 2f);
        }

        private void OnDisconnect(DisconnectCause cause)
        {
            m_Txt_connecte_Txt.text = "Failed : " + cause.ToString();
        }
    }
}

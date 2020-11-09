using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    class Room_UILogic : Room_UIBase
    {

        List<Toggle> playerToggleList;
        List<Toggle> playerCheckMarkList;

        public override void OnShow(object param)
        {
            base.OnShow(param);
            ActionOnShow.RegAction(RoomManager.instance.OnNewPlayerEnterRoom, OnNewPlayerEnterRoom);
            ActionOnShow.RegAction(RoomManager.instance.OnOtherPlayerLeftRoom, OnOtherPlayerLeftRoom);

            UIHelper.ClearTemplateChild(m_grid_Trans.gameObject);
            TimeDelay.SetTimeout(() => { UpdateInfo(); }, 0.5f);

            //If Not Host, Hide StartGame Button
            m_Btn_start4_Btn.gameObject.SetActive(RoomManager.instance.IsHost());
            m_Btn_start4_Btn.enabled = RoomManager.instance.IsRoomFull();
        }

        public override void OnButtonClicked(GameObject go)
        {
            if (go == this.m_Btn_start4_Btn.gameObject)
            {
                //LoadLevel
                RoomManager.instance.StartLevel(Def.SceneDef.TestNetScene);
            }
            else if(go == this.m_Btn_leave5_Btn.gameObject)
            {
                //leave Room
                RoomManager.instance.LeaveRoom();
                UIManager.HideUI(Def.UIDef.UI_Room);
                UIManager.ShowUI(Def.UIDef.UI_Lobby);
            }
        }

        private void UpdateInfo()
        {
            playerToggleList = new List<Toggle>();
            playerCheckMarkList = new List<Toggle>();
            var playerList = RoomManager.instance.GetPlayerList();
            if (playerList == null || playerList.Count == 0)
                UIHelper.ClearTemplateChild(m_grid_Trans.gameObject);
            else
            {
                UIHelper.ClearTemplateChild(m_grid_Trans.gameObject);
                foreach (var player in playerList)
                {
                    UIHelper.AddTemplateChild(m_grid_Trans.gameObject, (go) => {
                        Toggle m_toggle = go.transform.Find("Toggle").GetComponent<Toggle>();
                        Toggle m_toggle_ready = go.transform.Find("Toggle_ready").GetComponent<Toggle>();
                        Text m_Txt_info = go.transform.Find("Toggle/Txt_info").GetComponent<Text>();
                        playerToggleList.Add(m_toggle);
                        playerCheckMarkList.Add(m_toggle_ready);
                        m_Txt_info.text = player.NickName;
                        UIHelper.AddClickCallBack(m_toggle.gameObject, () => {
                            OnPlayerToggleClick(player);
                        });
                    });
                }
            }
            m_Btn_start4_Btn.enabled = RoomManager.instance.IsRoomFull();
        }

        private void OnPlayerToggleClick(Player player)
        {
            //Show Player Info
        }

        private void OnNewPlayerEnterRoom(Player player)
        {
            UpdateInfo();
        }

        private void OnOtherPlayerLeftRoom(Player player)
        {
            UpdateInfo();
        }
    }
}

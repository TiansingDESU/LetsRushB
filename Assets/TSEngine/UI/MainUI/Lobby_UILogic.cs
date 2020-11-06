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
    class Lobby_UILogic : Lobby_UIBase
    {

        List<Toggle> toggleList;

        private string CurSelectRoomName;

        public override void OnShow(object param)
        {
            base.OnShow(param);
            ActionOnShow.RegAction(LobbyManager.instance.OnLobbyEnter, OnLobbyEnter);
            ActionOnShow.RegAction(LobbyManager.instance.OnCreateSuccess, OnCreateSuccess);
            ActionOnShow.RegAction(LobbyManager.instance.OnCreateFailed, OnCreateFailed);
            ActionOnShow.RegAction(LobbyManager.instance.OnJoinSuccess, OnJoinSuccess);
            ActionOnShow.RegAction(LobbyManager.instance.OnJoinFailed, OnJoinFailed);
            ActionOnShow.RegAction(LobbyManager.instance.OnListUpdate, OnListUpdate);

            LobbyManager.instance.JoinLobby();
        }

        private void OnListUpdate()
        {
            var roomDict = LobbyManager.instance.curRoomDict;
            print("ListUpdate:Count = "+roomDict.Count);
            toggleList = new List<Toggle>();
            if (roomDict == null || roomDict.Count == 0)
                UIHelper.ClearTemplateChild(m_grid_Trans.gameObject);
            else
            {
                UIHelper.ClearTemplateChild(m_grid_Trans.gameObject);
                foreach (var room in roomDict)
                {
                    UIHelper.AddTemplateChild(m_grid_Trans.gameObject, (go) => {
                        Toggle m_toggle = go.transform.Find("e_Toggle").GetComponent<Toggle>();
                        Text m_Txt_info = go.transform.Find("e_Toggle/e_Txt_info").GetComponent<Text>();
                        toggleList.Add(m_toggle);
                        m_Txt_info.text = room.Key + "    " + room.Value.PlayerCount + "/" + room.Value.MaxPlayers;
                        UIHelper.AddClickCallBack(m_toggle.gameObject, () => {
                            OnRoomToggleClick(room.Value);
                        });
                    });
                }
            }
        }

        private void OnRoomToggleClick(RoomInfo info)
        {
            m_InputRoomName4_IptField.text = info.Name;
            CurSelectRoomName = info.Name;
        }

        private void OnLobbyEnter()
        {
            UIManager.ShowUIPop("Lobby Entered");
        }

        private void OnCreateSuccess()
        {
            UIManager.ShowUIPop("Create Success");
        }

        private void OnCreateFailed(short returnCode, string message)
        {
            UIManager.ShowUIPop("CreateRoomFailed:" + message + ", code:" + returnCode);
        }

        private void OnJoinSuccess()
        {
            UIManager.ShowUIPop("Join Success");
            UIManager.HideUI(Def.UIDef.UI_Lobby);
            UIManager.ShowUI(Def.UIDef.UI_Room);
        }

        private void OnJoinFailed(short returnCode, string message)
        {
            UIManager.ShowUIPop("JoinRoomFailed:" + message + ", code:" + returnCode);
        }

        public override void OnButtonClicked(GameObject go)
        {
            if(go == this.m_Btn_Create3_Btn.gameObject)
            {
                if (string.IsNullOrEmpty(m_InputRoomName4_IptField.text))
                {
                    UIManager.ShowUIPop("Enter U RoomName");
                    return;
                }
                LobbyManager.instance.CreateRoom(new CustomRoom() { roomName = m_InputRoomName4_IptField.text, maxPlayers = 2});
            }
            else if (go == this.m_Btn_EnterRoom2_Btn.gameObject)
            {
                if (string.IsNullOrEmpty(CurSelectRoomName))
                {
                    UIManager.ShowUIPop("No Room Selected");
                }
                LobbyManager.instance.JoinRoom(CurSelectRoomName);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    class Lobby_UILogic : Lobby_UIBase
    {
        public override void OnShow(object param)
        {
            base.OnShow(param);
            ActionOnShow.RegAction(RoomManager.instance.OnLobbyEnter, OnLobbyEnter);
            ActionOnShow.RegAction(RoomManager.instance.OnCreateSuccess, OnCreateSuccess);
            ActionOnShow.RegAction(RoomManager.instance.OnCreateFailed, OnCreateFailed);
            ActionOnShow.RegAction(RoomManager.instance.OnJoinSuccess, OnJoinSuccess);
            ActionOnShow.RegAction(RoomManager.instance.OnJoinFailed, OnJoinFailed);

            RoomManager.instance.JoinLobby();
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
        }

        private void OnJoinFailed(short returnCode, string message)
        {
            UIManager.ShowUIPop("JoinRoomFailed:" + message + ", code:" + returnCode);
        }

        public override void OnButtonClicked(GameObject go)
        {
            if(go == this.m_Btn_Create3_Btn.gameObject)
            {
                RoomManager.instance.CreateRoom(new CustomRoom() { roomName = m_InputRoomName4_IptField.text, maxPlayers = 2});
            }
            else if (go == this.m_Btn_EnterRoom2_Btn.gameObject)
            {
                //RoomManager.instance.JoinRoom();
            }
        }
    }
}

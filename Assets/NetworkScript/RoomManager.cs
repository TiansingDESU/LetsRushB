using Assets;
using Assets.Def;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public EZAction<Player> OnNewPlayerEnterRoom = new EZAction<Player>();
    public EZAction<Player> OnOtherPlayerLeftRoom = new EZAction<Player>();
    public EZAction<string> OnLevelLoadEnd = new EZAction<string>();

    public static RoomManager instance;

    List<Player> playerList;

    public bool isInRoom;

    private void Awake()
    {
        instance = this;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        isInRoom = true;
        playerList = new List<Player>();
        Player[] list = PhotonNetwork.PlayerList;
        for(int i = 0; i < list.Length; i++)
        {
            playerList.Add(list[i]);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        isInRoom = false;
        playerList.Clear();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        playerList.Add(newPlayer);
        OnNewPlayerEnterRoom?.Invoke(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (playerList.Contains(otherPlayer))
        {
            playerList.Remove(otherPlayer);
        }
        OnOtherPlayerLeftRoom?.Invoke(otherPlayer);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public List<Player> GetPlayerList()
    {
        return playerList;
    }

    public bool IsHost()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public bool IsRoomFull()
    {
        return (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void StartLevel(string levelName)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(levelName);
        //暂时两秒作为level加载的时间
        TimeDelay.SetTimeout(() => {
            object[] datas = new object[] { levelName };
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.Receivers = ReceiverGroup.All;
            PhotonNetwork.RaiseEvent(RaiseEventCode.LEVEL_LOAD_END_EVENT, datas, eventOptions, SendOptions.SendReliable);
        }, 2f);
    }

    private void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += EventReceived;
    }

    public void EventReceived(EventData obj)
    {
        if(obj.Code == RaiseEventCode.LEVEL_LOAD_END_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            string levelName = (string)datas[0];
            OnLevelLoadEnd?.Invoke(levelName);
        }
    }
}

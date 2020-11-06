using Assets;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public EZAction OnLobbyEnter = new EZAction();
    public EZAction OnCreateSuccess = new EZAction();
    public EZAction<short, string> OnCreateFailed = new EZAction<short, string>();
    public EZAction OnJoinSuccess = new EZAction();
    public EZAction<short, string> OnJoinFailed = new EZAction<short, string>();
    public EZAction OnListUpdate = new EZAction();

    public Dictionary<string, RoomInfo> curRoomDict;

    public static LobbyManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void JoinLobby()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        curRoomDict = new Dictionary<string, RoomInfo>();

        OnLobbyEnter?.Invoke();
        print("Lobby Joined");
    }

    public void CreateRoom(CustomRoom room)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = (byte)room.maxPlayers;
        PhotonNetwork.CreateRoom(room.roomName,option,TypedLobby.Default);
    }

    public void JoinRoom(string roomName)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        OnCreateSuccess?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        OnCreateFailed?.Invoke(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        OnJoinSuccess?.Invoke();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        OnJoinFailed?.Invoke(returnCode, message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach(var info in roomList)
        {
            if (info.RemovedFromList)
            {
                if (curRoomDict.ContainsKey(info.Name))
                {
                    curRoomDict.Remove(info.Name);
                }
            }
            else
            {
                curRoomDict.Add(info.Name, info);
            }
        }
        OnListUpdate?.Invoke();
    }
}

public struct CustomRoom
{
    public string roomName;
    public int maxPlayers;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Assets;

public class ServerManager : MonoBehaviourPunCallbacks
{

    public static ServerManager instance;

    public EZAction OnConnectSuccess = new EZAction();
    public EZAction<DisconnectCause> OnDisconnect = new EZAction<DisconnectCause>();

    private void Awake()
    {
        instance = this;
    }

    public void StartConnection()
    {
        print("connecting to China server");

        var AppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        AppSettings.FixedRegion = "cn";
        AppSettings.UseNameServer = true;
        AppSettings.AppIdRealtime = "409f1f42-a4ae-4d13-b87d-d20947ad4ee5";
        AppSettings.Server = "ns.photonengine.cn";

        string nickName = GameSetting.instance.PlayerNickName == null ? "PlayerDefault" : GameSetting.instance.PlayerNickName;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("connected to server successful");
        OnConnectSuccess?.Invoke();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("Disconnect/Connection Failed----reason:" + cause.ToString());
        OnDisconnect?.Invoke(cause);
    }
}

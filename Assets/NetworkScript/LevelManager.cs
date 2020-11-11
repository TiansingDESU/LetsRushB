using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager instance;

    public GameObject player;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RoomManager.instance.OnLevelLoadEnd += OnLevelLoadEnd;
    }

    public void OnLevelLoadEnd(string levelName)
    {
        if(levelName!= Assets.Def.SceneDef.Net_DayScene)
        {
            return;
        }
        LevelStart();
    }

    public void LevelStart()
    {
        Transform pos;
        if (RoomManager.instance.IsHost())
        {
            //born in Pos A
            pos = LevelInfo.instance.TeamABornPos;
            
        }
        else
        {
            //born in pos B
            pos = LevelInfo.instance.TeamBBornPos;
        }
        player = MasterManager.NetworkInstantiate("TestPlayer", pos.position, pos.rotation);
        player.GetComponent<CarStatus>().PlayerName = MasterManager.GetMyPlayerInfo().NickName;
        player.GetComponent<CarStatus>().PlayerId = MasterManager.GetMyPlayerInfo().UserId;
    }

}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager instance;

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
        if(levelName!= Assets.Def.SceneDef.TestNetScene)
        {
            return;
        }
        LevelStart();
    }

    public void LevelStart()
    {
        if (RoomManager.instance.IsHost())
        {
            //born in Pos A
        }
        else
        {
            //born in pos B
        }
    }

}

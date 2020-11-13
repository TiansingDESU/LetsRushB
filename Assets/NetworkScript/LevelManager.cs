using Assets;
using Assets.Def;
using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager instance;

    public GameObject player;

    public enum TeamType
    {
        None,
        BlueTeam,
        RedTeam
    }

    #region Level Instance Data
    public const int TeamRedInitLifes = 5;
    public int TeamRedLifes;

    public const int TeamBlueInitLifes = 5;
    public int TeamBlueLifes;

    public List<string> TeamBlueIdList;
    public List<string> TeamRedIdList;

    public TeamType WinTeam;

    public bool isLevelEnd;
    #endregion

    #region Level Action Callbacks
    public EZAction OnLifeChange = new EZAction();
    public EZAction OnShowLevelEnd = new EZAction();
    public EZAction OnLevelEnd = new EZAction();
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PhotonNetwork.NetworkingClient.EventReceived += EventReceived;

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
        player = null;
        InitLevelData();
        LocalPlayerReborn();
    }

    private void InitLevelData()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        SetLife(TeamBlueInitLifes, TeamRedInitLifes);
        InitTeamIdList();
        isLevelEnd = false;
    }

    private void InitTeamIdList()
    {
        var playerList = RoomManager.instance.GetPlayerList();
        TeamBlueIdList = new List<string>();
        TeamRedIdList = new List<string>();
        foreach(var player in playerList)
        {
            if (player.IsMasterClient)
            {
                TeamRedIdList.Add(player.UserId);
            }
            else
            {
                TeamBlueIdList.Add(player.UserId);
            }
        }
    }

    private void SetLife(int blue, int red)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        object[] datas = new object[] { blue, red };
        RaiseEventOptions eventOptions = new RaiseEventOptions();
        eventOptions.Receivers = ReceiverGroup.All;
        PhotonNetwork.RaiseEvent(RaiseEventCode.LEVEL_SET_LIFE_EVENT, datas, eventOptions, SendOptions.SendReliable);
    }

    private void ChangeLife(int blue, int red)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        object[] datas = new object[] { blue, red };
        RaiseEventOptions eventOptions = new RaiseEventOptions();
        eventOptions.Receivers = ReceiverGroup.All;
        PhotonNetwork.RaiseEvent(RaiseEventCode.LEVEL_CHANGE_LIFE_EVENT, datas, eventOptions, SendOptions.SendReliable);
    }

    public void OnPlayerDead(string playerId)
    {
        //客户端自己判断重生
        if(PhotonNetwork.LocalPlayer.UserId == playerId)
        {
            //本地客户端玩家死亡
            //三秒后重生
            TimeDelay.SetTimeout(() => { LocalPlayerReborn(); }, 3f);
        }
        //主机判断Life剩余
        if (PhotonNetwork.IsMasterClient)
        {
            if (IsRedTeam(playerId))
            {
                ChangeLife(0, -1);
            }
            else if (isBlueTeam(playerId))
            {
                ChangeLife(-1, 0);
            }
        }
    }

    public bool IsRedTeam(string id)
    {
        return TeamRedIdList.Contains(id);
    }

    public bool isBlueTeam(string id)
    {
        return TeamBlueIdList.Contains(id);
    }

    public void LocalPlayerReborn()
    {
        if (isLevelEnd)
        {
            //关卡结束就不重生了
            return;
        }
        if (player != null)
        {
            //如果玩家没死，就处死
            player.GetComponent<PlayerActionEvent>().OnHit?.Invoke(1000);
            return;
        }
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

    public void KillLocalPlayer()
    {
        if (player != null)
        {
            //如果玩家没死，就处死
            player.GetComponent<PlayerActionEvent>().OnHit?.Invoke(1000);
        }
    }

    public void CheckEnd()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        TeamType teamType =  TeamType.None;
        if (TeamBlueLifes <= 0)
            teamType = TeamType.BlueTeam;
        else if (TeamRedLifes <= 0)
            teamType = TeamType.RedTeam;

        if (teamType != TeamType.None)
        {
            object[] datas = new object[] { teamType };
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.Receivers = ReceiverGroup.All;
            PhotonNetwork.RaiseEvent(RaiseEventCode.LEVEL_END_EVENT, datas, eventOptions, SendOptions.SendReliable);
        }

    }

    private void LevelEnd(TeamType loseTeam)
    {
        if (loseTeam == TeamType.BlueTeam)
            WinTeam = TeamType.RedTeam;
        else if (loseTeam == TeamType.RedTeam)
            WinTeam = TeamType.BlueTeam;
        isLevelEnd = true;
        OnLevelEnd?.Invoke();

        LevelInfo.instance.EndMissle.SetActive(true);
        TSEngine.Instance.ExecuteOnNextUpdate(() =>
        {
            float delay = LevelInfo.instance.EndMissle.GetComponent<MissleDown>().exploseDelay;
            TimeDelay.SetTimeout(() => { 
                LevelInfo.instance.WaterEnd.GetComponent<DOTweenAnimation>().DOPlay();
                TimeDelay.SetTimeout(() => {
                    OnShowLevelEnd?.Invoke();
                },6f);
            }, delay);
        });
        

    }

    public void EventReceived(EventData obj)
    {
        if (obj.Code == RaiseEventCode.LEVEL_SET_LIFE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            int blue = (int)datas[0];
            int red = (int)datas[1];
            TeamBlueLifes = blue;
            TeamRedLifes = red;
            OnLifeChange?.Invoke();
            CheckEnd();
        }
        else if(obj.Code == RaiseEventCode.LEVEL_CHANGE_LIFE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            int blue = (int)datas[0];
            int red = (int)datas[1];
            TeamBlueLifes += blue;
            TeamRedLifes += red;
            if (TeamRedLifes < 0)
                TeamRedLifes = 0;
            if (TeamBlueLifes < 0)
                TeamBlueLifes = 0;
            OnLifeChange?.Invoke();
            CheckEnd();
        }
        else if(obj.Code == RaiseEventCode.LEVEL_END_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            TeamType loseTeam = (TeamType)datas[0];
            LevelEnd(loseTeam);
        }
    }
}

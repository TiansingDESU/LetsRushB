using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviourPun
{
    [Header("玩家参数")]
    public string PlayerName;
    public string PlayerId;

    [Header("初始性能参数")]
    public float Heath = 100f;

    [Header("死亡FXPrefab")]
    public GameObject DeadFX_prefab;

    PlayerActionEvent pAction;

    bool isDead;

    private void Start()
    {
        pAction = this.GetComponent<PlayerActionEvent>();
        pAction.OnHit += OnHit;
        pAction.OnDead += OnDead;

        isDead = false;
    }

    private void OnHit(float hit)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        this.photonView.RPC("OnHitRPC", RpcTarget.All, hit);
    }

    [PunRPC]
    private void OnHitRPC(float hit)
    {
        Debug.Log("Hit:"+PlayerName);
        Heath -= hit;
        if (Heath < 0)
            Heath = 0;
        if (Heath == 0)
            pAction.OnDead?.Invoke();
    }

    private void OnDead()
    {
        if (isDead)
        {
            return;
        }
        Debug.Log("Player:" + PlayerId + "just Dead");
        isDead = true;
        //play Dead FX
        GameObject goFx = GameObject.Instantiate(DeadFX_prefab);
        goFx.transform.position = this.transform.position;
        //destory on next frame to pervent sth not executed
        TSEngine.Instance.ExecuteOnNextUpdate(() => {
            Destroy(gameObject);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    [Header("玩家参数")]
    public string PlayerName;
    public long PlayerId;

    [Header("初始性能参数")]
    public float Heath = 100f;

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
    }
}

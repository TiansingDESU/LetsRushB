using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActionEvent))]
public class CarShootController : MonoBehaviourPun
{

    public Transform gunEndPos;

    PlayerActionEvent pAction;

    private void Start()
    {
        pAction = this.GetComponent<PlayerActionEvent>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        //左键按下/空格按下
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            pAction.Shoot?.Invoke(gunEndPos.position, gunEndPos.forward);
        }
    }
}

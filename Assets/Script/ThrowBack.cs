using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBack : MonoBehaviourPun
{
    public GameObject throwItem;

    public Transform throwPoint;

    PlayerActionEvent pAct;

    private void Start()
    {
        pAct = this.GetComponent<PlayerActionEvent>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC("ThrowItemRPC", RpcTarget.All, this.transform.position);
            pAct.Throw?.Invoke(throwItem.name);
        }
    }

    [PunRPC]
    private void ThrowItemRPC(Vector3 pos)
    {
        GameObject go = GameObject.Instantiate(throwItem);
        go.transform.position = pos;
    }
}

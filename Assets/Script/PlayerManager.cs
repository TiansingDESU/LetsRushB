using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    private void Start()
    {
        //Camera
        if (this.photonView.IsMine)
        {
            LevelInfo.instance.PlayerCamera.gameObject.SetActive(true);
            CinemachineVirtualCamera VC = LevelInfo.instance.VirtualCamera;
            VC.Follow = this.gameObject.transform;
            VC.LookAt = this.gameObject.transform;
        }
    }
}

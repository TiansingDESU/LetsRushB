using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform TeamABornPos;
    public Transform TeamBBornPos;

    public CinemachineVirtualCamera VirtualCamera;

    public Camera PlayerCamera;

    public GameObject EndMissle;

    public GameObject WaterEnd;
}

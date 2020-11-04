using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionEvent : MonoBehaviour
{
    //所有玩家的响应事件
    public Action<Vector3, Vector3> Shoot;

}

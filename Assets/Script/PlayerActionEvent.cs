using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionEvent : MonoBehaviour
{
    //所有玩家的响应事件
    //攻击
    public Action<Vector3, Vector3> Shoot;

    //受击
    public Action<float> OnHit;
    //死亡
    public Action OnDead;

}

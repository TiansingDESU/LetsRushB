using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [Header("不可配置部分，游戏里输入")]
    public string PlayerNickName;


    public static GameSetting instance;

    private void Awake()
    {
        instance = this;
    }
}

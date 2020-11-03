using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    AudioClip s_ui;
    private void Start()
    {
        PlayUISound = OnPlayerUISound;
        s_ui = Resources.Load<AudioClip>("SFX/UI");
    }

    public static Action PlayUISound;

    public void OnPlayerUISound()
    {
        PlayClip(s_ui);
    }

    private void PlayClip(AudioClip clip)
    {
        Transform trans = GameObject.Find("Main Camera").transform;
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, trans.position);
        else
            Debug.LogError("Audio Clip Not Found");
    }
}

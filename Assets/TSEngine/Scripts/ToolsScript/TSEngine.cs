using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSEngine : MonoBehaviour
{
    static TSEngine inst = null;

    public static TSEngine Instance
    {
        get
        {
            if (inst == null)
            {
                inst = new TSEngine();
            }
            return inst;
        }
    }

    void Awake()
    {
        inst = this;
    }

    public void ExecuteOnNextUpdate(Action action)
    {
        StartCoroutine(OnFrame(1,action));
    }

    public void ExecuteOnUpdate(int frameCount, Action action)
    {
        StartCoroutine(OnFrame(frameCount, action));
    }

    IEnumerator OnFrame(int frameCount, Action action)
    {
        yield return frameCount;
        action?.Invoke();
    }

    public void ExecuteOnNextFixedUpdate(Action action)
    {
        StartCoroutine(OnFixedFrame(1, action));
    }

    public void ExecuteOnFixedUpdate(int frameCount, Action action)
    {
        StartCoroutine(OnFixedFrame(frameCount, action));
    }

    IEnumerator OnFixedFrame(int frameCount, Action action)
    {
        for (int i = 0; i < frameCount; i++)
            yield return new WaitForFixedUpdate();
        action?.Invoke();
    }
}

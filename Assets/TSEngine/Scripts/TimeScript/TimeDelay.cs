using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDelay : MonoBehaviour
{
    public static bool Pause { get; set; }
    private static List<TimeFunc> funcs = new List<TimeFunc>();

    internal static List<TimeFunc> Functions { get { return funcs; } }

    static TimeDelay inst = null;

    public static TimeDelay Instance { 
        get 
        {
            if (inst == null)
            {
                inst = new TimeDelay();
            }
            return inst; 
        } 
    }

    void Awake()
    {
        inst = this;
    }

    public static void SetTimeoutScaled(Action func, float time, bool loop, bool canPause)
    {
        SetTimeout(func, time, loop, canPause, true);
    }

    public static void SetTimeout(Action func, float time, bool loop, bool canPause)
    {
        SetTimeout(func, time, loop, canPause, true);
    }

    public static void SetTimeout(Action func, float time, bool loop = false, bool canPause = true, bool scaledTime = false)
    {
        if (time > 0)
        {
            if (CheckDuplicate(func))
                return;
            TimeFunc tf = new TimeFunc(func, time);
            tf.loop = loop;
            tf.canPause = canPause;
            tf.scaledTime = scaledTime;
            funcs.Add(tf);
            inst.enabled = true;
        }
        else
        {
            func();
        }
    }

    public static bool HasAction(Action func)
    {
        foreach (var i in funcs)
        {
            if (i.handler == func)
                return true;
        }
        return false;
    }

    public static void SetTimeIntervalScaled(Action func, float interval, bool canPause = true)
    {
        if (interval > 0)
        {
            SetTimeout(func, interval, true, canPause, true);
        }
    }

    public static void SetTimeInterval(Action func, float interval, bool canPause = true)
    {
        if (interval > 0)
        {
            SetTimeout(func, interval, true, canPause);
        }
    }

    public static void RemoveTimeaction(Action func)
    {
        int length = funcs.Count;
        for (int i = 0; i < length; i++)
        {
            if (funcs[i].handler == func)
            {
                funcs.RemoveAt(i);
                return;
            }
        }
    }

    public static void ClearAllCanPaused()
    {
        int length = funcs.Count;
        for (int i = 0; i < length; i++)
        {
            if (funcs[i].canPause)
            {
                funcs.RemoveAt(i);
                i--;
            }
        }
    }

    public void ClearAllAction()
    {
        funcs.Clear();
    }

    private static bool CheckDuplicate(Action func)
    {
        int length = funcs.Count;
        for (int i = 0; i < length; i++)
        {
            if (funcs[i].handler == func)
                return true;
        }
        return false;
    }

    void Update()
    {
        float time = Time.deltaTime;
        float timescale = Time.timeScale;
        int length = funcs.Count;
        for (int i = 0; i < length; i++)
        {
            var func = funcs[i];
            if (func.canPause && Pause)
                continue;
            func.time -= time * (func.scaledTime ? timescale : 1);
            if (func.time <= 0)
            {
                func.handler?.Invoke();
                if (func.loop)
                {
                    func.Reset();
                }
                else
                {
                    funcs.RemoveAt(i);
                    i--;
                    length--;
                }
            }
        }
        if (funcs.Count == 0)
            enabled = false;
    }

    public static string GetDoubleTimer(int time)
    {
        if (time > 0)
        {
            return time.ToString();
        }
        else
        {
            return "0" + time.ToString();
        }
    }
}

class TimeFunc
{
    public Action handler;
    public float time;
    public float maxTime;
    public bool loop;
    public bool canPause;
    public bool scaledTime;
    public TimeFunc(Action func, float tm)
    {
        handler = func;
        time = tm;
        maxTime = time;
        loop = false;
        canPause = true;
        scaledTime = false;
    }

    public void Reset()
    {
        time = maxTime;
    }
}

    //public static TimeDelay Begin(GameObject go,float Time)
    //{
    //    GameObject tempGo;
    //    if (go == null)
    //        tempGo = new GameObject();
    //    else
    //        tempGo = go;
    //    if (tempGo.GetComponent<TimeDelay>() != null)
    //    {
    //        Debug.LogError("GameObject:"+go.name+" already has a TimeDelay script");
    //        return null;
    //    }
    //    else
    //    {
    //        TimeDelay comp = tempGo.AddComponent<TimeDelay>();
    //        comp.StartDelay(Time,(go==null));
    //        return comp;
    //    }
    //}

    //public Action DelayFinish;

    //public void StartDelay(float time,bool t)
    //{
    //    StartCoroutine(Delay(time,t));
    //}

    //IEnumerator Delay(float time,bool t)
    //{
    //    yield return new WaitForSeconds(time);
    //    DelayFinish?.Invoke();
    //    if (t)
    //        Destroy(this.gameObject);
    //}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSScene : MonoBehaviour
{
    [Header("UIPrefab")]
    public GameObject blackUI;


    static TSScene inst = null;

    public static TSScene Instance
    {
        get
        {
            if (inst == null)
            {
                inst = new TSScene();
            }
            return inst;
        }
    }

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        blackUI.SetActive(false);
        blackImg = blackUI.transform.Find("Image").GetComponent<Image>();
    }
    private Image blackImg;

    public void FadeIn(float time)
    {
        blackUI.SetActive(true);
        blackImg.CrossFadeAlpha(0, 0, true);
        blackImg.CrossFadeAlpha(1, time, true);
    }

    public void FadeOut(float time)
    {
        blackImg.CrossFadeAlpha(0, time, true);
        TimeDelay.SetTimeout(() => { blackUI.SetActive(false); }, time);
    }
}

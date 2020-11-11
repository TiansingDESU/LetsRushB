using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float ActDelay = 2f;

    public GameObject FX_smoke;

    private void Start()
    {
        TimeDelay.SetTimeout(() => { Explose(); }, 2f);
    }

    private void Explose()
    {
        GameObject go = GameObject.Instantiate(FX_smoke);
        go.transform.position = transform.position;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleDown : MonoBehaviour
{
    public float exploseDelay;

    public float Scale = 10;

    public GameObject FX_explose;

    private void Start()
    {
        this.GetComponent<Animation>().Play();

        exploseDelay = this.GetComponent<Animation>().clip.length;
        TimeDelay.SetTimeout(() =>
        {
            GameObject go = GameObject.Instantiate(FX_explose);
            go.transform.position = this.transform.position;
            go.transform.localScale = Vector3.one * Scale;
            this.gameObject.SetActive(false);
        }, exploseDelay);
    }
}

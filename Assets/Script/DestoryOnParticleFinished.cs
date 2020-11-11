using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnParticleFinished : MonoBehaviour
{
    public float DestoryDelay;

    private void Start()
    {
        DestoryDelay = GetComponent<ParticleSystem>().main.duration;
        //add 1 seconds to fade away
        TimeDelay.SetTimeout(() => { Destroy(gameObject); }, DestoryDelay + 1f);
    }
}

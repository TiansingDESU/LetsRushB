using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnParticleFinished : MonoBehaviour
{
    public float DestoryDelay;

    private void Start()
    {
        DestoryDelay = GetComponent<ParticleSystem>().main.duration;
        TimeDelay.SetTimeout(() => { Destroy(gameObject); }, DestoryDelay);
    }
}

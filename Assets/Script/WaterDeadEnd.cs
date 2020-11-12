using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarSphere"))
        {
            other.GetComponent<SphereParent>().parent.GetComponent<PlayerActionEvent>().OnHit?.Invoke(1000);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody carRB;

    [Header("Param")]
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180f;

    private float speedInput, turnInput;

    Vector3 offset;

    private void Start()
    {
        carRB.transform.parent = null;
        offset = transform.position - carRB.transform.position;
    }


    private void Update()
    {
        speedInput = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        transform.position = carRB.transform.position + offset;
    }

    private void FixedUpdate()
    {
        if(speedInput != 0)
        {
            carRB.AddForce(transform.forward * speedInput);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody carRB;

    [Header("Param")]
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180f, gravityForce = 10f, dragOnGround = 2f, dragOnAir = 1f;

    private float speedInput, turnInput;

    [SerializeField]
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;

    [Header("CarPart")]
    public Transform LeftFrontWheel;
    public Transform RightFrontWheel;
    public float MaxWheelTurn = 50f;

    [Header("Particle")]
    public ParticleSystem[] dusts;
    public float emissionRate;
    public float maxEmission = 20f;


    Vector3 offset;

    private void Start()
    {
        carRB.transform.parent = null;
        offset = transform.position - carRB.transform.position;
    }


    private void Update()
    {
        //param
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        //vertical
        speedInput = 0;
        if (vert > 0)
        {
            speedInput = vert * forwardAccel;
        }
        else if(vert < 0)
        {
            speedInput = vert * reverseAccel;
        }

        //horizontal
        turnInput = hor;

        //car rotation
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * vert, 0f));
        }

        LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheel.localRotation.eulerAngles.x, (turnInput * MaxWheelTurn) - 180, LeftFrontWheel.localRotation.eulerAngles.z);
        RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheel.localRotation.eulerAngles.x, (turnInput * MaxWheelTurn), RightFrontWheel.localRotation.eulerAngles.z);

        //make sure car follow sphere
        transform.position = carRB.transform.position + offset;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        emissionRate = 0;

        if (grounded)
        {
            //Drag On Ground
            carRB.drag = dragOnGround;
            //Control avalible on ground
            if (speedInput != 0)
            {
                carRB.AddForce(transform.forward * speedInput);

                emissionRate = maxEmission;
            }
        }
        else
        {
            //Drag On Air
            carRB.drag = dragOnAir;
            //Add G Force
            carRB.AddForce(Vector3.up * -gravityForce);
        }

        foreach(var ps in dusts)
        {
            var emissionModule = ps.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }
}

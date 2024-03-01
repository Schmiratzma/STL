using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float pitchSpeed = 10f; // rotation around X Axis
    [SerializeField] float rollSpeed = 10f; // rotation around Y Axis
    [SerializeField] float yawSpeed = 10f; // rotation around Z Axis

    [SerializeField] float flySpeed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // pitch up
            transform.localRotation *= Quaternion.Euler(pitchSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // pitch down
            transform.localRotation *= Quaternion.Euler(-pitchSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            // roll left
            transform.localRotation *= Quaternion.Euler(0, -rollSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            // roll right
            transform.localRotation *= Quaternion.Euler(0, rollSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // yaw left
            transform.localRotation *= Quaternion.Euler(0, 0, yawSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // yaw right
            transform.localRotation *= Quaternion.Euler(0, 0, -yawSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * flySpeed;
    }
}

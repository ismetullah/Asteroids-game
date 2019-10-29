using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float rotationSpeed = 5;
    public float thrustForce = 7;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Input.GetAxis("Horizontal"));
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(transform.up * thrustForce);
        }
    }
}

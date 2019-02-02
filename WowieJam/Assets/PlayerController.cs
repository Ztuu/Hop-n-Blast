using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 offset;
    float speed = 5.0f;
    Rigidbody2D rb;

    void Start()
    {
        offset = new Vector3(1.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInp = Input.GetAxisRaw("Horizontal");

        if(horizontalInp != 0.0f)
        {
            transform.position += offset * horizontalInp * Time.deltaTime * speed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0.0f, 100.0f, 0.0f));
        }
    }
}

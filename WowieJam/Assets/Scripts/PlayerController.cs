using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 offset;
    float speed = 5.0f;
    Rigidbody2D rb;

    enum PlayerState {IDLE, RUNNING, JUMPING};
    PlayerState playerState;

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
            playerState = PlayerState.RUNNING;
            transform.position += offset * horizontalInp * Time.deltaTime * speed;
        }else
        {
            playerState = PlayerState.IDLE;
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerState = PlayerState.JUMPING;
            rb.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 offset;
    float speed = 7.0f;
    float projDistance = 0.5f; //How far from the player the projectile spawns
    Rigidbody2D rb;
    SpriteRenderer spriteRend;

    enum PlayerDirection {LEFT, RIGHT};
    PlayerDirection playerDirection;
    enum PlayerState {IDLE, RUNNING, JUMPING, FIRING};
    PlayerState playerState;
    bool canShoot;

    public GameObject projectile;


    void Start()
    {
        offset = new Vector3(1.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();

        playerState = PlayerState.IDLE;
        playerDirection = PlayerDirection.RIGHT;
        canShoot = true;
    }

    void Update()
    {
        float horizontalInp = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            switch (playerState)
            {
                case PlayerState.IDLE:
                    Shoot();
                    break;
                case PlayerState.RUNNING:
                    Shoot();
                    break;
                case PlayerState.JUMPING:
                    ShootDown();
                    break;
                case PlayerState.FIRING:
                    break;
            }
        }

        if(horizontalInp != 0.0f)
        {
            playerState = PlayerState.RUNNING;
            transform.position += offset * horizontalInp * Time.deltaTime * speed;

            if(horizontalInp > 0.0f && playerDirection != PlayerDirection.RIGHT)
            {
                playerDirection = PlayerDirection.RIGHT;
                spriteRend.flipX = false;
            }
            else if (horizontalInp < 0.0f && playerDirection != PlayerDirection.LEFT)
            {
                playerDirection = PlayerDirection.LEFT;
                spriteRend.flipX = true;
            }
        }
        else
        {
            playerState = PlayerState.IDLE;
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerState = PlayerState.JUMPING;
            rb.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode2D.Impulse);
        }
    }

    void Shoot()
    {
        canShoot = false;

        Vector3 direction = Vector3.zero;

        switch (playerDirection)
        {
            case PlayerDirection.RIGHT:
                direction = new Vector3(1.0f, 0.0f, 0.0f);
                break;
            case PlayerDirection.LEFT:
                direction = new Vector3(-1.0f, 0.0f, 0.0f);
                break;
        }

        Vector3 startPos = transform.position + direction * projDistance;


        GameObject tempProjRef = Instantiate(projectile, startPos, Quaternion.identity);
        Projectile tempProjScript = tempProjRef.GetComponent<Projectile>();

        try
        {
            tempProjScript.SetDirection(direction);
        }
        catch (System.NullReferenceException npe)
        {
            Debug.Log(npe.Message.ToString());
            //TODO: Handle this
        }

        canShoot = true; 
    }

    void ShootDown()
    {
        canShoot = false;
    }
}

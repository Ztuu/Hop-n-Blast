using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 offset;
    float speed = 7.0f;
    float projDistance = 0.5f; //How far from the player the horizontal projectile spawns
    float vertProjDistance = 0.8f; //How far below the player the vertical projectile spawns
    Rigidbody2D rb;
    SpriteRenderer spriteRend;

    enum PlayerDirection {LEFT, RIGHT};
    PlayerDirection playerDirection;
    enum PlayerState {IDLE, RUNNING, JUMPING, FIRING};
    PlayerState playerState;

    bool canShoot;
    bool canDoubleJump;

    public GameObject projectile;
    public LayerMask mask; //Public so that it can be assigned in the editor


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

        //TODO: Use 3 raycasts incase player is on edge of platformew|
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -1.0f * transform.up, 0.6f, mask);
        if(hit)
        {
            canDoubleJump = true;
            if(rb.velocity.x > 0.3f | rb.velocity.x < 0.3f)
            {
                playerState = PlayerState.RUNNING;
            }else
            {
                playerState = PlayerState.IDLE;
            }
        }else //if the ray doesn't hit anything
        {
            playerState = PlayerState.JUMPING;
        }

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
                    if (canDoubleJump)
                    {
                        ShootDown();
                    }
                    break;
                case PlayerState.FIRING:
                    break;
            }
        }

        if(horizontalInp != 0.0f)
        {
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

        if (Input.GetButtonDown("Jump") && (playerState == PlayerState.IDLE || playerState == PlayerState.RUNNING))
        {
            rb.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode2D.Impulse);
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

        StartCoroutine(ShootDelay());
    }

    void ShootDown()
    {
        canShoot = false;
        canDoubleJump = false;

        Vector3 vertDirection = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 startPos = transform.position + vertProjDistance * vertDirection;

        GameObject tempProjRef = Instantiate(projectile, startPos, Quaternion.identity);
        Projectile tempProjScript = tempProjRef.GetComponent<Projectile>();

        try
        {
            tempProjScript.SetDirection(vertDirection);
        }
        catch (System.NullReferenceException npe)
        {
            Debug.Log(npe.Message.ToString());
            //TODO: Handle this
        }

        rb.AddForce(new Vector3(0.0f, 20.0f, 0.0f), ForceMode2D.Impulse); //TODO: Fix double jump
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.4f);
        canShoot = true;
    }
}

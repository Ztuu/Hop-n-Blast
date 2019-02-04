using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 9.0f;
    float timer = 10.0f;
    Vector3 offset = Vector3.zero;

    void Update()
    {
        transform.position += speed * offset * Time.deltaTime;
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(Vector3 newDir)
    {
        offset = newDir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 7.0f;
    Vector3 offset = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * offset * Time.deltaTime;
    }

    public void SetDirection(Vector3 newDir)
    {
        offset = newDir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: What should the projectile do when it hits something
    }
}

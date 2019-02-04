using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool direction = true;
    Vector3 moveOffset;

    // Start is called before the first frame update
    void Start()
    {
        moveOffset = new Vector3(2.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.position += moveOffset * Time.deltaTime;
        }else
        {
            transform.position -= moveOffset * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag("Player")){
            Destroy(col.collider.gameObject);
        }else
        {
            direction = !direction;
        }
    }

    void OnDisable()
    {
        GameController.instance.AddScore(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    static List<Vector3> doorPositions;
    static readonly float doorCooldown = 0.2f;
    static float doorTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (doorPositions == null)
        {
            doorPositions = new List<Vector3>();
        }

        doorPositions.Add(transform.position);
    }

    void Update()
    {
        if(doorTimer > 0.0f)
        {
            doorTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (doorTimer <= 0.0f)
        {
            Vector3 targetPos = transform.position;

            while (targetPos.Equals(transform.position))
            {
                int index = Random.Range(0, doorPositions.Count);
                targetPos = doorPositions[index];
            }

            other.transform.position = targetPos;
            doorTimer = doorCooldown;
        }
    }
}

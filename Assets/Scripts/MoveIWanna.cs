using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIWanna : MonoBehaviour
{
    public MovetypeIWanna moveType;
    private Vector3 velocity;
    /* Patrol mode*/
    [Header("Patrol")] public Transform[] patrolPoints;
    public float patrolSpeed;
    public float movementSmoothing;
    public float stopThrottle;
    private int patrolIndex;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < patrolPoints.Length; i++) {
            patrolPoints[i].transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        switch(moveType) {
            case MovetypeIWanna.Patrol:
            MovePatrol();
            break;
            case MovetypeIWanna.None:
            default:
                break;
        }
    }

    void MovePatrol()
    {
        // in pos
        // change target
        // move
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(patrolPoints[patrolIndex].position.x, patrolPoints[patrolIndex].position.y));
        if (distance <= stopThrottle) {
            patrolIndex++;
            if (patrolIndex >= patrolPoints.Length) {
                patrolIndex = 0;
            }
        }
        Vector3 targetPos = patrolPoints[patrolIndex].position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
    }
}

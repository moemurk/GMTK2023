using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIWanna : MonoBehaviour
{
    public MovetypeIWanna moveType;
    public GameObject hpBar;
    private Vector3 velocity;
    /* Patrol mode*/
    [Header("Patrol")] public Transform[] patrolPoints;
    public float patrolSpeed;
    public float movementSmoothing;
    public float stopThrottle;
    private int patrolIndex;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < patrolPoints.Length; i++) {
            patrolPoints[i].transform.SetParent(null);
        }
        animator = GetComponent<Animator>();
    }

    public void InitState()
    {
        Debug.Log("IWanna InitState");
        velocity = Vector3.zero;
        patrolIndex = 0;
    }

    public void Move()
    {
        switch(moveType) {
            case MovetypeIWanna.Patrol:
                MovePatrol();
                break;
            case MovetypeIWanna.None:
            default:
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
                break;
        }
    }

    void MovePatrol()
    {
        // in pos
        // change target
        // move
        if (Time.deltaTime > 0.1f) {
            return;
        }
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
            new Vector2(patrolPoints[patrolIndex].position.x, patrolPoints[patrolIndex].position.y));
        if (distance <= stopThrottle) {
            patrolIndex++;
            if (patrolIndex >= patrolPoints.Length) {
                patrolIndex = 0;
            }
        }
        Vector3 targetDir = (patrolPoints[patrolIndex].position - transform.position).normalized;
        Vector3 targetPos = targetDir * patrolSpeed * 10f * Time.deltaTime + transform.position;
        SetDirection(targetPos);
        Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
        if (animator) {
            animator.SetBool("IsWalking", true);
        }
    }

    private void SetDirection(Vector3 targetPos)
    {
        if ((targetPos - transform.position).x >= 0) {
            // right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            hpBar.transform.localScale = new Vector3(-1f * Mathf.Abs(hpBar.transform.localScale.x), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        } else {
            // left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            hpBar.transform.localScale = new Vector3(1f * Mathf.Abs(hpBar.transform.localScale.x), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
    }
}

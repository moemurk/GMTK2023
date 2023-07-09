using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsaac : MonoBehaviour
{
    public MovetypeISaac moveType;
    public float moveSpeed;
    public float movementSmoothing;
    public bool activated = false;
    private Vector3 velocity;
    [Header("Rush")] public float coolTime;
    public float rushingDistance;
    public float rushSpeed;
    private float remainCoolTime;
    private bool isRushing;
    private Vector3 rushDirection;
    private Vector3 rushStartPoint;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        remainCoolTime = coolTime;
        isRushing = false;
        animator = GetComponent<Animator>();
    }

    public void InitState()
    {
        Debug.Log("Isaac InitState");
        velocity = Vector3.zero;
        activated = false;
        remainCoolTime = coolTime;
        isRushing = false;
        rushDirection = Vector3.zero;
        rushStartPoint = Vector3.zero;
    }

    public void Move()
    {
        if (!activated) {
            return;
        }
        switch(moveType) {
            case MovetypeISaac.Incline:
                MoveIncline();
                break;
            case MovetypeISaac.Rush:
                MoveRush();
                break;
            case MovetypeISaac.Vertical:
                MoveVertical();
                break;
            case MovetypeISaac.Horizontal:
                MoveHorizontal();
                break;
            case MovetypeISaac.None:
            default:
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
                break;
        }
    }

    private void MoveRush()
    {
        if (isRushing) {
            Vector3 targetPos = rushDirection * rushSpeed * Time.deltaTime * 10f + transform.position;
            Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
            SetDirection(targetPos);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
            if (Vector3.Distance(transform.position, rushStartPoint) >= rushingDistance) {
                Debug.Log("End Rush");
                isRushing = false;
                remainCoolTime = coolTime;
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
            }
        } else {
            // only cal time when rushing is over
            remainCoolTime -= Time.deltaTime;
            if (remainCoolTime <= 0f) {
                Debug.Log("Start to Rush!");
                remainCoolTime = coolTime;
                isRushing = true;
                rushDirection = (GameStateManager.Instance.player.transform.position - transform.position).normalized;
                rushStartPoint = transform.position;
                if (animator) {
                    animator.SetBool("IsWalking", true);
                }
            }
        }
    }

    private void MoveIncline()
    {
        Vector3 targetDir = (new Vector3(GameStateManager.Instance.player.transform.position.x, GameStateManager.Instance.player.transform.position.y, 0f) - transform.position).normalized;
        Vector3 targetPos = targetDir * moveSpeed * 10f * Time.deltaTime + transform.position;
        Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
        SetDirection(targetPos);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
        if (animator) {
            animator.SetBool("IsWalking", true);
        }
    }

    private void MoveVertical()
    {
        //Vector3 targetPos = new Vector3(transform.position.x, GameStateManager.Instance.player.transform.position.y, 0f);

        Vector3 targetDir = (new Vector3(transform.position.x, GameStateManager.Instance.player.transform.position.y, 0f) - transform.position).normalized;
        Vector3 targetPos = targetDir * moveSpeed * 10f * Time.deltaTime + transform.position;
        Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
        SetDirection(targetPos);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
        if (animator) {
            animator.SetBool("IsWalking", true);
        }
    }

    private void MoveHorizontal()
    {
        //Vector3 targetPos = new Vector3(GameStateManager.Instance.player.transform.position.x, transform.position.y, 0f);

        Vector3 targetDir = (new Vector3(GameStateManager.Instance.player.transform.position.x, transform.position.y, 0f) - transform.position).normalized;
        Vector3 targetPos = targetDir * moveSpeed * 10f * Time.deltaTime + transform.position;
        Debug.DrawLine(transform.position, targetPos, Color.red, 0.1f);
        SetDirection(targetPos);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
        if (animator) {
            animator.SetBool("IsWalking", true);
        }
    }

    public void SetEnemyActivated(bool a)
    {
        activated = a;
        if (animator) {
            animator.SetTrigger("Activate");
        }
    }

    private void SetDirection(Vector3 targetPos)
    {
        if ((targetPos - transform.position).x >= 0) {
            // right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        } else {
            // left
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

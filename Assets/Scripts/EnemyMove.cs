using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public MoveIWanna moveIWanna;
    public MoveIsaac moveIsaac;
    public Teleport ownLevel;
    private Vector3 originPos;
    private Animator animator;
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        ownLevel.RegisterEnemy(this);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ownLevel != GameStateManager.Instance.player.GetComponent<PlayerController>().nowTeleport) {
            return;
        }
        if (!canMove) {
            return;
        }
        StateName gameState = GameStateManager.Instance.GetState();
        switch(gameState) {
            case StateName.IWanna:
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
                moveIWanna?.Move();
                break;
            case StateName.Isaac:
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
                moveIsaac?.Move();
                break;
            default:
                if (animator) {
                    animator.SetBool("IsWalking", false);
                }
                break;
        }
    }

    public void InitState()
    {
        Debug.Log("InitState");
        transform.position = originPos;
        moveIWanna?.InitState();
        moveIsaac?.InitState();
        if (animator) {
            animator.SetTrigger("InitState");
        }
    }

    public void StopMove()
    {
        Debug.Log("StopMoving");
        canMove = false;
    }

    public void StartMove()
    {
        Debug.Log("StartMoving");
        canMove = true;
    }
    
}

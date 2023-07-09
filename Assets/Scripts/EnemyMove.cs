using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public MoveIWanna moveIWanna;
    public MoveIsaac moveIsaac;
    public Teleport ownLevel;
    private Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        ownLevel.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (ownLevel != GameStateManager.Instance.player.GetComponent<PlayerController>().nowTeleport) {
            return;
        }
        StateName gameState = GameStateManager.Instance.GetState();
        switch(gameState) {
            case StateName.IWanna:
                GetComponent<Animator>()?.SetBool("IsWalking", false);
                moveIWanna?.Move();
                break;
            case StateName.Isaac:
                GetComponent<Animator>()?.SetBool("IsWalking", false);
                moveIsaac?.Move();
                break;
            default:
                GetComponent<Animator>()?.SetBool("IsWalking", false);
                break;
        }
    }

    public void InitState()
    {
        Debug.Log("InitState");
        transform.position = originPos;
        moveIWanna?.InitState();
        moveIsaac?.InitState();
    }
}

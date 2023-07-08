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
                moveIWanna?.Move();
                break;
            case StateName.Isaac:
                moveIsaac?.Move();
                break;
            default:
                break;
        }
    }
}

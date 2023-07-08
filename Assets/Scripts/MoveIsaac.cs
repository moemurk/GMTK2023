using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsaac : MonoBehaviour
{
    public MovetypeISaac moveType;
    public float moveSpeed;
    public float movementSmoothing;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        switch(moveType) {
            case MovetypeISaac.Vertical:
                MoveVertical();
                break;
            case MovetypeISaac.Horizontal:
                MoveHorizontal();
                break;
            case MovetypeISaac.None:
            default:
                break;
        }
    }

    private void MoveVertical()
    {
        Vector3 targetPos = new Vector3(transform.position.x, GameStateManager.Instance.player.transform.position.y + 1f, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
    }

    private void MoveHorizontal()
    {
        Vector3 targetPos = new Vector3(GameStateManager.Instance.player.transform.position.x, transform.position.y, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
    }
}

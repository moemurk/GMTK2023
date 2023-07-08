using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsaac : MonoBehaviour
{
    public MovetypeISaac moveType;
    public float moveSpeed;
    public float movementSmoothing;
    private bool activated = false;
    private Vector3 velocity;
    [Header("Rush")] public float coolTime;
    public float rushingDistance;
    public float rushSpeed;
    private float remainCoolTime;
    private bool isRushing;
    private Vector3 rushDirection;
    private Vector3 rushStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        remainCoolTime = coolTime;
        isRushing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (!activated) {
            return;
        }
        switch(moveType) {
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
                break;
        }
    }

    private void MoveRush()
    {
        if (isRushing) {
            Vector3 targetPos = rushDirection * rushSpeed * Time.deltaTime * 10f + transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, movementSmoothing, Mathf.Infinity);
            if (Vector3.Distance(transform.position, rushStartPoint) >= rushingDistance) {
                Debug.Log("End Rush");
                isRushing = false;
                remainCoolTime = coolTime;
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
            }
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

    public void SetEnemyActivated(bool a)
    {
        activated = a;
    }
}

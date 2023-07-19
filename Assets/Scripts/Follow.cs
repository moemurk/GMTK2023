using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;
    public float movementSmoothing = 0.3f;
    private Vector3 velocity;

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) {
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, GameStateManager.Instance.player.transform.position, ref velocity, movementSmoothing, Mathf.Infinity);
    }
}

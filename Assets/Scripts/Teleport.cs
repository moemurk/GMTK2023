using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport target;
    public bool canChangeStateNextLevel;
    public Transform targetCameraPos;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger Enter");
        if (col.CompareTag("Player") && col.GetComponent<PlayerController>().nowTeleport != this) {
            Debug.Log("comfirm player");
            col.GetComponent<PlayerController>().nowTeleport = target;
            SceneManager.Instance.Teleport(target.transform, canChangeStateNextLevel, targetCameraPos);
        }
    }
}

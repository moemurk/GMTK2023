using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHalo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            col.GetComponent<MoveIsaac>()?.SetEnemyActivated(true);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        col.GetComponent<MoveIsaac>()?.SetEnemyActivated(false);
    }
}

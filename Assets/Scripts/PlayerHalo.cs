using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHalo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            col.GetComponent<MoveIsaac>()?.SetEnemyActivated(true);
            if (col.GetComponent<Unit>()){
                col.GetComponent<Unit>().DisplayHpBar(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        col.GetComponent<MoveIsaac>()?.SetEnemyActivated(false);
        if (col.CompareTag("Enemy")) {
            if (col.GetComponent<Unit>()){
                col.GetComponent<Unit>().DisplayHpBar(false);
            }
        }
    }
}

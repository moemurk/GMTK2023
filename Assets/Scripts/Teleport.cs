using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport target;
    public bool canChangeStateNextLevel;
    public Transform targetCameraPos;
    public Transform targetCameraPos_Isaac;
    private List<EnemyMove> enemies = new List<EnemyMove>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PlayerController>().nowTeleport != this) {
            col.GetComponent<PlayerController>().nowTeleport.InitEnemies();
            col.GetComponent<PlayerController>().nowTeleport = target;
            SceneManager.Instance.Teleport(target.transform, canChangeStateNextLevel, targetCameraPos);
        }
    }

    public void RegisterEnemy(EnemyMove enemy)
    {
        enemies.Add(enemy);
    }

    public void InitEnemies()
    {
        Debug.Log("InitEnemies");
        foreach (EnemyMove e in enemies) {
            e.InitState();
        }
    }
}

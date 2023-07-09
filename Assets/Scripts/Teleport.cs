using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport target;
    public bool canChangeStateNextLevel;
    public Transform targetCameraPos;
    public Transform targetCameraPos_Isaac;
    public bool needKey;
    public Sprite notOpen;
    public Sprite open;
    public Teleport endPoint;
    private List<EnemyMove> enemies = new List<EnemyMove>();
    private bool hasKey = false;

    void Start()
    {
        if (needKey) {
            GetComponent<SpriteRenderer>().sprite = notOpen;
        } else {
            GetComponent<SpriteRenderer>().sprite = open;
        }
        
    }

    public void GetKey()
    {
        hasKey = true;
        // maybe Sprite Change?
        GetComponent<SpriteRenderer>().sprite = open;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.GetComponent<PlayerController>().nowTeleport != this) {
            if (needKey && !hasKey) {
                return;
            }
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
        endPoint?.InitEnemies();
        hasKey = false;
        if (needKey) {
            GetComponent<SpriteRenderer>().sprite = notOpen;
        } else {
            GetComponent<SpriteRenderer>().sprite = open;
        }
        foreach (EnemyMove e in enemies) {
            e.gameObject.SetActive(true);
            e.GetComponent<Unit>()?.InitState();
            e.InitState();
        }
    }

    public void OpenTheDoor()
    {
        endPoint.GetKey();
    }
}

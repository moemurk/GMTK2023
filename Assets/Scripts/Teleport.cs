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
    public Teleport endPoint;
    private List<EnemyMove> enemies = new List<EnemyMove>();
    private bool hasKey = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (needKey) {
            if (animator) {
                animator.SetTrigger("InitState");
            }
        } else {
            Debug.Log("needKey");
            if (animator) {
                animator.SetTrigger("Open");
                Debug.Log("set Open");
            }
        }
    }

    public void GetKey()
    {
        hasKey = true;
        if (animator) {
            animator.SetTrigger("Open");
        }
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
            if (animator) {
                animator.SetTrigger("InitState");
            }
        } else {
            if (animator) {
                animator.SetTrigger("Open");
                Debug.Log("set Open");
            }
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

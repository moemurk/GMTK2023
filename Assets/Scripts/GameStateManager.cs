using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateManager : Singleton<GameStateManager>
{
    public float changeDuration;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI deathUI;
    public GameObject player;
    private bool isPlaying = false;
    public bool canChange = false;
    public StateName stateName = StateName.IWanna;
    public float remainTimeInState;
    private List<GameObject> attachItems = new List<GameObject>();
    private int deathNum = 0;


    public void StartPlay()
    {
        isPlaying = true;
        player.GetComponent<PlayerController>().SetMoveState(true);
    }

    public void AddItem(GameObject item)
    {
        attachItems.Add(item);
        player.GetComponent<PlayerController>().nowTeleport.OpenTheDoor();
    }

    void Start()
    {
        remainTimeInState = changeDuration;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void InitState()
    {
        remainTimeInState = changeDuration;
        stateName = StateName.IWanna;
        // change g
        player.GetComponent<PlayerController>().SwitchGravity(true);
        Camera.main.transform.position = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos.position;
        Camera.main.transform.rotation = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos.rotation;
        Camera.main.orthographic = true;
        foreach (GameObject i in attachItems) {
            Destroy(i);
        }
    }

    void Update()
    {
        if (isPlaying) {
            if (canChange) {
                remainTimeInState -= Time.deltaTime;
                countDown.SetText(decimal.Round(decimal.Parse(remainTimeInState.ToString()), 1).ToString());
                if (remainTimeInState < 0) {
                    ChangeState();
                }
            } else {
                countDown.SetText("I Wanna");
            }
        }
    }

    public StateName GetState()
    {
        return stateName;
    }

    void ChangeState() {
        if (stateName == StateName.IWanna) {
            stateName = StateName.Isaac;
            // change g
            player.GetComponent<PlayerController>().SwitchGravity(false);
            Camera.main.transform.position = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos_Isaac.position;
            Camera.main.transform.rotation = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos_Isaac.rotation;
            Camera.main.orthographic = false;
        } else {
            stateName = StateName.IWanna;
            // change g
            player.GetComponent<PlayerController>().SwitchGravity(true);
            Camera.main.transform.position = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos.position;
            Camera.main.transform.rotation = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos.rotation;
            Camera.main.orthographic = true;
        }
        remainTimeInState = changeDuration;
    }

    public void SetStateCanChange(bool can)
    {
        canChange = can;
        // Default state should be I wanna
        if (!canChange && stateName == StateName.Isaac) {
            ChangeState();
        }
    }

    public void Dead()
    {
        Debug.Log("Dead");
        deathNum++;
        deathUI.text = "Death:" + deathNum.ToString();
        Spawn();
    }

    public void Spawn()
    {
        Teleport t = player.GetComponent<PlayerController>().nowTeleport; 
        t.InitEnemies();
        SceneManager.Instance.Teleport(t.target.transform, t.canChangeStateNextLevel, t.targetCameraPos);
        player.GetComponent<Unit>().InitState();
    }
}

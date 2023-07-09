using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateManager : Singleton<GameStateManager>
{
    public int changeDuration;
    public TextMeshProUGUI countDown;
    public GameObject player;
    private bool isPlaying = true;
    private bool canChange = false;
    private StateName stateName = StateName.IWanna;
    private float remainTimeInState;

    void Start()
    {
        remainTimeInState = changeDuration;
        player = GameObject.FindGameObjectWithTag("Player");
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

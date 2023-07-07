using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateManager : Singleton<GameStateManager>
{
    public int changeDuration;
    public TextMeshProUGUI countDown;
    private bool isPlaying = true;
    private bool canChange = true;
    private StateName stateName = StateName.IWanna;
    private float remainTimeInState;

    void Start()
    {
        remainTimeInState = changeDuration;
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
            }
        }
    }

    public StateName GetState()
    {
        return stateName;
    }

    void ChangeState() {
        if (stateName == StateName.IWanna) {
            Debug.Log("Changing to Isaac");
            stateName = StateName.Isaac;
            // change g
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SwitchGravity(false);
        } else {
            Debug.Log("Changing to Iwanna");
            stateName = StateName.IWanna;
            // change g
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SwitchGravity(true);
        }
        remainTimeInState = changeDuration;
    }
}

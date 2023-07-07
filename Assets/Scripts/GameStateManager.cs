using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public int changeDuration;
    private bool isPlaying = true;
    private bool canChange = true;
    private StateName stateName = StateName.IWanna;

    void Start()
    {
        StartCoroutine(StartChangingState());
    }

    public StateName GetState()
    {
        return stateName;
    }

    IEnumerator StartChangingState()
    {
        yield return new WaitForSeconds(changeDuration);
        while (canChange) {
            // change
            ChangeState();
            yield return new WaitForSeconds(changeDuration);
        }
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
    }
}

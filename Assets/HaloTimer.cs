using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaloTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    private float timeRemain;
    private float duration;
    private StateName stateNameCurrent = StateName.IWanna;

    public void Start()
    {   
        duration = FindObjectOfType<GameStateManager>().changeDuration;
        timeRemain = duration;
        stateNameCurrent = StateName.IWanna;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemain = FindObjectOfType<GameStateManager>().remainTimeInState;
        stateNameCurrent = FindAnyObjectByType<GameStateManager>().stateName;
        image.fillAmount = timeRemain / duration;
        if (stateNameCurrent == StateName.IWanna){
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else {
            image.color = new Color(0.3971f, 0.7319f, 0.9905f, 1.0f);
        }
    }
}

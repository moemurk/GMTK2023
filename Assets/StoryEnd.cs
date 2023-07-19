using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEnd : MonoBehaviour
{
    public GameObject countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerStoryEnd()
    {
        countDown.SetActive(true);
        GameStateManager.Instance.StartPlay();
        Destroy(gameObject);
    }
}

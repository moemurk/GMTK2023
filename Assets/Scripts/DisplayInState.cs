using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInState : MonoBehaviour
{
    public StateName displayState;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.GetState() == displayState) {
            if (sr.color.a == 0f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        } else {
            if (sr.color.a == 1f) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            }
        }
    }
}

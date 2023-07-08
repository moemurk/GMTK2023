using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIWanna : MonoBehaviour
{
    public MovetypeIWanna moveType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        switch(moveType) {
            case MovetypeIWanna.None:
            default:
                break;
        }
    }
}

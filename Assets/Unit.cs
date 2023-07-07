using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int hp;
    public Slider slice;
    private int curHp;

    void UpdateUI()
    {
        if (curHp <= 0) {
            slice.value = 0;
        } else {
            slice.value = curHp / hp;
        }
        
    }
}

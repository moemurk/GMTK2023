using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int hp;
    public Slider slice;
    public float coolTime;
    public SpriteRenderer img;
    private float remainCoolTime;
    private int curHp;

    void Start()
    {
        curHp = hp;
    }

    public void InitState()
    {
        curHp = hp;
        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log(curHp);
        if (curHp <= 0) {
            slice.value = 0;
        } else {
            slice.value = (float)curHp / (float)hp;
        }
    }

    void Update()
    {
        if (remainCoolTime > 0f) {
            remainCoolTime -= Time.deltaTime;
            if (remainCoolTime <= 0f) {
                img.color = Color.white;
            }
        }
    }

    // return true if dead
    // return false if not dead
    public bool TakeDamage(int damage)
    {
        Debug.Log("damage:" + damage.ToString());
        Debug.Log("curHp:" + damage.ToString());
        if (remainCoolTime > 0f) {
            return false;
        }
        remainCoolTime = coolTime;
        img.color = Color.red;
        curHp -= damage;
        if (curHp <= 0) {
            curHp = 0;
            UpdateUI();
            return true;
        } else {
            UpdateUI();
            return false;
        }
    }
}

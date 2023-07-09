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
    public bool hideHpBarInIsaac;
    private float remainCoolTime;
    private int curHp;
    private bool canBeAttack = true;

    void Start()
    {
        curHp = hp;
        if (hideHpBarInIsaac) {
            canBeAttack = false;
            DisplayHpBar(false);
        }
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
        if (hideHpBarInIsaac) {
            if (GameStateManager.Instance.GetState() == StateName.IWanna) {
                DisplayHpBar(false);
            } else if (GetComponent<MoveIsaac>() && GetComponent<MoveIsaac>().activated) {
                // if been activated
                DisplayHpBar(true);
            }
        }
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
        if (!canBeAttack) {
            return false;
        }
        remainCoolTime = coolTime;
        //img.color = Color.red;
        img.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
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

    public void DisplayHpBar(bool b)
    {
        if (!hideHpBarInIsaac) {
            return;
        }
        canBeAttack = b;
        if (b) {
            slice.gameObject.SetActive(true);
        } else {
            slice.gameObject.SetActive(false);
        }
    }
}

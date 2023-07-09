using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public TriggerType triggerType_IWanna;
    public TriggerType triggerType_Isaac;
    public int damage;
    [Header("Item")]public Sprite key;

    void OnTriggerStay2D(Collider2D col)
    {
        TriggerType curType = GameStateManager.Instance.GetState() == StateName.IWanna ? triggerType_IWanna : triggerType_Isaac;
        Unit unit = col.GetComponent<Unit>();
        if (!unit) {
            return;
        }
        Debug.Log(curType);
        switch(curType) {
            case TriggerType.Damage:
                TriggerDamage(col);
                break;
            case TriggerType.OneHit:
                TriggerOneHit(col);
                break;
            case TriggerType.Item:
                TriggerItem(col);
                break;
            case TriggerType.Heal:
                TriggerHeal(col);
                break;
            case TriggerType.None:
            default:
                break;
        }
    }

    void TriggerDamage(Collider2D col)
    {
        Debug.Log("Damage");
        if (col.GetComponent<Unit>().TakeDamage(damage)) {
            GameStateManager.Instance.Dead();
        }
        
    }

    void TriggerOneHit(Collider2D col)
    {
        Debug.Log("OneHit");
        col.GetComponent<Unit>().TakeDamage(10000);
        GameStateManager.Instance.Dead();
    }

    void TriggerItem(Collider2D col)
    {
        Debug.Log("Item");
        gameObject.SetActive(false);
        GameObject follower = new GameObject("following item");
        follower.transform.SetParent(null);
        follower.transform.localScale = transform.localScale;
        follower.transform.position = GameStateManager.Instance.player.transform.position;
        Follow f = follower.AddComponent<Follow>();
        SpriteRenderer sr = follower.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "middle";
        sr.sprite = key;
        f.SetTarget(GameStateManager.Instance.player);
        GameStateManager.Instance.AddItem(follower);
    }

    void TriggerHeal(Collider2D col)
    {
        gameObject.SetActive(false);
        Unit u = col.GetComponent<Unit>();
        if (u) {
            u.InitState();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public TriggerType triggerType_IWanna;
    public TriggerType triggerType_Isaac;
    public int damage;

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
    }
}

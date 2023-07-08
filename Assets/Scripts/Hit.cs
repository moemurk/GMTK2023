using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public TriggerType triggerType_IWanna;
    public TriggerType triggerType_Isaac;

    void OnTriggerEnter2D(Collider2D col)
    {
        TriggerType curType = GameStateManager.Instance.GetState() == StateName.IWanna ? triggerType_IWanna : triggerType_Isaac;
        Debug.Log(curType);
        if (!col.CompareTag("Player")) {
            return;
        }
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
    }

    void TriggerOneHit(Collider2D col)
    {
        Debug.Log("OneHit");
    }

    void TriggerItem(Collider2D col)
    {
        Debug.Log("Item");
    }
}

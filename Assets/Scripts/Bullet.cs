using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) {
            return;
        }
        Unit u = col.GetComponent<Unit>();
        if (u) {
            Debug.Log("Bullet hit:" + col.name);
            if (u.TakeDamage(damage)) {
                if(!col.CompareTag("Player")) {
                    Debug.Log("set in activate");
                    col.gameObject.SetActive(false);
                }
            }
            Destroy(this.gameObject);
        }
        if (col.gameObject.layer == 6) {
            Destroy(gameObject);
        }
    }
}

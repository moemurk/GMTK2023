using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPlayer : MonoBehaviour
{
    public Sprite[] openingImgs;
    public Image img;
    public GameObject countDown;
    private int cur;
    // Start is called before the first frame update
    void Start()
    {
        cur = 0;
        countDown.SetActive(false);
        img.sprite = openingImgs[cur++];
        StartCoroutine(Next());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Next()
    {
        for (; cur < openingImgs.Length; cur++) {
            while(!Input.GetMouseButtonUp(0)) {
                yield return null;
            }
            img.sprite = openingImgs[cur];
            yield return null;
        }
        while(!Input.GetMouseButtonUp(0)) {
            yield return null;
        }
        countDown.SetActive(true);
        GameStateManager.Instance.StartPlay();
        Destroy(gameObject);
    }
}

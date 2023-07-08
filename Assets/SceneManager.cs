using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public float fadeDuration;
    public CanvasGroup fadeCanvasGroup;

    public void Teleport(Transform target, bool canChangeState, Transform targetCameraPos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log(player);
        if (!player.GetComponent<PlayerController>().CanBeTeleport()) {
            Debug.Log("Can't be teleport");
            return;
        }
        StartCoroutine(TeleportCoroutine(target, canChangeState, targetCameraPos));
    }
    
    private IEnumerator TeleportCoroutine(Transform target, bool canChangeState, Transform targetCameraPos)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().SetMoveState(false);
        //yield return CloseCurtain();
        player.transform.position = target.position;
        GameStateManager.Instance.SetStateCanChange(canChangeState);
        Camera.main.transform.position = targetCameraPos.position;
        //yield return OpenCurtain();
        Debug.Log("endFade0");
        player.GetComponent<PlayerController>().SetMoveState(true);
        yield return null;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Debug.Log("FadeStart");
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            Debug.Log("FadeIng");
            Debug.Log(fadeCanvasGroup.alpha);
            Debug.Log(targetAlpha);
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("FadeEnd");
    }

    private IEnumerator CloseCurtain()
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - 1) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, 1f))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, 1, speed * Time.deltaTime);
            if (fadeCanvasGroup.alpha >= 1f) {
                fadeCanvasGroup.alpha = 1f;
            }
            yield return null;
        }
    }

    private IEnumerator OpenCurtain()
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - 0) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, 0f))
        {
            Debug.Log("speed");
            Debug.Log(speed);
            Debug.Log("alpha before");
            Debug.Log(fadeCanvasGroup.alpha);
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, 1, speed * Time.deltaTime);
            Debug.Log("alpha after");
            Debug.Log(fadeCanvasGroup.alpha);
            if (fadeCanvasGroup.alpha <= 0f) {
                Debug.Log("alpha process");
                fadeCanvasGroup.alpha = 0f;
            }
            yield return null;
        }
    }
}

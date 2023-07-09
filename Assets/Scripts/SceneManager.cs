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
        if (!player.GetComponent<PlayerController>().CanBeTeleport()) {
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
        Camera.main.transform.rotation = player.GetComponent<PlayerController>().nowTeleport.targetCameraPos.rotation;
        Camera.main.orthographic = true;
        //yield return OpenCurtain();
        player.GetComponent<PlayerController>().SetMoveState(true);
        yield return null;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
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
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, 1, speed * Time.deltaTime);
            if (fadeCanvasGroup.alpha <= 0f) {
                fadeCanvasGroup.alpha = 0f;
            }
            yield return null;
        }
    }
}

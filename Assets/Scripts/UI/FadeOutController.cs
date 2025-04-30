using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class FadeOutController : MonoBehaviour
{
    public Image image;

    public void OnFadeStart()
    {
        StartCoroutine(FadingOut());
    }

    protected IEnumerator FadingOut()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 2f; // 총 소요 시간

        Color temp = image.color;

        while (elapsedTime <= fadedTime)
        {
            temp.a = Mathf.Lerp(0f, 1f, elapsedTime / fadedTime);
            image.color = temp;

            elapsedTime += Time.deltaTime;
            Debug.Log("Fade Out 중...");
            yield return null;
        }
        temp.a = 1;
        image.color = temp;

        Debug.Log("Fade Out 끝");

        yield break;
    }
}

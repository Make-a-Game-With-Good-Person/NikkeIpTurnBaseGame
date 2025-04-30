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
        float elapsedTime = 0f; // ���� ��� �ð�
        float fadedTime = 2f; // �� �ҿ� �ð�

        Color temp = image.color;

        while (elapsedTime <= fadedTime)
        {
            temp.a = Mathf.Lerp(0f, 1f, elapsedTime / fadedTime);
            image.color = temp;

            elapsedTime += Time.deltaTime;
            Debug.Log("Fade Out ��...");
            yield return null;
        }
        temp.a = 1;
        image.color = temp;

        Debug.Log("Fade Out ��");

        yield break;
    }
}

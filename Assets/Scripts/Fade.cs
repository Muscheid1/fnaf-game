using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float fadeDuration = 1f;
    // Start is called before the first frame update
    public void FadeToBlack()
    {
        StartCoroutine(FadeEnum(0f, 1f));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadeEnum(1f, 0f));
    }

    private IEnumerator FadeEnum(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = GetComponent<Image>().color;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / fadeDuration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            GetComponent<Image>().color = color;
            yield return null;
        }
        color.a = endAlpha;
        GetComponent<Image>().color = color;
    }
}

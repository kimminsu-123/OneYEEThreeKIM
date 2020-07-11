using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public RectTransform showTransform;
    public RectTransform hideTransform;

    private Coroutine currCoroutine;

    public void ShowBtn(RectTransform from)
    {
        if (currCoroutine != null)
        {
            StopCoroutine(currCoroutine);
            currCoroutine = null;
        }


        currCoroutine = StartCoroutine(ShowAndHide(from, true));
    }

    public void HideBtn(RectTransform from)
    {
        if (currCoroutine != null)
        {
            StopCoroutine(currCoroutine);
            currCoroutine = null;
        }

        currCoroutine = StartCoroutine(ShowAndHide(from, false));
    }

    private IEnumerator ShowAndHide(RectTransform from, bool isShow)
    {
        float t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime;
            if (isShow)
            {
                from.anchoredPosition = Vector3.Lerp(from.anchoredPosition, showTransform.anchoredPosition, t);
            }
            else
            {
                from.anchoredPosition = Vector3.Lerp(from.anchoredPosition, hideTransform.anchoredPosition, t);
            }
            yield return null;
        }
    }

}

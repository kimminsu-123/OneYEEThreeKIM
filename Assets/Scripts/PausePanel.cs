using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public float speed;

    public RectTransform optionPanelTr;
    public RectTransform pausePanelTr;

    public RectTransform hideTransform;
    public RectTransform showTransform;

    public void Resume()
    {
        GameManager.Instance.Pause = false;
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowBtn(RectTransform from)
    {
        StartCoroutine(ShowAndHide(from, true));
    }

    public void HideBtn(RectTransform from)
    {
        StartCoroutine(ShowAndHide(from, false));
    }

    public void ShowAndHideOption(RectTransform from, bool isShow)
    {
        StartCoroutine(ShowAndHide(from, isShow));
    }

    private IEnumerator ShowAndHide(RectTransform from, bool isShow)
    {
        float t = 0f;
        while(t <= 1f)
        {
            t += Time.deltaTime * speed;
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

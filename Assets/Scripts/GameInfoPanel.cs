using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoPanel : MonoBehaviour
{
    public Text clockTxt;

    public Sprite[] TimerImages;
    public Image timerImage;

    public float lerpTime = 0.5f;

    private int timerIndex = 0;

    public void SetClockTime(int hour, int minute)
    {
        clockTxt.text = $"{hour.ToString("00")} : {minute.ToString("00")}";
    }

    public void SetTimeDayOrNight()
    {
        ChangeWithFade();
    }

    private void UpdateTimerImage()
    {
        timerIndex = (int)Mathf.Repeat(timerIndex + 1, TimerImages.Length);
        timerImage.sprite = TimerImages[timerIndex];
    }

    private void ChangeWithFade()
    {
        StartCoroutine(FadeGroup(1f, 0f, lerpTime));
        UpdateTimerImage();
        StartCoroutine(FadeGroup(0f, 1f, lerpTime));
    }

    private IEnumerator FadeGroup(float start, float end, float lerpTime)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            SetAlpha(timerImage, currentValue);

            float alpha = timerImage.color.a;
            alpha -= Time.deltaTime * GameManager.Instance.TimeScale;

            if (percentageComplete >= 1f) break;

            yield return new WaitForEndOfFrame();
        }
    }

    private void SetAlpha(Image img, float value)
    {
        Color newColor = img.color;
        newColor.a = value;
        img.color = newColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Image dashGauge;
    [SerializeField]
    private Image hungryGauge;

    [SerializeField]
    private Image dashIcon;
    [SerializeField]
    private Image hungryIcon;


    [SerializeField]
    private Sprite dashZeroImg;
    [SerializeField]
    private Sprite hungryZeroImg;

    private Sprite dashFullImg;
    private Sprite hungryFullImg;


    private void Awake()
    {
        dashFullImg = dashIcon.sprite;
        hungryFullImg = hungryIcon.sprite;
    }

    public void UpdateDashGauge(float nomalDashValue)
    {
        if (dashGauge == null)
            return;

        dashGauge.fillAmount = nomalDashValue;
        dashIcon.sprite = dashGauge.fillAmount <= 0f ? dashZeroImg : dashFullImg;
    }

    public void UpdateHungryGauge(float nomalHungryValue)
    {
        if (hungryGauge == null)
            return;

        hungryGauge.fillAmount = nomalHungryValue;
        hungryIcon.sprite = hungryGauge.fillAmount <= 0f ? hungryZeroImg : hungryFullImg;
    }
}

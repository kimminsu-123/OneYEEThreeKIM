using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayPanel : MonoBehaviour
{
    [SerializeField]
    private Image eatDelayGauge;

    public void OnEatDelay(ItemInfo info)
    {
        eatDelayGauge.GetComponent<EatDelayController>().SetTime(info);
        eatDelayGauge.gameObject.SetActive(true);
    }
}

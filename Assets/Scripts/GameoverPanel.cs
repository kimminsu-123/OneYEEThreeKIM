using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    public Text playTimeText;
    public string content;
    public void UpdatePlayTime(string addStr)
    {
        playTimeText.text = $"{content} : {addStr}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Slider BGMSlider;
    public Text BGMText;

    public Slider effectSlider;
    public Text effectText;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        BGMSlider.value = AudioManager.Instance.sourceBGM.volume;
        effectSlider.value = AudioManager.Instance.source.volume;

        UpdateText();
    }

    private void UpdateText()
    {
        BGMText.text = $"{(BGMSlider.value * 100f).ToString("00.0")}%";
        effectText.text = $"{(effectSlider.value * 100f).ToString("00.0")}%";
    }

    public void OnBGMChange()
    {
        AudioManager.Instance.SetVolumeBGM(BGMSlider.value);
        UpdateText();
    }

    public void OnEffectChange()
    {
        AudioManager.Instance.SetVolumeEffectSound(effectSlider.value);
        UpdateText();
    }
}

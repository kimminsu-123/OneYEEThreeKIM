using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    public AudioClip mainBGM;

    private SourceEffect effectSource;
    private SourceBGM BGMSource;
    private void Awake()
    {
        effectSource = GameObject.FindWithTag("EffectSource").GetComponent<SourceEffect>();
        BGMSource = GameObject.FindWithTag("BGMSource").GetComponent<SourceBGM>();
    }

    void Start()
    {
        BGMSource.Play(mainBGM);
    }

    public void OneShotPlay(AudioClip clip)
    {
        effectSource.OneShotPlay(clip);
    }
}

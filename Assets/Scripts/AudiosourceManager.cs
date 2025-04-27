using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosourceManager : MonoBehaviour
{
    public static AudiosourceManager instance { get; private set; }

    public AudioSource EffectAudio;

    public AudioClip correctEffect;
    public AudioClip failEffect;
    public AudioClip ClickEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
            
        }
        
    }
    public void Clicksound()
    {
       EffectAudio.clip = ClickEffect;
       EffectAudio.Play();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Clicksound();
        }
    }

    public void PlayCorrectSE()
    {
        EffectAudio.clip = correctEffect;
        EffectAudio.Play();
    }
    public void PlayFailSE()
    {
        EffectAudio.clip = failEffect;
        EffectAudio.Play();
    }
}

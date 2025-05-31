using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIsettingmanager : MonoBehaviour
{
    [Header("Sound")]
    public Button S_mutebutton;
    public Slider S_slider;
    public GameObject X_Sound;
    public bool S_mute;

    public Button M_mutebutton;
    public Slider M_slider;
    public GameObject X_Music;
    public bool M_mute;

    [Header("Gamemode")]
    public TextMeshProUGUI Gamemodetext;
    public string mode;
    public TextMeshProUGUI Easybutton, Normalbutton, Hardbutton;

    private void Start()
    {
        S_slider.value = PlayerPrefs.GetFloat("Soundvalue");
        M_slider.value = PlayerPrefs.GetFloat("Musicvalue");
        Gamemodetext.text = "(" + PlayerPrefs.GetString("Mode") + ")";
        mode = PlayerPrefs.GetString("Mode");
        S_mutebutton.onClick.AddListener(MuteSound);
        M_mutebutton.onClick.AddListener(MuteMusic);
        S_slider.onValueChanged.AddListener(SoundChange);
        M_slider.onValueChanged.AddListener(MusicChange);
        MuteMusic();
        MuteSound();
        SoundChange(S_slider.value);
        MusicChange(M_slider.value);
    }

    

    private void MuteSound()
    {
        if (AudiosourceManager.instance == null) { return; }
        if (S_mute)
        {
            AudiosourceManager.instance.EffectAudio.mute = false;
            S_mute = false;
            X_Sound.SetActive(false);
        }
        else
        {
            S_slider.value = 0;
            AudiosourceManager.instance.EffectAudio.mute = true;
            S_mute = true;
            X_Sound.SetActive(true);
        };
    }
    private void SoundChange(float value)
    {
        AudiosourceManager.instance.EffectAudio.volume = value;
        if(value == 0)
        {
            MuteSound();
        }
        else
        {
            AudiosourceManager.instance.EffectAudio.mute = false;
            S_mute = false;
            X_Sound.SetActive(false);
        }
        PlayerPrefs.SetFloat("Soundvalue",value);
    }
    private void MusicChange(float value)
    {
        AudiosourceManager.instance.musicAudio.volume = value;
        if (value == 0)
        {
            MuteMusic();
        }
        else
        {
            AudiosourceManager.instance.musicAudio.mute = false;
            M_mute = false;
            X_Music.SetActive(false);
        }
        PlayerPrefs.SetFloat("Musicvalue", value);

    }
    private void MuteMusic()
    {
        if (AudiosourceManager.instance == null) { return; }
        if (M_mute)
        {
            AudiosourceManager.instance.musicAudio.mute = false;
            M_mute = false;
            X_Music.SetActive(false);
        }
        else
        {
            M_slider.value = 0;
            AudiosourceManager.instance.musicAudio.mute = true;
            M_mute = true;
            X_Music.SetActive(true);
        }
    }

    public void SetGameMode(string mode)
    {
        PlayerPrefs.SetString("Mode", mode);
        this.mode = mode;
        Gamemodetext.text = "(" + mode + ")";
    }

    private void Update()
    {
        
        if(mode == "Easy")
        {
            Easybutton.fontStyle = FontStyles.Underline;
            Normalbutton.fontStyle &= ~FontStyles.Underline;
            Hardbutton.fontStyle &= ~FontStyles.Underline;
        }
        if(mode == "Normal")
        {
            Easybutton.fontStyle &= ~FontStyles.Underline;
            Normalbutton.fontStyle = FontStyles.Underline;
            Hardbutton.fontStyle &= ~FontStyles.Underline;
        }
        if(mode == "Hard")
        {
            Easybutton.fontStyle &= ~FontStyles.Underline;
            Normalbutton.fontStyle &= ~FontStyles.Underline;
            Hardbutton.fontStyle = FontStyles.Underline;
        }
    }


}

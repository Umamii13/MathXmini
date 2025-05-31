using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameUimanager : MonoBehaviour
{
    public Button Homebutton;
    public Button restartButton;
    public Button YesButton;
    public Button NoButton;
    public string sceneTarget;

    public GameObject yesornopanel;

    public TextMeshProUGUI mode;

    [Header("Setting")]
    public Button Settingbutton;
    public GameObject settingpanel;
    public Button soundButton;
    public Slider soundslider;
    public GameObject X_sound;
    public bool soundmute;
    public Button musicButton;
    public Slider musicslider;
    public GameObject X_music;
    public bool musicMute;

    private void Start()
    {
        mode.text = PlayerPrefs.GetString("Mode");
        soundslider.value = PlayerPrefs.GetFloat("Soundvalue");
        musicslider.value = PlayerPrefs.GetFloat("Musicvalue");
        Homebutton.onClick.AddListener(() => { ShowPanel("MenuScene");});
        restartButton.onClick.AddListener(() => { ShowPanel(SceneManager.GetActiveScene().name); });
        YesButton.onClick.AddListener(LoadScene);
        NoButton.onClick.AddListener(disablePanel);
        Settingbutton.onClick.AddListener(showsetting);
        musicButton.onClick.AddListener(MusicMute);
        soundButton.onClick.AddListener(SoundMute);
        soundslider.onValueChanged.AddListener(SoundChange);
        musicslider.onValueChanged.AddListener(MusicChange);
        SoundMute();
        MusicMute();
    }

    private void showsetting()
    {
        CalculateGamemanager gm = gameObject.GetComponent<CalculateGamemanager>();
        if (gm != null)
        {
            gm.clock.speed = 0;
            gm.gameStart = false;
        }

        SymboGamemanager sgm = gameObject.GetComponent<SymboGamemanager>();
        if (sgm != null)
        {
            sgm.clock.speed = 0;
            sgm.gameStart = false;
        }
        settingpanel.SetActive(true);
    }

    public void CloseSetting()
    {
        CalculateGamemanager gm = gameObject.GetComponent<CalculateGamemanager>();
        if (gm != null)
        {
            gm.clock.speed = 1;
            gm.gameStart = true;
            gm.RandomQuestion();
        }

        SymboGamemanager sgm = gameObject.GetComponent<SymboGamemanager>();
        if (sgm != null)
        {
            sgm.clock.speed = 1;
            gm.RandomQuestion();
            sgm.gameStart = true;
        }
        
        settingpanel.SetActive(false);
    }
    private void MusicChange(float value)
    {
        AudiosourceManager.instance.musicAudio.volume = value;
        if (value == 0)
        {
            MusicMute();
        }
        else
        {
            AudiosourceManager.instance.musicAudio.mute = false;
            musicMute = false;
            X_music.SetActive(false);
        }
        PlayerPrefs.SetFloat("Musicvalue", value);
    }

    private void SoundChange(float value)
    {
        AudiosourceManager.instance.EffectAudio.volume = value;
        if (value == 0)
        {
            SoundMute();
        }
        else
        {
            AudiosourceManager.instance.EffectAudio.mute = false;
            soundmute = false;
            X_sound.SetActive(false);
        }
        PlayerPrefs.SetFloat("Soundvalue", value);
    }

    public void ShowPanel(string target)
    {
        sceneTarget = target;
        yesornopanel.SetActive(true);
    }

    public void disablePanel()
    {
        yesornopanel.SetActive(false);
    }
    public void SoundMute()
    {
        if(AudiosourceManager.instance == null) { return; }
        if(soundmute)
        {
            AudiosourceManager.instance.EffectAudio.mute = false;
            soundmute = false;
            X_sound.SetActive(false);
        }
        else
        {
            soundslider.value = 0;
            AudiosourceManager.instance.EffectAudio.mute = true;
            soundmute = true;
            X_sound.SetActive(true);
        }
    }
    public void MusicMute()
    {
        if(AudiosourceManager.instance == null) { return; }
        if(musicMute)
        {
            AudiosourceManager.instance.musicAudio.mute = false;
            musicMute = false;
            X_music.SetActive(false);
        }
        else
        {
            musicslider.value = 0;
            AudiosourceManager.instance.musicAudio.mute = true;
            musicMute = true;
            X_music.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneTarget);
    }
    public void LoadScenes(string name)
    {
        SceneManager.LoadScene(name);
    }
}

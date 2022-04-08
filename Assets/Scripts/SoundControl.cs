using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {

    public static SoundControl Instance;

    //public AudioClip loseClip;
    public AudioClip bonusClip;
    
    public AudioClip musicClip;
    public AudioClip buttonClickClip;
    //public AudioClip punchClip;

    private AudioSource audioS;
    private AudioClip tempAudioClip;
    bool sound, music, vibration;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        audioS = GetComponent<AudioSource>();
        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        music = PlayerPrefs.GetInt(ConstantValue.music, 1) == 1;
        vibration = PlayerPrefs.GetInt(ConstantValue.vibration, 1) == 1;
        tempAudioClip = musicClip;
        PlayMusic(0);
        DontDestroyOnLoad(gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void PlayMusic(int statusIndex) {
        if (statusIndex == 1) {
            tempAudioClip = musicClip;
        }
        music = PlayerPrefs.GetInt(ConstantValue.music, 1) == 1;
        if (music) {
            audioS.clip = tempAudioClip;
            audioS.Play();
        } else {
            audioS.Stop();
        }
    }

    public void ChangeBGMusic(AudioClip userChooseMusic) {
        music = PlayerPrefs.GetInt(ConstantValue.music, 1) == 1;
        if (music) {
            tempAudioClip = userChooseMusic;
            PlayMusic(0);
        }
    }


    public void PlayClick() {
        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        if (sound) {
            audioS.PlayOneShot(buttonClickClip);
        }
    }

    public void PlayLose() {
        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        if (sound) {
            //audioS.PlayOneShot(loseClip);
        }
    }

    public void PlayBonus() {
        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        if (sound) {
            audioS.PlayOneShot(bonusClip);
        }
    }

    /*public void PlayPunch() {
        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        if (sound && !GlobalValue.isGameOver && !GlobalValue.isGamePause) {
            audioS.PlayOneShot(punchClip);
        }
    }*/

    public void Vibration() {
        vibration = PlayerPrefs.GetInt(ConstantValue.vibration, 1) == 1;
        if (vibration) {
            Handheld.Vibrate();
        }
    }


    private int InitialHour() {
        DateTime today = DateTime.Now;
        int hour = today.Hour;
        if (hour < 14) {
            return 14 - hour;
        } else {
            return 14 + 24 - hour;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class HomeSceneControl : MonoBehaviour {

    public TextMeshProUGUI bestScoreText;
    //public TextMeshProUGUI priceText;

    //public RawImage bgRowImage;
    //public Texture2D[] defaultImage;
    //public GameObject messageObject;

    //public GameObject changeSongButton;

    [Header("Group UI")]
    public GameObject mainUI; 
    public GameObject settingUI;
    //public GameObject shopUI;

    [Header("Button Image")]
    public Image soundButton;
    public Image musicButton;
    public Image vibrationButton;
    //public Image notificationButton;

    [Header("Sprite")]
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite vibrationOnSprite;
    public Sprite vibrationOffSprite;
    //public Sprite notificationOnSprite;
    //public Sprite notificationOffSprite;

    //private int registrationId = 1;
    private int totalPrice;
    //private string savePath;
    private bool sound;
    private bool music;
    private bool vibration;
    private bool notification;

    private void Start() {
        
        mainUI.SetActive(true);
        settingUI.SetActive(false);
        //shopUI.SetActive(false);
        //messageObject.SetActive(false);

        sound = PlayerPrefs.GetInt(ConstantValue.sound, 1) == 1;
        music = PlayerPrefs.GetInt(ConstantValue.music, 1) == 1;
        vibration = PlayerPrefs.GetInt(ConstantValue.vibration, 1) == 1;
        notification = PlayerPrefs.GetInt(ConstantValue.notification, 1) == 1;

        musicButton.sprite = music ? musicOnSprite : musicOffSprite;
        soundButton.sprite = sound ? soundOnSprite : soundOffSprite;
        vibrationButton.sprite = vibration ? vibrationOnSprite : vibrationOffSprite;
        //notificationButton.sprite = notification ? notificationOnSprite : notificationOffSprite;

        //changeSongButton.GetComponent<Button>().interactable = music;

        //savePath = Path.Combine(Application.persistentDataPath, "data");
        //savePath = Path.Combine(savePath, "MyImages3");

        //source change korte hobe;
        //SetBGRawImage();

        ShowBestScore();

        //totalPrice = PlayerPrefs.GetInt(ConstantValue.price, 0);
        //priceText.text = totalPrice.ToString();
    }

    private void ShowBestScore() {
        int bestScore = PlayerPrefs.GetInt(ConstantValue.bestScore, 0);
        //int level = PlayerPrefs.GetInt(ConstantValue.level, 1);
        //level = level - 1;
        //string bestScoreTitle = SetLanguage.Instance.GetBestScoreTitle();
        //bestScoreText.text = bestScoreTitle + ": " + bestScore.ToString();
        bestScoreText.text = "Best Score: " +  bestScore.ToString();
    }

    public void OnPlay() {
        SoundControl.Instance.PlayClick();
        SceneManager.LoadScene("Play");
    }

    public void OnSound() {
        SoundControl.Instance.PlayClick();
        sound = !sound;
        PlayerPrefs.SetInt(ConstantValue.sound, sound ? 1 : 0);
        soundButton.sprite = sound ? soundOnSprite : soundOffSprite;
    }

    public void OnMusic() {
        SoundControl.Instance.PlayClick();
        music = !music;
        
        //changeSongButton.GetComponent<Button>().interactable = music;

        PlayerPrefs.SetInt(ConstantValue.music, music ? 1 : 0);
        musicButton.sprite = music ? musicOnSprite : musicOffSprite;
        SoundControl.Instance.PlayMusic(0);
    }

    public void OnVibrate() {
        SoundControl.Instance.PlayClick();
        vibration = !vibration;
        PlayerPrefs.SetInt(ConstantValue.vibration, vibration ? 1 : 0);
        vibrationButton.sprite = vibration ? vibrationOnSprite : vibrationOffSprite;
    }

    


    public void OnBack() {
        SoundControl.Instance.PlayClick();
        settingUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void OnSetting() {
        SoundControl.Instance.PlayClick();
        mainUI.SetActive(false);
        settingUI.SetActive(true);
    }

    /*public void OnShop() {
        SoundControl.Instance.PlayClick();
        mainUI.SetActive(false);
        shopUI.SetActive(true);
    }*/


    /*public void LoadImageForGellery() {
        SoundControl.Instance.PlayClick();
        int maxSize = 512; 
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) => {
            Debug.Log("Image path: " + path);
            if (path != null) {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null) {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                bgRowImage.texture = texture;

                Texture2D dub = duplicateTexture(texture);
                byte[] bytes = dub.EncodeToPNG();
                string imageName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imageName = imageName + ".png";

                string newdirectory = Path.Combine(savePath, imageName);
                SaveImagetoFolder(newdirectory, bytes);
                
                PlayerPrefs.SetString(ConstantValue.bgImageName, imageName);
            }
        }, "Select a PNG image", "image/png");

        Debug.Log("Permission result: " + permission);
    }*/


    /*private Texture2D duplicateTexture(Texture2D source) {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }*/

    /*private void SaveImagetoFolder(string path, byte[] imageBytes) {
        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path))) {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try {
            File.WriteAllBytes(path, imageBytes);
            //Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        } catch (Exception e) {
            //Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            //Debug.LogWarning("Error: " + e.Message);
        }
    }*/

    /*private byte[] LoadImageFromFolder(string path) {
        byte[] dataByte = null;
        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path))) {
            //Debug.LogWarning("Directory does not exist");
            return null;
        }

        if (!File.Exists(path)) {
            //Debug.Log("File does not exist");
            return null;
        }

        try {
            dataByte = File.ReadAllBytes(path);
            //Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
        } catch (Exception e) {
            //Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
            //Debug.LogWarning("Error: " + e.Message);
        }

        return dataByte;
    }*/

    /*private void SetBGRawImage() {
        string imageName = PlayerPrefs.GetString(ConstantValue.bgImageName, "00");
        if (imageName == "00") {
            //empty
            int lan = PlayerPrefs.GetInt(ConstantValue.language, 0);
            bgRowImage.texture = defaultImage[lan];
        } else {
            string newdirectory = Path.Combine(savePath, imageName);
            byte[] byteDate = LoadImageFromFolder(newdirectory);

            Texture2D newTexture = new Texture2D(2, 2);
            newTexture.LoadImage(byteDate);
            bgRowImage.texture = newTexture;
        }
    }*/

    public void OnSetDefault() {
        //set background
        SoundControl.Instance.PlayMusic(1);
        SoundControl.Instance.PlayClick();
        //int lan = PlayerPrefs.GetInt(ConstantValue.language, 0);
        //bgRowImage.texture = defaultImage[lan];
        //PlayerPrefs.SetString(ConstantValue.bgImageName, "00");
        //messageObject.SetActive(true);
        //messageObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = SetLanguage.Instance.GetMessage(1);
        //CancelInvoke("SetFalseMessage");
        //Invoke("SetFalseMessage", 2);
    }

    private void OnChangeLanguage(int index) {
        if (SoundControl.Instance) {
            SoundControl.Instance.PlayClick();
        }
        ShowBestScore();
    }

    public void OnEnglish() {
        OnChangeLanguage(0);
    }

    public void OnGermany() {
        OnChangeLanguage(1);
    }

    public void OnRussia() {
        OnChangeLanguage(2);
    }

    /*public void OnMoreGame() {
        SoundControl.Instance.PlayClick();
        messageObject.SetActive(true);
        messageObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = SetLanguage.Instance.GetMessage(2);
        CancelInvoke("SetFalseMessage");
        Invoke("SetFalseMessage", 2);
    }*/

    /*public void Login() {
        SoundControl.Instance.PlayClick();
        messageObject.SetActive(true);
        messageObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = SetLanguage.Instance.GetMessage(2);
        CancelInvoke("SetFalseMessage");
        Invoke("SetFalseMessage", 2);
    }*/

    /*private void SetFalseMessage() {
        messageObject.SetActive(false);
    }*/

    /*public void OnChangeSong() {
        SoundControl.Instance.PlayClick();
        NativeGallery.Permission permission = NativeGallery.GetAudioFromGallery((path) => {
            if (path != null) {
                StartCoroutine(LoadAudio(path));
            }
        }, "Select a PNG image", "audio/wav");
    }**/

    IEnumerator LoadAudio(string path) {
        path = string.Format("file://{0}", path);
        WWW audioLoader = new WWW(path);
        yield return audioLoader;
        AudioClip userChooseMusic= audioLoader.GetAudioClip(false, true);
        SoundControl.Instance.ChangeBGMusic(userChooseMusic);
    }

    /*public void OnShopItemSelector(int index) {
        SoundControl.Instance.PlayClick();
        string name = "Ball" + index;
        int id = PlayerPrefs.GetInt(name, 0);
        //id=0 lock, id=1 unlock;
        if (id == 0) {
            int ballPrice = Shop.Instance.GetBallPrice(index);
            if (totalPrice >= ballPrice) {
                //change ball statau,change active status,minus price
                
                totalPrice -= ballPrice;
                priceText.text = totalPrice.ToString();
                PlayerPrefs.SetInt(ConstantValue.price, totalPrice);
                Shop.Instance.ChangeActiveStatus(index);
                //Shop.Instance.UnlockBall(index);
                PlayerPrefs.SetInt(name, 1);
                PlayerPrefs.SetInt(ConstantValue.activeBall, index);
            }
        } else if (id == 1) {
            //just change active status
            Shop.Instance.ChangeActiveStatus(index);
            //PlayerPrefs.SetInt(name, 1);
            PlayerPrefs.SetInt(ConstantValue.activeBall, index);
        } 
    }*/

    /*public void OnBallSelectorRestore() {
        SoundControl.Instance.PlayClick();
        Shop.Instance.BallSelectorReStore();
    }*/

    /*public void OnShopBack() {
        SoundControl.Instance.PlayClick();
        mainUI.SetActive(true);
        shopUI.SetActive(false);
    }*/

    public void OnPrivacyPolicy() {
        SoundControl.Instance.PlayClick();
        string url = "https://docs.google.com/document/d/1GblwPGlCLkDVPv5z5dWcRKWbqqaNY-yYTbNYC8Pw0os/edit";
        Application.OpenURL(url);
    }

    public void OnQuit() {
        SoundControl.Instance.PlayClick();
        Application.Quit();
    }


}

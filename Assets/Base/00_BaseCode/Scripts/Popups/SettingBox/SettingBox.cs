using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SettingBox : BaseBox
{
    #region instance
    public static SettingBox instance;
    public static SettingBox Setup(bool isOffButton, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingBox>(PathPrefabs.SETTING_BOX));
            instance.Init();
        }

        instance.InitState(isOffButton);
        return instance;
    }
    #endregion
    #region Var

    [SerializeField] private Button btnClose;
  

    [SerializeField] private Button btnVibration;
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnSound;
  

    [SerializeField] private Image imageVibration;
    [SerializeField] private Image imageMusic;
    [SerializeField] private Image imageSound;

    [SerializeField] private Sprite spriteVibrationOn;
    [SerializeField] private Sprite spriteVMusicOn;
    [SerializeField] private Sprite spriteVSoundOn;


    [SerializeField] private Sprite spriteVibrationOff;
    [SerializeField] private Sprite spriteVMusicOff;
    [SerializeField] private Sprite spriteVSoundOff;

 
    public Button btnHome;
    public Button btnRestart;

    public bool isGameplay;
 
    #endregion
    private void Init()
    {
        btnClose.onClick.AddListener(delegate { OnClickButtonClose(); }); 
        btnVibration.onClick.AddListener(delegate { OnClickBtnVibration(); });
        btnMusic.onClick.AddListener(delegate { OnClickBtnMusic(); });
        btnSound.onClick.AddListener(delegate { OnClickBtnSound(); });
      
  
        btnHome.onClick.AddListener(delegate { HandleBtnHome(); });
        btnRestart.onClick.AddListener(delegate { HandleBtnRestart(); });
    }
    private void InitState(bool param)
    {
        isGameplay = param;
        if (param)
        {
            
            btnHome.gameObject.SetActive(true);
            btnRestart.gameObject.SetActive(true);
            GamePlayController.Instance.playerContain.boomInputController.enabled = false;
        }    
        else
        {
         
            btnHome.gameObject.SetActive(false);
            btnRestart.gameObject.SetActive(false);
        }    
    
        SetUpBtn();
       
    }

    public void OffBtn()
    {
        btnHome.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
    }    
    private void SetUpBtn()
    {
        if (GameController.Instance.useProfile.OnVibration)
        {
            imageVibration.sprite = spriteVibrationOn;
          //  btnVibration.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            imageVibration.sprite = spriteVibrationOff;
           // btnVibration.GetComponent<Image>().sprite = spriteBtnOff;
        }

        if (GameController.Instance.useProfile.OnMusic)
        {
            imageMusic.sprite = spriteVMusicOn;
        //    btnMusic.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            imageMusic.sprite = spriteVMusicOff;
          //  btnMusic.GetComponent<Image>().sprite = spriteBtnOff;
        }

        if (GameController.Instance.useProfile.OnSound)
        {
            imageSound.sprite = spriteVSoundOn;
           // btnSound.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            imageSound.sprite = spriteVSoundOff;
          //  btnSound.GetComponent<Image>().sprite = spriteBtnOff;
        }
        imageVibration.SetNativeSize();
        imageMusic.SetNativeSize();
        imageSound.SetNativeSize();
    }

  
    private void OnClickBtnVibration()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (GameController.Instance.useProfile.OnVibration)
        {
            GameController.Instance.useProfile.OnVibration = false;
        }
        else
        {
            GameController.Instance.useProfile.OnVibration = true;
        }
        SetUpBtn();
    }

    private void OnClickBtnMusic()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (GameController.Instance.useProfile.OnMusic)
        {
            GameController.Instance.useProfile.OnMusic = false;
        }
        else
        {
            GameController.Instance.useProfile.OnMusic = true;
        }
        SetUpBtn();
    }
    private void OnClickBtnSound()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (GameController.Instance.useProfile.OnSound)
        {
            GameController.Instance.useProfile.OnSound = false;
        }
        else
        {
            GameController.Instance.useProfile.OnSound = true;
        }
        SetUpBtn();
    }


    private void OnClickButtonClose()
    {
       
        GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "BtnCloseSettingBox");

        void Next()
        {
            if(isGameplay)
            {
                GamePlayController.Instance.playerContain.boomInputController.enabled = true;

            }    
        
            Close();

        }
  
    }


    public void HandleBtnHome()
    {

        //GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "BtnBackHomeSettingBox");

        //void Next()
        //{
 
        //    Close();
        //    Initiate.Fade("HomeScene", Color.black, 1.5f);

        //}

        BackHomeBox.Setup(TypeBackHOme.BackHome).Show();



    }
    public void HandleBtnRestart()
    {
        //GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "Restart");
        //void Next()
        //{
        //    Close();
        //    Initiate.Fade("GamePlay", Color.black, 1.5f);
        //}
        //Close();
        BackHomeBox.Setup(TypeBackHOme.ResetLevel).Show();


    }
    private void OnClickRestorePurchase()
    {
        GameController.Instance.iapController.RestorePurchases();
    }    

}

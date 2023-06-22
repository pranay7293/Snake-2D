using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button buttonPlay;
    public Button Oneplayer;
    public Button Twoplayers;
    public Button BackButton;
    public Button Music;
    public Button Sound;
    public Image spriteImageM;
    public Image spriteImageS;

    public Sprite newMusicSprite;
    public Sprite defaultMusicSprite;
    public Sprite newSoundSprite;
    public Sprite defaultSoundSprite;

    private bool isDefaultMusicSprite = true;
    private bool isDefaultSoundSprite = true;

    public GameObject LevelSelection;

    private void Start()
    {
        buttonPlay.onClick.AddListener(LoadPlayModes);
        Oneplayer.onClick.AddListener(OnePlayerMode);
        Twoplayers.onClick.AddListener(TwoPlayersMode);
        BackButton.onClick.AddListener(BacktoLobby);
        Music.onClick.AddListener(ChangeMusicSprite);
        Sound.onClick.AddListener(ChangeSoundSprite);
    }
    
    private void ChangeSoundSprite()
    {
        if (isDefaultSoundSprite)
        {
            spriteImageS.sprite = newSoundSprite;
            SoundManager.Instance.StopSfx();
        }
        else
        {
            spriteImageS.sprite = defaultSoundSprite;
            SoundManager.Instance.StartSfx();
        }
        isDefaultSoundSprite = !isDefaultSoundSprite;
    }

    private void ChangeMusicSprite()
    {
        if (isDefaultMusicSprite)
        {
            spriteImageM.sprite = newMusicSprite;
            SoundManager.Instance.StopMusic();
        }
        else
        {
            spriteImageM.sprite = defaultMusicSprite;
            SoundManager.Instance.StartMusic();
        }
        isDefaultMusicSprite = !isDefaultMusicSprite;
    }

    private void BacktoLobby()
    {
        SoundManager.Instance.PlaySound(Sounds.ExitButtonClick);
        LevelSelection.SetActive(false);
    }
    private void TwoPlayersMode()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelButtonClick);
        SceneManager.LoadScene(2);
    }

    private void OnePlayerMode()
    {
        SoundManager.Instance.PlaySound(Sounds.LevelButtonClick);
        SceneManager.LoadScene(1);
    }

    private void LoadPlayModes()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        LevelSelection.SetActive(true);            
    }  
}

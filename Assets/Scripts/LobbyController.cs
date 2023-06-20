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


    public GameObject LevelSelection;

    // Start is called before the first frame update
    void Awake()
    {
        buttonPlay.onClick.AddListener(LoadPlayModes);
        Oneplayer.onClick.AddListener(OnePlayerMode);
        Twoplayers.onClick.AddListener(TwoPlayersMode);
        BackButton.onClick.AddListener(ExittoLobby);

    }

    private void ExittoLobby()
    {
        SoundManager.Instance.Play(Sounds.ExitButtonClick);
        SceneManager.LoadScene(0);      
    }
    private void TwoPlayersMode()
    {
        SoundManager.Instance.Play(Sounds.LevelButtonClick);
        SceneManager.LoadScene(2);
    }

    private void OnePlayerMode()
    {
        SoundManager.Instance.Play(Sounds.LevelButtonClick);
        SceneManager.LoadScene(1);
    }

    private void LoadPlayModes()
    {
        SoundManager.Instance.Play(Sounds.PlayButtonClick);
        LevelSelection.SetActive(true);            
    }  
}

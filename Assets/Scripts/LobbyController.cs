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

    public GameObject LevelSelection;

    // Start is called before the first frame update
    void Awake()
    {
        buttonPlay.onClick.AddListener(LoadPlayModes);
        Oneplayer.onClick.AddListener(OnePlayerMode);
        Twoplayers.onClick.AddListener(TwoPlayersMode);

    }

    private void TwoPlayersMode()
    {
        SceneManager.LoadScene(2);
    }

    private void OnePlayerMode()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadPlayModes()
    {
        LevelSelection.SetActive(true);            
    }  
}

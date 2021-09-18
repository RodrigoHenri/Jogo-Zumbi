using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerPause : MonoBehaviour
{
    [Header("Pause Panel")]
    [SerializeField] private GameObject PauseHUD;

    [Header("Player Reference")]
    [SerializeField] private Transform Player;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        this.GetComponent<CinemachineBrain>().enabled = false;
        this.GetComponent<AudioListener>().enabled = false;
        PauseHUD.SetActive(true);
        Time.timeScale = 0f;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Time.timeScale == 1f && Player.GetComponent<PlayerController>().CanPlay == true)
        {
            PauseGame();
        }
    }
}

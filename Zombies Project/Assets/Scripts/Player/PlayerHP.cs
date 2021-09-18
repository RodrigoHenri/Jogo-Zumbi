using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHP : MonoBehaviour
{
    [Header("Life Variables")]
    public int Life;
    public int MaxLife;
    public Slider slider;

    [Header("Player Parent Reference")]
    [SerializeField] private Transform Player;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource audio;

    [Header("Camera Reference")]
    private Transform cameraTransform;

    [Header("Game Over HUD")]
    [SerializeField] private GameObject GameOverImage;

    void Start()
    {
        Life = MaxLife;
        slider.maxValue = MaxLife;
        slider.value = Life;
    }

   

    public void TakeDamage(int DamageTaken)
    {
        if (!audio.isPlaying)
        {
            audio.Play(0);
        }

        Life = Life - DamageTaken;
        slider.value = Life;

        if (Life <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
    }

   
    public IEnumerator Die()
    {
        Player.GetComponent<PlayerController>().CanPlay = false;
        cameraTransform = Camera.main.transform;
        cameraTransform.GetComponent<CinemachineBrain>().enabled = false;
        cameraTransform.GetComponent<AudioListener>().enabled = false;

        yield return new WaitForSeconds(3f);
        GameOverImage.SetActive(true);
        Player.GetComponent<PlayerScore>().SavePointsWhenDied();
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Enemies")
        {
            TakeDamage(1);
        }
    }
}

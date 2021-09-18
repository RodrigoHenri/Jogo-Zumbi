using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;


public class PauseSystem : MonoBehaviour
{

    [Header("Loading Panel Settings")]
    private Transform cameraTransform;

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;
        cameraTransform.GetComponent<CinemachineBrain>().enabled = true;
        cameraTransform.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1f;
    }

    public void LoadLevel(string NameScene)
    {
        StartCoroutine(LoadYourAsyncScene(NameScene));
    }

    IEnumerator LoadYourAsyncScene(string NameScene)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NameScene);
       

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

       
    }
}

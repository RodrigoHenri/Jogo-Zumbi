using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Menu : MonoBehaviour
{
    [Header("My HighScore")]
    [SerializeField] private int HighScoreReached = 0;
    [SerializeField] private TextMeshProUGUI text_HighScoreReached;

    

    void Awake()
    {
        HighScoreReached = PlayerPrefs.GetInt("HighScore");
        text_HighScoreReached.text = "Recorde Atual: " + HighScoreReached;
    }
    
    public void LoadLevel(string NameScene)
    {
        StartCoroutine(LoadYourAsyncScene(NameScene));
    }

    IEnumerator LoadYourAsyncScene(string NameScene)
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NameScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClearAllDate()
    {
        PlayerPrefs.DeleteAll();
        HighScoreReached = 0;
        PlayerPrefs.SetInt("HighScore", HighScoreReached);
        text_HighScoreReached.text = "Recorde Atual: 0";
    }

    
}

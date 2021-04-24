using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    //
    public GameObject loadScreen;
    public Slider slider;

    public void loadNextlevel()
    {
        StartCoroutine(loadLevel());
    }

    IEnumerator loadLevel()
    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while(!operation.isDone)
        {
            slider.value = operation.progress;

            yield return null;
        }
    }
}

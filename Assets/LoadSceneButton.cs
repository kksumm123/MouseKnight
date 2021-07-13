using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] string loadSceneName = "Main";
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneName);
    }
}

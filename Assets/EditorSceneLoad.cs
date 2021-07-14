using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorSceneLoad
{
    [MenuItem("Window/1. Title Scene Load", false, -10000)]
    static void TitleSceneLoad()
    {
        LoadScene("Title");
    }
    [MenuItem("Window/2. Stage1 Load", false, -10000)]
    static void Stage1SceneLoad()
    {
        LoadScene("Stage1");
    }

    static void LoadScene(string sceneName)
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity");
    }
}

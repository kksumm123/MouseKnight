using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneProperty : MonoBehaviour
{
    public static SceneProperty instance;
    void Awake()
    {
        instance = this;

        if(GameData.Instance == null)
            Instantiate(Resources.Load("GameData"));
    }
    public int StageID = -1;
    public enum SceneType
    {
        NONE,
        Stage,
        Title,
    }
    public SceneType sceneType = SceneType.Stage;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProperty : MonoBehaviour
{
    public static StageProperty instance;
    void Awake()
    {
        instance = this;

        if (PersistCanvas.instance == null)
            Instantiate(Resources.Load("PersistCanvas"));
    }
    void OnDestroy()
    {
        instance = null;
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

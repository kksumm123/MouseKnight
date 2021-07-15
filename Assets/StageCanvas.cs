using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCanvas : MonoBehaviour
{
    public static StageCanvas instance;
    public CanvasGroup blackScreen;
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        blackScreen = transform.Find("BlackScreen").GetComponent<CanvasGroup>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    public static PersistCanvas instance;
    public CanvasGroup blackScreen;
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        blackScreen = transform.Find("BlackScreen").GetComponent<CanvasGroup>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

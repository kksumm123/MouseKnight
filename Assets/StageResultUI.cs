using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : BaseUI<StageResultUI>
{
    Text gradeText;
    Text enemiesKiiledText;
    Text damageTakenText;
    Button continueButton;

    void Start()
    {
        gradeText = transform.Find("GradeText").GetComponent<Text>();
        enemiesKiiledText = transform.Find("EnemiesKiiledText").GetComponent<Text>();
        damageTakenText = transform.Find("DamageTakenText").GetComponent<Text>();
        continueButton = transform.Find("ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(LoadNextStage);
    }

    private void LoadNextStage()
    {
        Debug.LogWarning("LoadNextStage");
    }
}

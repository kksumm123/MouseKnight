using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour
{
    Text gradeText;
    Text enemiesKiiledText;
    Text damageTakenText;
    Button continueButton;

    void Start()
    {
        gradeText = transform.Find("StageResultUI/GradeText").GetComponent<Text>();
        enemiesKiiledText = transform.Find("StageResultUI/EnemiesKiiledText").GetComponent<Text>();
        damageTakenText = transform.Find("StageResultUI/DamageTakenText").GetComponent<Text>();
        continueButton = transform.Find("StageResultUI/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(LoadNextStage);
    }

    private void LoadNextStage()
    {
        throw new NotImplementedException();
    }
}

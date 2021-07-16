using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : BaseUI<StageResultUI>
{
    public override string HierarchyPath => "StageCanvas/StageResultUI";

    Text gradeText;
    Text enemiesKiiledText;
    Text damageTakenText;
    Button continueButton;

    void Init()
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

    protected override void OnShow()
    {
        Init();
        enemiesKiiledText.text = StageManager.Instance.enemiesKilledCount.ToString();
        damageTakenText.text = StageManager.Instance.damageTakenPoint.ToString();
        gradeText.text = "A"; //임시로 A로 나오도록 
    }
}

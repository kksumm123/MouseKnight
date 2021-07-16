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

    private void LoadNextStage()
    {
        Debug.LogWarning("LoadNextStage");

        base.Close();
        gameObject.SetActive(false);
    }

    protected override void OnShow()
    {
        gradeText = transform.Find("GradeText").GetComponent<Text>();
        enemiesKiiledText = transform.Find("EnemiesKiiledText").GetComponent<Text>();
        damageTakenText = transform.Find("DamageTakenText").GetComponent<Text>();
        continueButton = transform.Find("ContinueButton").GetComponent<Button>();
        continueButton.AddListener(this, LoadNextStage);
    }

    public void ShowUI(int enemiesKilledCount, int sumMonsterCount, int damageTakenPoint)
    {
        base.Show();

        enemiesKiiledText.text = $"{enemiesKilledCount} / {sumMonsterCount}";
        damageTakenText.text = damageTakenPoint.ToString();
        gradeText.text = "A"; //임시로 A로 나오도록 
    }
}

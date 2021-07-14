using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    CanvasGroup blackScreen;
    [SerializeField] string loadSceneName = "Stage1";
    void Start()
    {
        // ���� ȭ�鿡�� ��� �Ѵ�.
        blackScreen = GameObject.Find("PersistCanvas").transform.Find("BlackScreen").GetComponent<CanvasGroup>();
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0, 0.7f).OnComplete(()=>
        {
            blackScreen.gameObject.SetActive(false);
        });

        // �� ���� ������ �������� 1�ε�
        Button button = GameObject.Find("TitleCanvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(OnClickNewGame);
        // ��ο���������������1 �ε�
        // �������
    }

    public void OnClickNewGame()
    {
        // ���� ��Ӱ�
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(1, 0.7f).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneName);
        });
    }
}
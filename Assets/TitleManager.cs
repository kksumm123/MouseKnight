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
        // 검은 화면에서 밝게 한다.
        blackScreen = GameObject.Find("PersistCanvas").transform.Find("BlackScreen").GetComponent<CanvasGroup>();
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0, 0.7f).OnComplete(()=>
        {
            blackScreen.gameObject.SetActive(false);
        });

        // 뉴 게임 누르면 스테이지 1로드
        Button button = GameObject.Find("TitleCanvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(OnClickNewGame);
        // 어두워졌을때스테이지1 로드
        // 밝아지자
    }

    public void OnClickNewGame()
    {
        // 점점 어둡게
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(1, 0.7f).OnComplete(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneName);
        });
    }
}
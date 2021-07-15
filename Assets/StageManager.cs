using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지에서 발행하는 모든 이벤트 관리
//스테이지 부서지면 같이 부서짐
//1. 플레이어 로드게임 시작시까지 플레이어 대기시킴
//2. 스테이지 시작시 화면 밝아지게
//3. 몬스터 로드
public class StageManager : MonoBehaviour
{
    IEnumerator Start()
    {
        //화면 어두운 상태로 만들고 2초 동안 밝아지게 하자
        CanvasGroup blackscreen = PersistCanvas.instance.blackScreen;
        blackscreen.gameObject.SetActive(true);
        blackscreen.alpha = 1;
        blackscreen.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);

        // 스테이지 이름 표시하자
        //Stage1

        // 2초 쉬었다가
        //플레이어를 움직일 수 있게

        yield return null;
    }

    void Update()
    {
        
    }
}

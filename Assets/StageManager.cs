using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������������ �����ϴ� ��� �̺�Ʈ ����
//�������� �μ����� ���� �μ���
//1. �÷��̾� �ε���� ���۽ñ��� �÷��̾� ����Ŵ
//2. �������� ���۽� ȭ�� �������
//3. ���� �ε�
public enum GameStageType
{
    Ready,
    Playing,
    StageEnd,
}
public class StageManager : MonoBehaviour
{
    public GameStageType gameStage = GameStageType.Ready;
    private void Awake()
    {
        gameStage = GameStageType.Ready;
    }

    IEnumerator Start()
    {
        //ȭ�� ��ο� ���·� ����� 2�� ���� ������� ����
        CanvasGroup blackscreen = PersistCanvas.instance.blackScreen;
        blackscreen.gameObject.SetActive(true);
        blackscreen.alpha = 1;
        blackscreen.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);

        // �������� �̸� ǥ������
        //string stageText = 

        // 2�� �����ٰ�
        //�÷��̾ ������ �� �ְ�
        gameStage = GameStageType.Playing;

        yield return null;
    }

    void Update()
    {
        
    }
}

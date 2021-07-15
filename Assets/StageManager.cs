using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������������ �����ϴ� ��� �̺�Ʈ ����
//�������� �μ����� ���� �μ���
//1. �÷��̾� �ε���� ���۽ñ��� �÷��̾� ����Ŵ
//2. �������� ���۽� ȭ�� �������
//3. ���� �ε�
public enum GameStateType
{
    Ready,
    Playing,
    StageEnd,
}
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public GameStateType gameState = GameStateType.Ready;
    private void Awake()
    {
        instance = this;
        gameState = GameStateType.Ready;
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
        StageInfo stageInfo = 
            GameData.stageInfoMap[SceneProperty.instance.StageID];
        string stageName = stageInfo.titleString;
        StageCanvas.instance.stageNameText.text = stageName;

        // 2�� �����ٰ�
        yield return new WaitForSeconds(2f);

        StageCanvas.instance.stageNameText.transform.DOMoveX(-2000, 1)
            .SetEase(Ease.OutBounce);

        //�÷��̾ ������ �� �ְ�
        gameState = GameStateType.Playing;

        yield return null;
    }

    void Update()
    {
        
    }
}

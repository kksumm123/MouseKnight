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

    public Ease inEaseType = Ease.InElastic;
    public Ease outEaseType = Ease.OutElastic;
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
        StageCanvas.instance.stageNameText.transform.localPosition 
            = new Vector3(-1000, 0, 0);
        StageCanvas.instance.stageNameText.transform.DOLocalMoveX(0, 1)
            .SetEase(inEaseType);
        StageCanvas.instance.stageNameText.text = stageName;

        // 2�� �����ٰ�
        yield return new WaitForSeconds(2f);

        StageCanvas.instance.stageNameText.transform.DOLocalMoveX(-1000, 1)
            .SetEase(outEaseType);

        //�÷��̾ ������ �� �ְ�
        gameState = GameStateType.Playing;

        yield return null;
    }

    void Update()
    {

    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
public class StageManager : BaseUI<StageManager>
{
    public GameStateType gameState = GameStateType.Ready;

    public int enemiesKilledCount;
    public int damageTakenPoint;
    public int sumMonsterCount;
    new void Awake()
    {
        base.Awake();
        gameState = GameStateType.Ready;

        List<SpawnPoint> allSpawnPoints = 
            new List<SpawnPoint>(FindObjectsOfType<SpawnPoint>(true));
        sumMonsterCount = allSpawnPoints.Where(x => x.spawnType != SpawnType.Player).Count();
    }

    public Ease inEaseType = Ease.InElastic;
    public Ease outEaseType = Ease.OutElastic;
    IEnumerator Start()
    {
        //ȭ�� ��ο� ���·� ����� 2�� ���� ������� ����
        CanvasGroup blackscreen = PersistCanvas.Instance.blackScreen;
        blackscreen.gameObject.SetActive(true);
        blackscreen.alpha = 1;
        blackscreen.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);

        // �������� �̸� ǥ������
        StageInfo stageInfo =
            GameData.stageInfoMap[SceneProperty.instance.StageID];
        string stageName = stageInfo.titleString;
        StageCanvas.instance.stageNameText.transform.localPosition
            = new Vector3(-2000, 0, 0);
        StageCanvas.instance.stageNameText.transform.DOLocalMoveX(0, 1)
            .SetEase(inEaseType);
        StageCanvas.instance.stageNameText.text = stageName;

        // 2�� �����ٰ�
        yield return new WaitForSeconds(2f);

        StageCanvas.instance.stageNameText.transform.DOLocalMoveX(-2000, 1)
            .SetEase(outEaseType);

        //�÷��̾ ������ �� �ְ�
        gameState = GameStateType.Playing;

        yield return null;
    }
}

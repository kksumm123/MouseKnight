using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    //���и��� �߰�
    // �����ϴ� Ÿ�ֿ̹� ���� ��� ���� �����ϰ�
    // ���� �ִ� ���� ������ X (���� ����Ʈ ����)

    protected override void SelectAttackType()
    {
        CurrentFSM = AttackCo;
    }
}

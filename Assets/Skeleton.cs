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
        if (Random.Range(0, 1) > 0.5f)
        CurrentFSM = AttackCo;
        else
            CurrentFSM = ShieldCo;
    }

    bool isOnShield = false;
    [SerializeField] float activeShieldTime = 2;
    IEnumerator ShieldCo()
    {
        PlayAnimClip("Shield");
        isOnShield = true;
        yield return new WaitForSeconds(activeShieldTime);
        isOnShield = false;
        CurrentFSM = ChaseCo;
    }

    [SerializeField] GameObject blockEffect;
    public override void TakeHit(float damage)
    {
        if (isOnShield)
            Instantiate(blockEffect, transform.position, Quaternion.identity);
        else
            base.TakeHit(damage);
    }
}

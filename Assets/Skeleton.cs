using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    //방패막기 추가
    // 공격하는 타이밍에 공격 대신 막기 랜덤하게
    // 막고 있는 동안 데미지 X (막는 이펙트 생성)

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

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
        CurrentFSM = AttackCo;
    }
}

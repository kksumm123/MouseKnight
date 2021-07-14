using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    [SerializeField] const string blockEffectPath = "BlockFlash";
    Transform BlockEffectPos;

    //���и��� �߰�
    // �����ϴ� Ÿ�ֿ̹� ���� ��� ���� �����ϰ�
    // ���� �ִ� ���� ������ X (���� ����Ʈ ����)
    protected override void Awake()
    {
        base.Awake();
        blockEffect = (GameObject)Resources.Load(blockEffectPath);
        BlockEffectPos = transform.Find("BlockEffectPos").GetComponent<Transform>();
    }

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
    bool isFrontofPlayer = false;
    enum Direction
    {
        Left,
        Right,
    }
    public override void TakeHit(float damage)
    {
        if (FrontofShield() == true)
        {
            Instantiate(blockEffect, BlockEffectPos.position, Quaternion.identity);
        }
        else
            base.TakeHit(damage);
    }

    private bool FrontofShield()
    {
        if (isOnShield == false)
            return false;

        // right rotation.y = 0 
        // left rotation.y = 180 
        Direction currentDirection = transform.rotation.eulerAngles.y == 180
            ? Direction.Left : Direction.Right;

        float playerDistance = player.transform.position.x - transform.position.x;
        int directionMultiplyValue = currentDirection == Direction.Right ? 1 : -1;

        if (playerDistance * directionMultiplyValue > 0)
            return true;

        return false;
    }
}

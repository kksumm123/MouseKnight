using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Func<IEnumerator> currentFSM;
    Player player;
    Animator animator;

    [SerializeField] int power = 10;
    [SerializeField] float speed = 40;

    bool isLive = false;
    bool isChase = false;
    IEnumerator Start()
    {
        player = Player.instance;
        animator = GetComponentInChildren<Animator>();
        yield return null;

        isLive = true;
        currentFSM = IdleCo;

        while (isLive)
        { //���¸� ������ �ݺ��ؼ� �����ϴ� �κ�
            yield return StartCoroutine(currentFSM());
        }
    }

    [SerializeField] float detectRange = 40;
    private IEnumerator IdleCo()
    {
        //IdleCo
        // �����ϸ� Idle
        // �÷��̾� �����ϸ� �߰�
        animator.Play("Idle");

        // �����Ÿ����� ũ�� while, ������ Ż��
        while (
            Vector3.Distance(transform.position, player.transform.position)
            > detectRange)
        {
            yield return null;
        }

        // �����Ÿ� ������ ����
        isChase = true;
        currentFSM = ChaseCo;
    }

    IEnumerator ChaseCo()
    {
        animator.Play("Run");

        while (isChase)
        {
            Vector3 toPlayerDirection = player.transform.position - transform.position;
            toPlayerDirection.Normalize();
            transform.rotation = Quaternion.Euler(0, toPlayerDirection.x > 0 ? 0 : 180, 0);
            transform.Translate(toPlayerDirection * speed * Time.deltaTime, Space.World);
            
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                currentFSM = AttackCo;
                yield break;
            }

            yield return null;
        }

    }

    [SerializeField] float attackRange = 10f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] float attackApplyTime = 0.2f;
    IEnumerator AttackCo()
    {
        animator.Play("Attack");
        yield return new WaitForSeconds(attackApplyTime);
        
        //���� ����
        if(Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {//�÷��̾� ������
            player.TakeHit(power);
        }

        yield return new WaitForSeconds(attackTime - attackApplyTime);
        currentFSM = ChaseCo;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
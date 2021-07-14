using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Func<IEnumerator> currentFSM;
    Func<IEnumerator> CurrentFSM
    {
        get => currentFSM;
        set
        {
            currentFSM = value;
            coroutineHandle = null;
        }
    }
    Player player;
    Animator animator;

    [SerializeField] int hp = 50;
    [SerializeField] int power = 10;
    [SerializeField] float speed = 40;

    Coroutine coroutineHandle;
    bool isLive = false;
    bool isChase = false;
    IEnumerator Start()
    {
        player = Player.instance;
        animator = GetComponentInChildren<Animator>();
        yield return null;

        isLive = true;
        CurrentFSM = IdleCo;

        while (isLive)
        { //상태를 무한히 반복해서 실행하는 부분
            coroutineHandle = StartCoroutine(CurrentFSM());
            while (coroutineHandle != null)
                yield return null;
        }
    }

    [SerializeField] float detectRange = 40;
    private IEnumerator IdleCo()
    {
        //IdleCo
        // 시작하면 Idle
        // 플레이어 근접하면 추격
        animator.Play("Idle");

        // 감지거리보다 크면 while, 작으면 탈출
        while (
            Vector3.Distance(transform.position, player.transform.position)
            > detectRange)
        {
            yield return null;
        }

        // 감지거리 안으로 들어옴
        isChase = true;
        CurrentFSM = ChaseCo;
    }
    void Playanim(string str)
    {
        animator.Play(str);
        Debug.Log(str);
    }
    IEnumerator ChaseCo()
    {
        animator.Play("Run", 0, 0);

        while (isChase)
        {
            Vector3 toPlayerDirection = player.transform.position - transform.position;
            toPlayerDirection.Normalize();
            transform.rotation = Quaternion.Euler(0, toPlayerDirection.x > 0 ? 0 : 180, 0);
            transform.Translate(toPlayerDirection * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                CurrentFSM = AttackCo;
                yield break;
            }

            yield return null;
        }

    }

    [SerializeField] float attackRange = 10f;
    [SerializeField] float attackTime = 1.2f;
    [SerializeField] float attackApplyTime = 0.2f;
    IEnumerator AttackCo()
    {
        animator.Play("Attack", 0, 0);

        yield return new WaitForSeconds(attackApplyTime);

        //실제 어택
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {//플레이어 때리자
            player.TakeHit(power);
            Debug.Log("공격함");
        }

        yield return new WaitForSeconds(attackTime - attackApplyTime);
        CurrentFSM = ChaseCo;
    }

    [SerializeField] float TakeHitTime = 0.3f;
    private IEnumerator TakeHitCo()
    {
        animator.Play("TakeHit");
        yield return new WaitForSeconds(TakeHitTime); ;
        if (hp > 0)
            CurrentFSM = IdleCo;
        else
            CurrentFSM = DeathCo;
    }
    [SerializeField] float deathTime = 0.5f;
    IEnumerator DeathCo()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    public void TakeHit(float damage)
    {
        hp -= (int)damage;
        StopCo(coroutineHandle);
        coroutineHandle = null;
        CurrentFSM = TakeHitCo;
    }
    void StopCo(Coroutine handle)
    {
        if (handle != null)
            StopCoroutine(handle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

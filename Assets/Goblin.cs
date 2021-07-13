using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Player player;
    Animator animator;

    [SerializeField] float speed = 4;

    bool isLive = false;
    bool isChase = false;
    IEnumerator Start()
    {
        player = Player.instance;
        animator = GetComponentInChildren<Animator>();
        yield return null;
        
        isLive = true;

        while (isLive)
        { //상태를 무한히 반복해서 실행하는 부분
            StartCoroutine(IdleCo());
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
    }

    IEnumerator ChaseCo()
    {
        while (isChase)
        {
            Vector3 toPlayerDirection = player.transform.position - transform.position;
            toPlayerDirection.Normalize();
            transform.Translate(toPlayerDirection * speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}

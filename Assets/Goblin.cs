using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Func<IEnumerator> currentFSM;
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
        currentFSM = IdleCo;

        while (isLive)
        { //���¸� ������ �ݺ��ؼ� �����ϴ� �κ�
            StartCoroutine(currentFSM());
            yield return null;
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
        while (isChase)
        {
            Vector3 toPlayerDirection = player.transform.position - transform.position;
            toPlayerDirection.Normalize();
            transform.Translate(toPlayerDirection * speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}

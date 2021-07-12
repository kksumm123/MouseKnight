using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 60;
    [SerializeField] float moveAbleDistance = 12;
    Transform spriteTr;
    Transform moustPointer;
    Animator animator;
    enum JumpStateType
    {
        Ground,
        Jump,
    }
    JumpStateType jumpState;
    JumpStateType JumpState { get => jumpState; set => jumpState = value; }

    enum StateType
    {
        Idle,
        Walk,
        JumpUp,
        JumpDown,
        Attakc,
    }
    StateType state;
    StateType State
    {
        get => state;
        set
        {
            if (state == value)
                return;
            state = value;
            animator.Play(State.ToString());
        }
    }

    void Start()
    {
        spriteTr = GetComponentInChildren<SpriteRenderer>().transform;
        moustPointer = GameObject.Find("mousePointer").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        State = StateType.Idle;
    }
    Plane plain = new Plane(new Vector3(0, 1, 0), 0);
    void Update()
    {
        Move();
        Jump();
    }

    public AnimationCurve jumpYac;
    void Jump()
    {
        if (JumpState == JumpStateType.Jump)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(JumpCo());
        }
    }

    [SerializeField] float jumpYMult = 1;
    [SerializeField] float jumpTimeMult = 1;
    private IEnumerator JumpCo()
    {
        JumpState = JumpStateType.Jump;
        State = StateType.JumpUp;
        float jumpStartTime = Time.time;
        float jumpDuration = jumpYac[jumpYac.length - 1].time;
        jumpDuration *= jumpTimeMult;
        float jumpEndTime = jumpStartTime + jumpDuration;
        float sumEvaluateTime = 0;
        float preY = 0;
        while (Time.time < jumpEndTime)
        {
            float y = jumpYac.Evaluate(sumEvaluateTime);
            y *= jumpYMult;
            transform.Translate(0, y, 0);
            yield return null;
            if (preY > y)
                State = StateType.JumpDown;
            preY = y;
            sumEvaluateTime += Time.deltaTime;
        }
        JumpState = JumpStateType.Ground;
        State = StateType.Idle;
    }
    Vector3 dir;

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (plain.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            moustPointer.position = hitPoint;
            float distnace = Vector3.Distance(hitPoint, transform.position);
            if (distnace > moveAbleDistance)
            {
                dir = hitPoint - transform.position;
                dir.Normalize();
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
                bool isRightSide = dir.x > 0;
                if (isRightSide)
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    spriteTr.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteTr.rotation = Quaternion.Euler(new Vector3(-45, 180, 0));
                }
                if (JumpState != JumpStateType.Jump)
                    State = StateType.Walk;
            }
            else
            {
                if (JumpState != JumpStateType.Jump)
                    State = StateType.Idle;
            }
        }
    }
}

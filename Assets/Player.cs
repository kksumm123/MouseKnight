using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 30;
    [SerializeField] float moveAbleDistance = 12;
    Transform moustPointer;
    void Start()
    {
        moustPointer = GameObject.Find("mousePointer").GetComponent<Transform>();
    }
    Plane plain = new Plane(new Vector3(0, 1, 0), 0);
    void Update()
    {
        Move();
        Jump();
    }

    enum JumpStateType
    { 
        Ground,
        Jump,
    }
    JumpStateType jumpState;
    private JumpStateType JumpState { get => jumpState; set => jumpState = value; }
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
    private IEnumerator JumpCo()
    {
        JumpState = JumpStateType.Jump;
        float jumpStartTime = Time.time;
        float jumpDuration = jumpYac[jumpYac.length - 1].time;
        float jumpEndTime = jumpStartTime + jumpDuration;
        float sumEvaluateTime = 0;
        while (Time.time < jumpEndTime)
        {
            float y = jumpYac.Evaluate(sumEvaluateTime);
            y *= jumpYMult;
            transform.Translate(0, y, 0);
            yield return null;
            sumEvaluateTime += Time.deltaTime;
        }
        JumpState = JumpStateType.Ground;
    }

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
                var dir = hitPoint - transform.position;
                dir.Normalize();
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
    }
}

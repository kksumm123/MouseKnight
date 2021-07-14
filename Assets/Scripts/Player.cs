using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] int hp = 100;
    [SerializeField] int power = 20;
    [SerializeField] float speed = 60;
    float originSpeed;
    [SerializeField] float stopDistance = 7;
    [SerializeField] float walkDistance = 12;
    Animator animator;
    SpriteTrailRenderer.SpriteTrailRenderer spriteTrailRenderer;
    enum JumpStateType
    {
        Ground,
        Jump,
    }
    JumpStateType jumpState;
    JumpStateType JumpState { get => jumpState; set => jumpState = value; }

    enum StateType
    {
        NONE,
        Idle,
        Walk,
        JumpUp,
        JumpDown,
        Attack,
        Dash,
        TakeHit,
        Death,
    }
    [SerializeField]
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
    NavMeshAgent agent;
    void Start()
    {
        originSpeed = speed;
        animator = GetComponentInChildren<Animator>();
        spriteTrailRenderer = GetComponentInChildren<SpriteTrailRenderer.SpriteTrailRenderer>();
        agent = GetComponent<NavMeshAgent>();
        enemyLayer = 1 << LayerMask.NameToLayer("Monster");
        attackCollider = GetComponentInChildren<SphereCollider>(true);
        state = StateType.NONE;
    }
    void Update()
    {
        if (IsMovable())
        {
            Move();
            Jump();
        }
        bool isSucceedDash = Dash();
        if (isSucceedDash == false)
            Attack();
    }


    #region Move
    Plane plain = new Plane(new Vector3(0, 1, 0), 0);
    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;

        float movealbeDistance = stopDistance;
        // State가 Walk 일땐 7(stopDistance)사용.
        // Idle에서 Walk로 갈땐 12(WalkDistance)사용
        if (State == StateType.Idle)
            movealbeDistance = walkDistance;

        if (plain.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            float distnace = Vector3.Distance(hitPoint, transform.position);
            dir = hitPoint - transform.position;
            dir.Normalize();

            if (State == StateType.Dash)
                dir = dashDirection;

            if (distnace > movealbeDistance || State == StateType.Dash)
            {
                transform.Translate(dir * speed * Time.deltaTime, Space.World);

                if (ChangeableWalkOrIdleState())
                    State = StateType.Walk;
            }
            else
            {
                if (ChangeableWalkOrIdleState())
                    State = StateType.Idle;
            }

            bool isRightSide = dir.x > 0;
            if (isRightSide)
                transform.rotation = Quaternion.Euler(Vector3.zero);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        bool ChangeableWalkOrIdleState()
        {
            if (jumpState == JumpStateType.Jump)
                return false;

            if (State == StateType.Dash)
                return false;

            return true;
        }
    }

    #endregion Move
    #region Jump
    public AnimationCurve jumpYac;
    void Jump()
    {
        if (JumpState == JumpStateType.Jump)
            return;
        if (Input.GetMouseButtonDown(1))
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
        agent.enabled = false;
        while (Time.time < jumpEndTime)
        {
            float y = jumpYac.Evaluate(sumEvaluateTime);
            y *= jumpYMult * Time.fixedDeltaTime;
            transform.Translate(0, y, 0);
            yield return null;
            if (preY > transform.position.y)
                State = StateType.JumpDown;
            preY = transform.position.y;
            if (preY < 0)
                break;
            sumEvaluateTime += Time.deltaTime;
        }
        agent.enabled = true;
        JumpState = JumpStateType.Ground;
        State = StateType.Idle;
    }
    Vector3 dir;
    #endregion Jump
    #region Dash
    [Header("대쉬----------")]
    [SerializeField] float dashCoolTime = 2;
    float nextDashCoolTime;
    [SerializeField] float dashableDistance = 10;
    [SerializeField] float dashableTime = 0.4f;
    float mouseDownTime = 0;
    Vector3 mouseDownPosition;
    bool Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time;
            mouseDownPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            bool isDashDrag = IsSuccesDashDrag();
            if (isDashDrag)
            {
                nextDashCoolTime = Time.time;
                StartCoroutine(DashCo());

                return true;
            }
        }
        return false;
    }

    [SerializeField] float dashTime = 0.3f;
    [SerializeField] float dashSpeedMult = 4f;
    Vector3 dashDirection;
    private IEnumerator DashCo()
    {
        // dash는 방향을 바꿀 수 없다
        spriteTrailRenderer.enabled = true;
        dashDirection = Input.mousePosition - mouseDownPosition;
        dashDirection.y = 0;
        dashDirection.z = 0;
        dashDirection.Normalize();
        speed = originSpeed * dashSpeedMult;
        State = StateType.Dash;
        yield return new WaitForSeconds(dashTime);
        speed = originSpeed;
        State = StateType.Idle;
        spriteTrailRenderer.enabled = false;
    }

    bool IsSuccesDashDrag()
    {
        // 시간, 거리 체크해야함
        float dragTime = Time.time - mouseDownTime;
        if (dragTime > dashableTime)
            return false;

        float dragDistance = Vector3.Distance(mouseDownPosition, Input.mousePosition);
        if (dragDistance < dashableDistance)
            return false;

        if (State == StateType.Dash)
            return false;

        if (Time.time - nextDashCoolTime > dashCoolTime)
            return false;

        return true;
    }
    #endregion Dash
    #region Attack
    void Attack()
    {
        if (Input.GetMouseButtonUp(0))
            StartCoroutine(AttackCo());
    }
    [SerializeField] float attackTime = 0.5f;
    [SerializeField] float attackApplyTime = 0.2f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] SphereCollider attackCollider;
    Collider[] enemyColliders;
    private IEnumerator AttackCo()
    {
        State = StateType.Attack;
        yield return new WaitForSeconds(attackApplyTime);

        //실제 어택하는 부분
        enemyColliders = Physics.OverlapSphere(attackCollider.transform.position, attackCollider.radius, enemyLayer);
        foreach (var item in enemyColliders)
        {
            item.GetComponent<Monster>().TakeHit(power);
        }

        yield return new WaitForSeconds(attackTime - attackApplyTime);
        State = StateType.Idle;
    }
    #endregion Attack
    #region Methods
    bool IsMovable()
    {
        switch (State)
        {
            case StateType.Attack:
            case StateType.TakeHit:
            case StateType.Death:
                return false;
        }

        return true;
    }

    public void TakeHit(int damage)
    {
        if (State == StateType.Death)
            return;

        hp -= damage;
        // 피격 모션

        StartCoroutine(TakeHitCo());
    }

    [SerializeField] float takeHitTime = 0.3f;
    private IEnumerator TakeHitCo()
    {
        State = StateType.TakeHit;
        yield return new WaitForSeconds(takeHitTime);

        // hp < 0 죽자
        if (hp > 0)
            State = StateType.Idle;
        else
            StartCoroutine(DeathCo());
    }
    [SerializeField] float deathTime = 1f;
    private IEnumerator DeathCo()
    {
        State = StateType.Death;
        yield return new WaitForSeconds(deathTime);
        Debug.LogWarning("깨꼬닥");
    }
    #endregion Methods
}
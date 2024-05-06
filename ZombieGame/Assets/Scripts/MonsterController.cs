using System.Collections;
using UnityEngine;

using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }

    public State state = State.IDLE;

    public float traceDistance = 100.0f;
    public float attackDistance = 2.0f;
    public float attackDamage = 20f;
    public float traceSpeed = 3.5f;

    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    private StateMachine stateMachine;
    private LivingEntity zombieHealth;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");

    private void Awake()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        zombieHealth = GetComponent<LivingEntity>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.AddState(State.IDLE, new IdleState(this));
        stateMachine.AddState(State.TRACE, new TraceState(this));
        stateMachine.AddState(State.ATTACK, new AttackState(this));
        stateMachine.AddState(State.DIE, new DieState(this));
        stateMachine.InitState(State.IDLE);
    }

    public void Setup(float traceDistance, float traceSpeed, float attackDistance, float attackDamage)
    {
        this.traceDistance = traceDistance;
        agent.speed = traceSpeed;
        this.attackDistance = attackDistance;
        this.attackDamage = attackDamage;
    }


    private void Start()
    {
        StartCoroutine(CheckMonsterState());
    }


    private IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (zombieHealth.IsDead)
            {
                isDie = true;
                stateMachine.ChangeState(State.DIE);
                yield break;
            }

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDistance)
            {
                stateMachine.ChangeState(State.ATTACK);
            }
            else if (distance <= traceDistance)
            {
                stateMachine.ChangeState(State.TRACE);
            }
            else
            {
                stateMachine.ChangeState(State.IDLE);
            }
        }
    }

    //애니메이션 이벤트로 실행되는 메서드
    public void ApplyDamageToPlayer()
    {
        if (Vector3.Distance(playerTr.position, monsterTr.position) > attackDistance) return;
        DamageMessage damageMessage = new DamageMessage();
        damageMessage.damage = attackDamage;
        playerTr.GetComponent<LivingEntity>().ApplyDamage(damageMessage);
    }

    private class BaseMonsterState : BaseState
    {
        protected MonsterController owner;
        public BaseMonsterState(MonsterController owner)
        {
            this.owner = owner;
        }
    }
    private class IdleState : BaseMonsterState
    {
        public IdleState(MonsterController owner) : base(owner) { }

        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.anim.SetBool(owner.hashTrace, false);
        }
    }
    private class TraceState : BaseMonsterState
    {
        public TraceState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.SetDestination(owner.playerTr.position);
            owner.agent.isStopped = false;
            owner.anim.SetBool(owner.hashTrace, true);
            owner.anim.SetBool(owner.hashAttack, false);
        }
    }
    private class AttackState : BaseMonsterState
    {
        public AttackState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {
            owner.anim.SetBool(owner.hashTrace, false);
            owner.anim.SetBool(owner.hashAttack, true);
        }
    }

    private class DieState : BaseMonsterState
    {
        public DieState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.agent.velocity = Vector3.zero;
        }
    }
}

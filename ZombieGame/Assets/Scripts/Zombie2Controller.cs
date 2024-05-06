using System.Collections;
using UnityEngine;

using UnityEngine.AI;

public class Zombie2Controller : MonoBehaviour
{
    public enum State
    {
        IDLE, TRACE, ATTACK, RANGED_ATTACK, DIE
    }

    public State state = State.IDLE;

    public float traceDistance = 100.0f;
    public float rangedDistance = 8f;
    public float attackDistance = 2.0f;
    public float attackDamage = 20f;
    public float traceSpeed = 3.5f;
    public bool isDie = false;

    public GameObject stonePrefab;
    public Transform stonePoint;
    public GameObject stone;
    private float throwSpeed = 10f;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    private StateMachine stateMachine;
    private LivingEntity zombieHealth;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashRanged = Animator.StringToHash("IsRanged");

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
        stateMachine.AddState(State.RANGED_ATTACK, new RangedState(this));
        stateMachine.AddState(State.DIE, new DieState(this));
        stateMachine.InitState(State.IDLE);
    }

    public void Setup(float traceDistance, float traceSpeed, float attackDistance, float attackDamage)
    {
        agent.stoppingDistance = rangedDistance;
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
            else if (distance <= rangedDistance)
            {
                stateMachine.ChangeState(State.RANGED_ATTACK);
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

    public void PickupStone()
    {
        Debug.Log("돌 생성");
        stone = Instantiate(stonePrefab, stonePoint);
        stone.transform.localPosition = Vector3.zero;
        Debug.Log(monsterTr.position);
        monsterTr.LookAt(playerTr.position);//안돌아보네?
    }

    public void ThrowRockToPlayer()
    {
        Debug.Log("돌 던지기");
        Vector3 throwVector = (playerTr.position + new Vector3(0, 1.2f, 0) - stonePoint.position).normalized;
        stone.transform.parent = null;
        stone.GetComponent<Rigidbody>().AddForce(throwVector * throwSpeed, ForceMode.Impulse);
    }

    private class BaseMonsterState : BaseState
    {
        protected Zombie2Controller owner;
        public BaseMonsterState(Zombie2Controller owner)
        {
            this.owner = owner;
        }
    }
    private class IdleState : BaseMonsterState
    {
        public IdleState(Zombie2Controller owner) : base(owner) { }

        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.anim.SetBool(owner.hashTrace, false);
        }
    }
    private class TraceState : BaseMonsterState
    {
        public TraceState(Zombie2Controller owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.SetDestination(owner.playerTr.position);
            owner.agent.isStopped = false;
            owner.anim.SetBool(owner.hashTrace, true);
            owner.anim.SetBool(owner.hashAttack, false);
            owner.anim.SetBool(owner.hashRanged, false);
        }
    }
    private class AttackState : BaseMonsterState
    {
        public AttackState(Zombie2Controller owner) : base(owner) { }
        public override void Enter()
        {
            owner.anim.SetBool(owner.hashTrace, false);
            owner.anim.SetBool(owner.hashAttack, true);
            owner.anim.SetBool(owner.hashRanged, false);
        }
    }

    private class RangedState : BaseMonsterState
    {
        public RangedState(Zombie2Controller owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.agent.velocity = Vector3.zero;
            owner.anim.SetBool(owner.hashTrace, false);
            owner.anim.SetBool(owner.hashAttack, false);
            owner.anim.SetBool(owner.hashRanged, true);
        }
    }

    private class DieState : BaseMonsterState
    {
        public DieState(Zombie2Controller owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.agent.velocity = Vector3.zero;
        }
    }
}

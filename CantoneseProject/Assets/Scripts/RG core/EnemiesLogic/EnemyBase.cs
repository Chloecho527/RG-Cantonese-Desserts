using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("基础属性")]
    public float hp;            // 血量
    public float speed;         // 移动速度
    
    [Header("攻击属性")]
    public bool isContact = false;      // 是否接触到玩家
    public bool isCooling = false;      // 是否处于攻击冷却期间
    public float damage;                // 攻击力
    public float attackTime;            // 攻击间隔
    public float atkTimer = 0;          // 攻击计时器
    public bool isLittleEnemy;          // 是否为小怪
    
    [Header("死亡属性")]
    public int provideExp = 1;              // 被击杀后提供的经验值
    public GameObject money_prefab;         // 被击杀后掉落金币预制体
    
    private void Awake()
    {
        money_prefab = Resources.Load<GameObject>("Money");
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        EnemyMove();
        
        // 攻击
        if(isContact && !isCooling) 
        {
            EnemyAttack();
        }

        if (isCooling)
        {
            atkTimer -= Time.deltaTime;
            
            if (atkTimer <= 0)
            {
                atkTimer = 0;
                isCooling = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContact = true;
        }
    }
    
    /// <summary>
    /// 小怪：接触则造成伤害
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isLittleEnemy)
        {
            isContact = false;
        }
    }

    // TODO 自动移动
    public void EnemyMove()
    {
        Vector2 direction =(Vector2)(Player.Instance.transform.position - this.transform.position).normalized;
        transform.Translate((Vector3)(direction * speed * Time.deltaTime));
        
        EnemyTurnAround();
    }
    
    // TODO 自动转向
    public void EnemyTurnAround()
    {
        // 玩家在怪物右边
        if (Player.Instance.transform.position.x - transform.position.x >= 0.1)
        {
            //怪物向右看
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
        }
        
        else if (Player.Instance.transform.position.x - transform.position.x < 0.1)
        {
            //怪物向左看
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
    
        }
    }
    
    // TODO 攻击
    public void EnemyAttack()
    {
        //如果处于攻击冷却期间, 则返回
        if (isCooling)
        {
            return;
        }

        Player.Instance.PlayerInjured(damage);

        //攻击进入冷却
        isCooling = true;
        atkTimer = attackTime;  
    }
    
    // TODO 受伤
    public void EnemyInjured(float enemyATK)
    {
        // if (isDead)
        // {
        //     return;
        // }

        // 判断本次被攻击是否死亡
        if (hp - enemyATK <= 0)
        {
            hp = 0;
            EnemyDead();
        }
        else
        {
            hp -= enemyATK;
        }
    }
    
    // TODO 死亡
    public void EnemyDead()
    {
        // 增加玩家经验值
        Player.Instance.exp += provideExp;
        GamePanel.Instance.RenewExp();
        
        // 掉落金币
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        // 销毁自身
        Destroy(gameObject);
    }
}

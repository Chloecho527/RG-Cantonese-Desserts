using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("基本属性")]
    public float hp;            // Ѫ��
    public float speed;         // �ƶ��ٶ�
    
    [Header("攻击属性")]
    public bool isContact = false;      // �Ƿ�Ӵ������
    public bool isCooling = false;      // �Ƿ��ڹ�����ȴ�ڼ�
    public float damage;                // ������
    public float attackTime;            // �������
    public float atkTimer = 0;          // ������ʱ��
    public bool isLittleEnemy;          // �Ƿ�ΪС��
    
    [Header("死亡属性")]
    public int provideExp = 1;              // ����ɱ���ṩ�ľ���ֵ
    public GameObject money_prefab;         // ����ɱ�������Ԥ����
    
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
        
        // 小怪接触攻击
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
    /// 小怪接触攻击
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isLittleEnemy)
        {
            isContact = false;
        }
    }

    
    public void EnemyMove()
    {
        Vector2 direction =(Vector2)(Player.Instance.transform.position - this.transform.position).normalized;
        transform.Translate((Vector3)(direction * speed * Time.deltaTime));
        
        EnemyTurnAround();
    }
    
    
    public void EnemyTurnAround()
    {
        // ����ڹ����ұ�
        if (Player.Instance.transform.position.x - transform.position.x >= 0.1)
        {
            //�������ҿ�
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
        }
        
        else if (Player.Instance.transform.position.x - transform.position.x < 0.1)
        {
            //��������
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
    
        }
    }
    
    
    public void EnemyAttack()
    {
        // 处于冷却期间
        if (isCooling)
        {
            return;
        }

        Player.Instance.PlayerInjured(damage);

        // 本次攻击完毕
        isCooling = true;
        atkTimer = attackTime;  
    }
    
    // 怪物受伤
    public void EnemyInjured(float enemyATK)
    {
        // if (isDead)
        // {
        //     return;
        // }

        // 生命值小于0
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
    
    
    public void EnemyDead()
    {
        // 更新经验值
        Player.Instance.exp += provideExp;
        GamePanel.Instance.RenewExp();
        
        // 掉落物
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        // 销毁自身
        Destroy(gameObject);
    }
}

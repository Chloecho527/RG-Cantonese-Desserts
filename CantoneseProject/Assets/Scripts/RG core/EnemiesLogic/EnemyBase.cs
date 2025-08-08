using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("��������")]
    public float hp;            // Ѫ��
    public float speed;         // �ƶ��ٶ�
    
    [Header("��������")]
    public bool isContact = false;      // �Ƿ�Ӵ������
    public bool isCooling = false;      // �Ƿ��ڹ�����ȴ�ڼ�
    public float damage;                // ������
    public float attackTime;            // �������
    public float atkTimer = 0;          // ������ʱ��
    public bool isLittleEnemy;          // �Ƿ�ΪС��
    
    [Header("��������")]
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
        
        // ����
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
    /// С�֣��Ӵ�������˺�
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isLittleEnemy)
        {
            isContact = false;
        }
    }

    // TODO �Զ��ƶ�
    public void EnemyMove()
    {
        Vector2 direction =(Vector2)(Player.Instance.transform.position - this.transform.position).normalized;
        transform.Translate((Vector3)(direction * speed * Time.deltaTime));
        
        EnemyTurnAround();
    }
    
    // TODO �Զ�ת��
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
    
    // TODO ����
    public void EnemyAttack()
    {
        //������ڹ�����ȴ�ڼ�, �򷵻�
        if (isCooling)
        {
            return;
        }

        Player.Instance.PlayerInjured(damage);

        //����������ȴ
        isCooling = true;
        atkTimer = attackTime;  
    }
    
    // TODO ����
    public void EnemyInjured(float enemyATK)
    {
        // if (isDead)
        // {
        //     return;
        // }

        // �жϱ��α������Ƿ�����
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
    
    // TODO ����
    public void EnemyDead()
    {
        // ������Ҿ���ֵ
        Player.Instance.exp += provideExp;
        GamePanel.Instance.RenewExp();
        
        // ������
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        // ��������
        Destroy(gameObject);
    }
}

using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBase : MonoBehaviour
{
    [Header("基本属性")]
    public float hp;            // 生命值
    public float speed;         // 移动速度

    [Header("寻路")] 
    private GameObject player;
    [SerializeField]private Seeker seeker;                                        // seeker组件
    [SerializeField]private List<Vector3> pathPointList = new List<Vector3>();    // 路径点列表
    [SerializeField]private int currentIndex = 0;                                 // 路径点的索引
    private float pathGenerateInterval = 0.5f;                                    // 每 0.5 秒生成一条路径
    private float pathGenerateTimer = 0.5f;                                       // 计时器
    
    [Header("攻击属性")]
    public bool isContact = false;      // 是否接触玩家
    public bool isCooling = false;      // 是否处于攻击冷却期间
    public float damage;                // 攻击力
    public float attackTime;            // 攻击间隔时长
    public float atkTimer = 0;          // 攻击计时器
    // public bool isLittleEnemy;          // 是否为接触式攻击的小怪
    
    [Header("死亡属性")]
    public int provideExp = 1;              // 死亡后提供的经验值
    public GameObject money_prefab;         // 死亡掉落的金币

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        money_prefab = Resources.Load<GameObject>("Money");
        seeker = GetComponent<Seeker>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    
    // 小怪接触攻击
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContact = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isContact = false;
        }
    }
    
    
    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        
        // 三个参数：起点、终点、回调函数
        seeker.StartPath(transform.position, target, path =>
        {
            if (path.error)
            {
                // 路径计算失败时，清空列表并打印错误
                pathPointList.Clear();
                Debug.LogWarning("路径计算失败: " + path.errorLog);
            }
            else
            {
                // 路径计算成功，更新路径点（确保至少有2个点才有效）
                if (path.vectorPath.Count >= 2)
                {
                    pathPointList = new List<Vector3>(path.vectorPath);
                }
                else
                {
                    pathPointList.Clear();
                    Debug.LogWarning("路径点数量不足，无法移动");
                }
            }
        });
        
    }

    private void AutoPath()
    {
        if(player == null)
            return;
        
        pathGenerateTimer += Time.deltaTime;
        
        // 间隔一定时间，获取路径点
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(player.transform.position);
            pathGenerateTimer = 0;   // 重置计时器
            return;
        }
        
        // 当路径点列表为空时，进行路径计算。0.1秒防抖动
        if (pathPointList.Count <= 0 && pathGenerateTimer > 0.1f)
        {
            GeneratePath((player.transform.position));
        }
        // 当怪物到达当前路径点时，递增索引 currentIndex 并进行路径计算
        else if (pathPointList.Count > 0)
        {
            currentIndex = Mathf.Clamp(currentIndex, 0, pathPointList.Count - 1);    // 限制索引
            
            if (Vector2.Distance(transform.position, pathPointList[currentIndex]) < 0.1f)
            {
                currentIndex++;
                if (currentIndex >= pathPointList.Count)
                {
                    GeneratePath(player.transform.position);
                }
            }
        }
    }
    
    
    public void EnemyMove()
    {
        AutoPath();
        
        // 先判断路径列表是否有效，无效则直接返回
        if (pathPointList == null || pathPointList.Count == 0)
        {
            return; // 没有路径时不执行移动逻辑，怪物不移动
        }
        
        // 限制currentIndex不超出列表范围
        currentIndex = Mathf.Clamp(currentIndex, 0, pathPointList.Count - 1);
        
        // Vector2 direction =(Vector2)(Player.Instance.transform.position - this.transform.position).normalized;
        Vector2 direction = (pathPointList[currentIndex] -  transform.position).normalized;
        rb.velocity = direction * speed;
        // transform.Translate((Vector3)(direction * speed * Time.deltaTime));
        
        EnemyTurnAround();
        
    }
    
    // sprite翻转
    public void EnemyTurnAround()
    {
        // 玩家在怪物右边
        if (Player.Instance.transform.position.x - transform.position.x >= 0.1)
        {
            // 怪物向右看
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
        }
        
        else if (Player.Instance.transform.position.x - transform.position.x < 0.1)
        {
            // 怪物向左看
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x ), transform.localScale.y, transform.localScale.z);
        }
    }
    
    // 怪物攻击
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
    public void EnemyInjured(float enemyAtk)
    {
        // if (isDead)
        // {
        //     return;
        // }

        // 生命值小于0
        if (hp - enemyAtk <= 0)
        {
            hp = 0;
            EnemyDead();
        }
        else
        {
            hp -= enemyAtk;
        }
    }
    
    
    public void EnemyDead()
    {
        // 更新经验值
        Player.Instance.exp += provideExp;
        GamePanel.Instance.RenewExp();
        
        // 掉落物
        Instantiate(money_prefab, transform.position, Quaternion.identity);
        
        // 广播敌人死亡（用于统计在场数量）
        EventHandler.CallEnemyDiedEvent(this);
        
        // 销毁自身
        Destroy(gameObject);
    }
}

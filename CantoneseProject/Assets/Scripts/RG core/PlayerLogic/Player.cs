using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [Header("基本属性")]
    public float speed = 5f;     // TEST 速度暂时为 5
    public float hp = 15f;       // 当前生命值
    public float maxHp = 15f;    // 最大生命值
    public float exp = 0;        // TEST 当前经验值
    
    [Header("状态")]
    public bool isDead = false;      // 是否死亡
    
    [Header("脚本组件获取")]
    public Animator anim;                  // 动画机
    public Transform playerSpriteTrans;    // 角色sprite位置
    public int money = 30;                 // 当前金币

    
    private void Awake()
    {
        Instance = this;
        
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
        anim = playerSpriteTrans.GetComponent<Animator>();
        
        // TEST 对应角色的动画
        if (!string.IsNullOrEmpty(GameManager.Instance.currentRole.animatorController))
        {
            RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(GameManager.Instance.currentRole.animatorController);
            if (controller != null)
            {
                anim.runtimeAnimatorController = controller;
            }
        }
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        
        Move();
    }

    // 吃金币
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("jinbi?");
        if (other.CompareTag("Money"))   // 吃金币
        {
            Destroy(other.gameObject);
            money += 1;
            GamePanel.Instance.RenewMoney();
        }
    }

    // wasd移动
    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // 1 或 -1
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
        
        // 动画机，判断角色是否移动
        if (anim != null)
        {
            if (movement.magnitude != 0 )
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }
        }
    
        TurnAround(moveHorizontal);
    }
    
    // sprite翻转
    public void TurnAround(float h)
    {
        if (h < 0)        //  TEST localScale暂时为 5，后续调整
        {
            playerSpriteTrans.localScale = new Vector3(-5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
        else if (h > 0)   //  TEST localScale暂时为 5，后续调整
        {
            playerSpriteTrans.localScale = new Vector3(5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
    }
    
    // TODO 攻击
    public void PlayerAttack()
    {
        
    }
    
    // TODO 受伤
    public void PlayerInjured(float enemyATK)
    {
        if (isDead)
        {
            return;
        }

        // 判断本次攻击是否死亡
        if (hp - enemyATK <= 0)
        {
            hp = 0;
            PlayerDead();
        }
        else
        {
            hp -= enemyATK;
        }
        
        //更新血条
        GamePanel.Instance.RenewHp();
    }
    
    // TODO 死亡
    public void PlayerDead()
    {
        isDead = true;
        anim.speed = 0f;
        
        // 调用游戏失败函数
        //LevelController.Instance.FailGame();
        // TODO 
        WaveManager.Instance.FailGame();
    }
}

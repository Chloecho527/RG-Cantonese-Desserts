using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public float speed = 5f;                 // TEST 速度暂时为 5
    public Transform playerSpriteTrans;
    public Animator anim;   // 动画机
    public float hp = 15f;        // 血量
    public bool isDead = false;

    
    private void Awake()
    {
        Instance = this;
        
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
        anim = playerSpriteTrans.GetComponent<Animator>();

        Debug.Log(GameManager.Instance.currentRole.animatorController);
        // TEST 对应角色的动画
        if (!string.IsNullOrEmpty(GameManager.Instance.currentRole.animatorController))
        {
            RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(GameManager.Instance.currentRole.animatorController);
            if (controller != null)
            {
                anim.runtimeAnimatorController = controller;
            }
        }
        // if (GameManager.Instance.currentRole != null)
        // {
        //     string controllerPath = "Animations/" + GameManager.Instance.currentRole.ID + "_Controller";
        //     RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerPath);
        //     if (controller != null)
        //     {
        //         anim.runtimeAnimatorController = controller;
        //     }
        // }
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        
        Move();
    }

    /// <summary>
    /// wasd 移动
    /// </summary>
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
        //Debug.Log(playerSpriteTrans.localScale.x);
    }
    
    /// <summary>
    /// sprite翻转
    /// </summary>
    /// <param name="h"></param>
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
    }
    
    // TODO 死亡
    public void PlayerDead()
    {
        isDead = true;
        anim.speed = 0f;
        
        // TODO 调用游戏失败函数
    }
}

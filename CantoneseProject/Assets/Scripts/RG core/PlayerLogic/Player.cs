using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public float speed = 5f;                 // TEST 速度暂时为 5
    public Transform playerSpriteTrans;
    public Animator anim;   // 动画机

    
    private void Awake()
    {
        Instance = this;
        
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
        anim = GetComponentInChildren<Animator>();

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
        Move();
    }

    // TODO wasd移动
    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // 1 或 -1
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
    
        TurnAround(moveHorizontal);
        //Debug.Log(playerSpriteTrans.localScale.x);
    }
    
    // TODO 精灵翻转函数
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
        
    }
    
    // TODO 死亡
    public void PlayerDead()
    {
        
    }
}

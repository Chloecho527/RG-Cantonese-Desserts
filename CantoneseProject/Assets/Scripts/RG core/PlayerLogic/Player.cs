using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;                 // TEST 速度暂时为 5
    public Transform playerSpriteTrans;

    private void Awake()
    {
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
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
        Debug.Log(movement);
    }
    
    // TODO 精灵翻转函数
    public void TurnAround(float h)
    {
        if (h == -1)        //  TEST localScale暂时为 5，后续调整
        {
            playerSpriteTrans.localScale = new Vector3(-5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
        else if (h == 1)   //  TEST localScale暂时为 5，后续调整
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

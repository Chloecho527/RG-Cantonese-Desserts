using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;                 // TEST �ٶ���ʱΪ 5
    public Transform playerSpriteTrans;

    private void Awake()
    {
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
    }

    private void Update()
    {
        Move();
    }

    // TODO wasd�ƶ�
    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // 1 �� -1
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
    
        TurnAround(moveHorizontal);
        //Debug.Log(playerSpriteTrans.localScale.x);
        Debug.Log(movement);
    }
    
    // TODO ���鷭ת����
    public void TurnAround(float h)
    {
        if (h == -1)        //  TEST localScale��ʱΪ 5����������
        {
            playerSpriteTrans.localScale = new Vector3(-5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
        else if (h == 1)   //  TEST localScale��ʱΪ 5����������
        {
            playerSpriteTrans.localScale = new Vector3(5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
    }
    
    // TODO ����
    public void PlayerAttack()
    {
        
    }
    
    // TODO ����
    public void PlayerInjured(float enemyATK)
    {
        
    }
    
    // TODO ����
    public void PlayerDead()
    {
        
    }
}

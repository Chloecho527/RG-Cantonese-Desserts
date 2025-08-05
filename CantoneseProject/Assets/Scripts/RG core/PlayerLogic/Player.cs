using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public float speed = 5f;                 // TEST �ٶ���ʱΪ 5
    public Transform playerSpriteTrans;
    public Animator anim;   // ������
    public float hp = 15f;        // Ѫ��
    public bool isDead = false;

    
    private void Awake()
    {
        Instance = this;
        
        playerSpriteTrans = GameObject.Find("PlayerSprite").transform;
        anim = playerSpriteTrans.GetComponent<Animator>();

        Debug.Log(GameManager.Instance.currentRole.animatorController);
        // TEST ��Ӧ��ɫ�Ķ���
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
    /// wasd �ƶ�
    /// </summary>
    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");  // 1 �� -1
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        transform.Translate(movement * speed * Time.deltaTime);
        
        // ���������жϽ�ɫ�Ƿ��ƶ�
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
    /// sprite��ת
    /// </summary>
    /// <param name="h"></param>
    public void TurnAround(float h)
    {
        if (h < 0)        //  TEST localScale��ʱΪ 5����������
        {
            playerSpriteTrans.localScale = new Vector3(-5, playerSpriteTrans.localScale.y, playerSpriteTrans.localScale.z);
        }
        else if (h > 0)   //  TEST localScale��ʱΪ 5����������
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
        if (isDead)
        {
            return;
        }

        // �жϱ��ι����Ƿ�����
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
    
    // TODO ����
    public void PlayerDead()
    {
        isDead = true;
        anim.speed = 0f;
        
        // TODO ������Ϸʧ�ܺ���
    }
}

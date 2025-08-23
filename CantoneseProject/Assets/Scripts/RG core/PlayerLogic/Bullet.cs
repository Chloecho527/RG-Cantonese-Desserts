using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;                     // 子弹飞行速度
    public GameObject explosionPrefab;
    new private Rigidbody2D rigidbody;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // 子弹移动
    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }
    
    // 子弹碰撞，销毁
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 爆炸特效
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        // 销毁子弹
        ObjectPool.Instance.PushObject(gameObject);
    }
}

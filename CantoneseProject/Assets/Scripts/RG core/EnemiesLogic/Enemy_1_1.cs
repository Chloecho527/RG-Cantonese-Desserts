using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_1_1 : EnemyBase
{
    public void Start()
    {
        speed = 1f;
        hp = 8f;
        damage = 1f;
        attackTime = 1.5f;
        // isLittleEnemy = true;
    }
}

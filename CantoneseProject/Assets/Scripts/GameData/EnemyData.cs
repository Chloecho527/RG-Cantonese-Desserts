using System;
using UnityEngine;

[Serializable]
public enum EnemyType
{
    Small,      // 小怪
    Elite,      // 精英怪
    Boss        // Boss
}

[Serializable]
public enum EnemyBehavior
{
    Contact,    // 接触伤害
    Chase,      // 追击攻击
    Ranged      // 远程攻击
}

[Serializable]
public class EnemyData
{
    [Header("基本信息")]
    public int ID;                    // 怪物唯一ID
    public string name;               // 怪物名称
    public EnemyType enemyType;       // 怪物类型
    // public EnemyBehavior behavior;    // 行为模式
    public string prefabPath;         // 预制体路径
    
    // [Header("基础属性")]
    // public float maxHealth;           // 最大生命值
    // public float moveSpeed;           // 移动速度
    // public float damage;              // 攻击力
    // public float attackRange;         // 攻击范围
    // public float attackCooldown;      // 攻击冷却时间
    // public float detectionRange;      // 检测范围
    //
    // [Header("特殊属性")]
    // public float criticalChance;      // 暴击率
    // public float criticalMultiplier;  // 暴击倍数
    //
    // [Header("奖励")]
    // public int expReward;             // 经验奖励
    // public int moneyReward;           // 金钱奖励
    // //public string[] dropItems;        // 掉落物品
    
    // [Header("视觉效果")]
    // public string spritePath;         // 精灵图片路径
    // public string animatorPath;       // 动画控制器路径
}
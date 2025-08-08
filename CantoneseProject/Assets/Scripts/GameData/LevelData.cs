using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnConfig
{
    public int enemyDataID;           // 怪物数据ID
    public int spawnCount;            // 生成数量
    public float spawnDelay;          // 生成延迟
    public Vector2[] spawnPositions;  // 生成位置（可选，为空则随机生成）
}

[Serializable]
public class LevelData
{
    [Header("关卡信息")]
    public int levelNumber;           // 关卡编号（1-10）
    public string levelName;          // 关卡名称
    public float levelDuration;       // 关卡持续时间（秒）
    
    [Header("怪物配置")]
    public List<EnemySpawnConfig> enemySpawns;  // 怪物生成配置
    
    [Header("关卡奖励")]
    public int baseExpReward;         // 基础经验奖励
    public int baseMoneyReward;       // 基础金钱奖励
    public string[] levelRewards;     // 关卡特殊奖励
    
    [Header("环境设置")]
    public string backgroundPath;     // 背景图片路径
    public Color ambientColor;        // 环境光颜色
    public float difficulty;          // 难度系数（1.0-2.0）
}
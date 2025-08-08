using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // 支持JSON序列化/反序列化
public class MapData
{
    [Header("基本信息")]
    public int ID;                // 地图ID
    public string name;           // UI显示的名称
    public string mapSceneName;   // 地图场景名称     
    public string mapPath;        // 地图预览图路径
    public string describe;       // 地图描述
    
    [Header("关卡配置")]
    public List<LevelData> levels;  // 地图包含的10个关卡
    public float mapDifficulty;     // 地图整体难度系数
    
    [Header("地图特殊设置")]
    public string[] availableEnemies;  // 该地图可用的怪物ID列表
    public Color mapThemeColor;        // 地图主题色
    public string backgroundMusic;     // 背景音乐路径
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // 允许JSON序列化/反序列化
public class MapData
{
    public int ID;                // 地图ID
    public string name;           // UI显示的名字
    public string mapSceneName;   // 地图场景名字     
    public string mapPath;        // 地图预览图路径
    public string describe;       // 敌人详情
}

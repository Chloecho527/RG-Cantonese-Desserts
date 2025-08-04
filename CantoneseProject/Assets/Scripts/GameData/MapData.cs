using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // 允许JSON序列化/反序列化
public class MapData
{
    public int ID;              // 地图ID
    public string name;         // 地图名字
    public string mapPath;   // 地图预览图路径
    public string describe;     // 敌人详情
}

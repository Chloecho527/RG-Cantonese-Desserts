using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public int ID;                              // 武器唯一ID
    public int roleID;                          // 关联的角色ID（用于筛选）
    public string name;                         // 武器名称
    public int isLong;                          // 是否为远程攻击
    public string iconPath;                     // 武器图标路径（Resources下）
    public string describe;                     // 武器描述（如"增加10%攻击力"）
    public float attack;                        // 武器攻击力
    public int range;                           // 武器攻击范围
    public float cooling;                       // 冷却时长
    public int repel;                           // 击退效果
    public float criticalStrikeProbability;     // 暴击概率
    public float criticalStrikeMultiple;        // 暴击倍率
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // 允许JSON序列化/反序列化
public class RoleData
{
    public int ID;               // 角色ID
    
    [Header("角色选择面板数据")]
    public string name;          // 角色名字
    public string faction;       // 流派
    public string avatarPath;    // 角色头像
    public string describe;      // 角色台词和属性
    public int slot;             // 武器插槽
    
    [Header("肉鸽关卡数据")]
    public string animatorController;  // 不同角色的动画机
        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public RoleData currentRole;       // 选择的角色
    public WeaponData currentWeapon;   // 选择的武器
    public MapData currentMap;         // 选择的地图
    
}

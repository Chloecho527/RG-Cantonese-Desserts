using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public RoleData currentRole;       // 选择的角色
    public WeaponData currentWeapon;   // 选择的武器
    public MapData currentMap;         // 选择的地图

    
    [Header("角色数据")]
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;
    
    protected override void Awake()
    {
        // 读取角色数据json文件，转换文本
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
    }
}

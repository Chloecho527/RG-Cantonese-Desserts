using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public RoleData currentRole;       // 选择的角色
    public WeaponData currentWeapon;   // 选择的武器
    public MapData currentMap;         // 选择的地图
    public int currentWave = 1;        // 当前波次

    
    [Header("角色数据")]
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;
    
    [Header("敌人数据")]
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public TextAsset enemyTextAsset;
    
    protected override void Awake()
    {
        // 读取角色数据json文件，转换文本
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
        
        // 读取敌人数据（用于按名称找到预制体路径）
        enemyTextAsset = Resources.Load<TextAsset>(path: "Data/Enemy");
        if (enemyTextAsset != null)
        {
            enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(enemyTextAsset.text);
        }
    }
    
    public EnemyData GetEnemyByName(string name)
    {
        // return enemyDatas.FirstOrDefault(e => e.name == name);
        var enemy = enemyDatas.FirstOrDefault(e => e.name == name);
        if (enemy == null)
        {
            Debug.LogError($"未找到怪物数据：{name}，请检查 JSON 配置或波次配置！");
        }
        return enemy;
    }
}

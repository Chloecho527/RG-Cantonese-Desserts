using Microsoft.Unity.VisualStudio.Editor;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class WeaponSelectPanel : Singleton<WeaponSelectPanel>
{
    [Header("武器数据")]
    public List<WeaponData> weaponDatas = new List<WeaponData>();
    public TextAsset weaponTextAsset;
    
    [Header("组件和物体")]
    public Transform weaponList;     // 场景中的父物体RoleList
    public GameObject weaponPrefab;
    public CanvasGroup canvasGroup;
    public Transform weaponDetails;
    public Transform weaponContent;
    
    [Header("用于鼠标滑入时角色信息更新的UI组件")]
    public TextMeshProUGUI weaponName;   // 武器名称

    public TextMeshProUGUI weaponType;   // 武器类型（近or远）
    public Image weaponIcon;   // 武器图标
    public TextMeshProUGUI weaponDescribe;      // 武器属性
    
    
    protected override void Awake()
    {
        // 获取预制体信息
        weaponList = GameObject.Find("WeaponList").transform; 
        weaponPrefab = Resources.Load<GameObject>(path: "Prefabs/Weapon");   // 角色预制体

        
        // 读取武器数据json文件，转换文本
        weaponTextAsset = Resources.Load<TextAsset>(path: "Data/Weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
        
        // 组件获取
        weaponName = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        weaponType = GameObject.Find("WeaponType").GetComponent<TextMeshProUGUI>();
        weaponIcon = GameObject.Find("WeaponIcon").GetComponent<Image>();
        weaponDescribe = GameObject.Find("WeaponDescribe").GetComponent<TextMeshProUGUI>();
        
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        weaponContent = GameObject.Find("WeaponContent").transform;
        weaponDetails = GameObject.Find("WeaponDetails").transform;
    }

    private void Start()
    {
        foreach (WeaponData weaponData in weaponDatas)
        {
            WeaponUI weaponUI = GameObject.Instantiate(weaponPrefab, weaponList).GetComponent<WeaponUI>();
            weaponUI.SetData(weaponData);
        }
    }

    /// <summary>
    /// 选择角色后仅显示专武
    /// </summary>
    /// <param name="rID"></param>
    public void FilterWeaponsByRole(int roleID)
    {
        // 清空现有武器UI(全武器)
        foreach (Transform child in weaponList)
        {
            Destroy(child.gameObject);
        }

        // 筛选出当前角色的专武（roleID匹配的武器）
        List<WeaponData> roleWeapons = weaponDatas.FindAll(w => w.roleID == roleID);

        // 生成筛选后的武器UI
        foreach (WeaponData weaponData in roleWeapons)
        {
            WeaponUI weaponUI = Instantiate(weaponPrefab, weaponList).GetComponent<WeaponUI>();
            weaponUI.SetData(weaponData);
        }
    }
}

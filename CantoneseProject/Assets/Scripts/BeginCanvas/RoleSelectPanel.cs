using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : Singleton<RoleSelectPanel>
{
    [Header("角色数据")]
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;

    [Header("组件和物体")]
    public Transform roleList;           // 场景中的父物体RoleList
    public GameObject rolePrefab;
    public CanvasGroup canvasGroup;
    public GameObject roleDetails;

    [Header("用于鼠标滑入时角色信息更新的UI组件")]
    public TextMeshProUGUI roleName;          // 角色名称
    public TextMeshProUGUI roleFaction;       // 角色流派
    public UnityEngine.UI.Image roleAvatar;   // 角色头像
    public TextMeshProUGUI roleDescribe;      // 角色台词和属性


    protected override void Awake()
    {
        // 获取预制体信息
        roleList = GameObject.Find("RoleList").transform;                // 父物体
        rolePrefab = Resources.Load<GameObject>(path: "Prefabs/Role");   // 角色预制体

        // 读取角色数据json文件，转换文本
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        // 组件获取
        roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        roleFaction = GameObject.Find("RoleFaction").GetComponent<TextMeshProUGUI>();
        roleAvatar = GameObject.Find("RoleAvatar").GetComponent<Image>();
        roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
        
        canvasGroup = GetComponent<CanvasGroup>();
        roleDetails = GameObject.Find("RoleDetails");
    }

    private void Start()
    {
        foreach(RoleData roleData in roleDatas)
        {
            RoleUI roleUI = GameObject.Instantiate(rolePrefab, roleList).GetComponent<RoleUI>();
            roleUI.SetData(roleData);
        }
    }
}

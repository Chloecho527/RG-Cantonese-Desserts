using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : Singleton<RoleSelectPanel>
{
    [Header("��ɫ����")]
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;

    [Header("���������")]
    public Transform roleList;   // �����еĸ�����RoleList
    public GameObject rolePrefab;
    public CanvasGroup canvasGroup;
    public GameObject roleDetails;

    [Header("������껬��ʱ��ɫ��Ϣ���µ�UI���")]
    public TextMeshProUGUI roleName;          // ��ɫ����
    public TextMeshProUGUI roleFaction;       // ��ɫ����
    public UnityEngine.UI.Image roleAvatar;   // ��ɫͷ��
    public TextMeshProUGUI roleDescribe;      // ��ɫ̨�ʺ�����


    protected override void Awake()
    {
        // ��ȡԤ������Ϣ
        roleList = GameObject.Find("RoleList").transform;                // ������
        rolePrefab = Resources.Load<GameObject>(path: "Prefabs/Role");   // ��ɫԤ����

        // ��ȡ��ɫ����json�ļ���ת���ı�
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        // �����ȡ
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

using Microsoft.Unity.VisualStudio.Editor;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : Singleton<RoleSelectPanel>
{
    // ��ɫ������Ϣ
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;

    // ��ȡ���������
    public Transform roleList;   // �����еĸ�����RoleList
    public GameObject rolePrefab;

    // �����������껬��ʱ��ɫ��Ϣ����
    public TextMeshProUGUI roleName;   // ��ɫ����
    public TextMeshProUGUI roleFaction;   // ��ɫ����
    public UnityEngine.UI.Image roleAvatar;   // ��ɫͷ��
    public TextMeshProUGUI roleDescribe;   // ��ɫ̨�ʺ�����


    protected override void Awake()
    {
        roleList = GameObject.Find("RoleList").transform;   // ������
        rolePrefab = Resources.Load<GameObject>(path: "Prefabs/Role");   // ��ɫԤ����

        // ��ȡ��ɫ����json�ļ���ת���ı�
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        // �����ȡ
        roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        roleFaction = GameObject.Find("RoleFaction").GetComponent<TextMeshProUGUI>();
        roleAvatar = GameObject.Find("RoleAvatar").GetComponent<UnityEngine.UI.Image>();
        roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
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

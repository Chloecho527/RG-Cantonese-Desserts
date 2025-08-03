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
    [Header("��������")]
    public List<WeaponData> weaponDatas = new List<WeaponData>();
    public TextAsset weaponTextAsset;
    
    [Header("���������")]
    public Transform weaponList;     // �����еĸ�����RoleList
    public GameObject weaponPrefab;
    public CanvasGroup canvasGroup;
    public Transform weaponDetails;
    public Transform weaponContent;
    
    [Header("������껬��ʱ��ɫ��Ϣ���µ�UI���")]
    public TextMeshProUGUI weaponName;   // ��������

    public TextMeshProUGUI weaponType;   // �������ͣ���orԶ��
    public Image weaponIcon;   // ����ͼ��
    public TextMeshProUGUI weaponDescribe;      // ��������
    
    
    protected override void Awake()
    {
        // ��ȡԤ������Ϣ
        weaponList = GameObject.Find("WeaponList").transform; 
        weaponPrefab = Resources.Load<GameObject>(path: "Prefabs/Weapon");   // ��ɫԤ����

        
        // ��ȡ��������json�ļ���ת���ı�
        weaponTextAsset = Resources.Load<TextAsset>(path: "Data/Weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
        
        // �����ȡ
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
    /// ѡ���ɫ�����ʾר��
    /// </summary>
    /// <param name="rID"></param>
    public void FilterWeaponsByRole(int roleID)
    {
        // �����������UI(ȫ����)
        foreach (Transform child in weaponList)
        {
            Destroy(child.gameObject);
        }

        // ɸѡ����ǰ��ɫ��ר�䣨roleIDƥ���������
        List<WeaponData> roleWeapons = weaponDatas.FindAll(w => w.roleID == roleID);

        // ����ɸѡ�������UI
        foreach (WeaponData weaponData in roleWeapons)
        {
            WeaponUI weaponUI = Instantiate(weaponPrefab, weaponList).GetComponent<WeaponUI>();
            weaponUI.SetData(weaponData);
        }
    }
}

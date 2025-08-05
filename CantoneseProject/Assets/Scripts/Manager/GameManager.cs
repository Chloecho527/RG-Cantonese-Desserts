using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : Singleton<GameManager>
{
    public RoleData currentRole;       // ѡ��Ľ�ɫ
    public WeaponData currentWeapon;   // ѡ�������
    public MapData currentMap;         // ѡ��ĵ�ͼ

    
    [Header("��ɫ����")]
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;
    
    protected override void Awake()
    {
        // ��ȡ��ɫ����json�ļ���ת���ı�
        roleTextAsset = Resources.Load<TextAsset>(path: "Data/Role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
    }
}

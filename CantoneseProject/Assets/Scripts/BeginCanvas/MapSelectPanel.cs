using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectPanel : Singleton<MapSelectPanel>
{
    [Header("��ͼ����")]
    public List<MapData> mapDatas = new List<MapData>();
    public TextAsset mapTextAsset;
    
    [Header("���������")]
    public Transform mapList;                // �����еĸ�����MapList
    public GameObject mapPrefab;
    public Transform mapContentTrans;
    public CanvasGroup canvasGroup;
    public GameObject mapDetails;
    
    [Header("������껬��ʱ��ͼ��Ϣ���µ�UI���")]
    public TextMeshProUGUI mapName;          // ��ͼ����
    public Image mapAvatar;                  // ��ͼԤ��ͼ
    public TextMeshProUGUI mapDescribe;      // ��������

    protected override void Awake()
    {
        // ��ȡԤ������Ϣ
        mapList = GameObject.Find("MapList").transform;                // ������
        mapPrefab = Resources.Load<GameObject>(path: "Prefabs/Map");    // Ԥ��ͼԤ����
        
        // ��ȡ��ͼ����json�ļ���ת���ı�
        mapTextAsset = Resources.Load<TextAsset>(path: "Data/Map");
        mapDatas = JsonConvert.DeserializeObject<List<MapData>>(mapTextAsset.text);
        
        // �����ȡ
        mapName = GameObject.Find("MapName").GetComponent<TextMeshProUGUI>();
        mapAvatar = GameObject.Find("MapAvatar").GetComponent<Image>();
        mapDescribe = GameObject.Find("MapDescribe").GetComponent<TextMeshProUGUI>();
        mapContentTrans = GameObject.Find("MapContent").transform;
        
        canvasGroup = GetComponent<CanvasGroup>();
        mapDetails = GameObject.Find("MapDetails");
    }

    private void Start()
    {
        foreach (MapData mapData in mapDatas)
        {
            MapUI mapUI = Instantiate(mapPrefab, mapList).GetComponentInChildren<MapUI>();
            mapUI.SetData(mapData);
        }
    }
}

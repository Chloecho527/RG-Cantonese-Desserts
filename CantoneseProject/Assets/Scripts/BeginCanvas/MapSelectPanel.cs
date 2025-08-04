using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectPanel : Singleton<MapSelectPanel>
{
    [Header("地图数据")]
    public List<MapData> mapDatas = new List<MapData>();
    public TextAsset mapTextAsset;
    
    [Header("组件和物体")]
    public Transform mapList;   // 场景中的父物体MapList
    public GameObject mapPrefab;
    public Transform mapContentTrans;
    public CanvasGroup canvasGroup;
    
    [Header("用于鼠标滑入时地图信息更新的UI组件")]
    public TextMeshProUGUI mapName;          // 地图名称
    public Image mapAvatar;   // 地图预览图
    public TextMeshProUGUI mapDescribe;      // 敌情描述

    protected override void Awake()
    {
        // 获取预制体信息
        mapList = GameObject.Find("MapList").transform;                // 父物体
        mapPrefab = Resources.Load<GameObject>(path: "Prefabs/Map");    // 预览图预制体
        
        // 读取地图数据json文件，转换文本
        mapTextAsset = Resources.Load<TextAsset>(path: "Data/Map");
        mapDatas = JsonConvert.DeserializeObject<List<MapData>>(mapTextAsset.text);
        
        mapContentTrans = GameObject.Find("MapContent").transform;
        
        canvasGroup = GetComponent<CanvasGroup>();
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

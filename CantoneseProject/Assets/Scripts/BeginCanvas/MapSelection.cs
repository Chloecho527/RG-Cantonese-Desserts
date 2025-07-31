using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关联btn和对应地图
/// </summary>
[System.Serializable]
public class MapButtonData
{
    public Button mapButton;
    public string sceneName;
}



public class MapSelection : MonoBehaviour
{
    [Header("画布引用")]
    [SerializeField] private Canvas mapCanvas;

    [Header("地图按钮配置")]
    [SerializeField] private MapButtonData[] mapButtons; // 可编辑的地图按钮数组

    private void Awake()
    {
        // 为每个地图按钮注册点击事件
        foreach (var mapData in mapButtons)
        {
            string sceneName = mapData.sceneName; // 捕获场景名称
            mapData.mapButton.onClick.AddListener(() => OnMapSelected(sceneName));
        }
    }

    /// <summary>
    /// 地图选择事件
    /// </summary>
    private void OnMapSelected(string sceneName)
    {
        // 渐隐渐出后加载选中的地图场景
        LoadManager.Instance.FadeAndLoadMap(sceneName);
    }
}

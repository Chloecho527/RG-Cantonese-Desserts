using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����btn�Ͷ�Ӧ��ͼ
/// </summary>
[System.Serializable]
public class MapButtonData
{
    public Button mapButton;
    public string sceneName;
}



public class MapSelection : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private Canvas mapCanvas;

    [Header("��ͼ��ť����")]
    [SerializeField] private MapButtonData[] mapButtons; // �ɱ༭�ĵ�ͼ��ť����

    private void Awake()
    {
        // Ϊÿ����ͼ��ťע�����¼�
        foreach (var mapData in mapButtons)
        {
            string sceneName = mapData.sceneName; // ���񳡾�����
            mapData.mapButton.onClick.AddListener(() => OnMapSelected(sceneName));
        }
    }

    /// <summary>
    /// ��ͼѡ���¼�
    /// </summary>
    private void OnMapSelected(string sceneName)
    {
        // �������������ѡ�еĵ�ͼ����
        LoadManager.Instance.FadeAndLoadMap(sceneName);
    }
}

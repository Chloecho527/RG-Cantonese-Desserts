using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: home��select 
/// </summary>
public class Home : MonoBehaviour
{
    [SerializeField] private Canvas homeCanvas;
    [SerializeField] private Canvas selectCanvas;
    [SerializeField] private Canvas memoryCanvas;

    [SerializeField] private Button RGBtn;
    [SerializeField] private Button memoryBtn;

    private void Awake()
    {
        GameObject homeCanvasObj = this.gameObject;

        homeCanvas = homeCanvasObj.GetComponent<Canvas>();
        //selectCanvas = GameObject.Find("Select Canvas").GetComponent<Canvas>();
        //memoryCanvas = GameObject.Find("Memory Canvas").GetComponent<Canvas>();

        RGBtn = homeCanvasObj.transform.Find("RG Btn").GetComponent<Button>();
        memoryBtn = homeCanvasObj.transform.Find("Memory Btn").GetComponent<Button>();

        // ע�ᰴť�¼�
        RGBtn.onClick.AddListener(OnRGBtnClicked);
        memoryBtn.onClick.AddListener(OnMemoryBtnClicked);


    }

    /// <summary>
    /// ���rg��ť�¼�����canvas��ת�����ѡ��canvas
    /// </summary>
    private void OnRGBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(selectCanvas, homeCanvas);
    }

    /// <summary>
    /// ������䰴ť�¼�����canvas��ת�����ѡ��canvas
    /// </summary>
    private void OnMemoryBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(memoryCanvas, homeCanvas);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: home到select 
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

        // 注册按钮事件
        RGBtn.onClick.AddListener(OnRGBtnClicked);
        memoryBtn.onClick.AddListener(OnMemoryBtnClicked);


    }

    /// <summary>
    /// 点击rg按钮事件，家canvas跳转到肉鸽选择canvas
    /// </summary>
    private void OnRGBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(selectCanvas, homeCanvas);
    }

    /// <summary>
    /// 点击回忆按钮事件，家canvas跳转到肉鸽选择canvas
    /// </summary>
    private void OnMemoryBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(memoryCanvas, homeCanvas);
    }
}

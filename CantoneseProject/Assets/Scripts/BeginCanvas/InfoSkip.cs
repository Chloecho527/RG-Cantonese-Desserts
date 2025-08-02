using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: info��home 
/// </summary>
public class InfoSkip : MonoBehaviour
{
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private Canvas homeCanvas;

    [SerializeField] private Button skipBtn;


    private void Awake()
    {
        GameObject infoCanvasObj = this.gameObject;
        infoCanvas = infoCanvasObj.GetComponent<Canvas>();
        homeCanvas = GameObject.Find("Home Canvas").GetComponent<Canvas>();

        // ��ȡ��ť
        skipBtn = infoCanvasObj.transform.Find("Skip Btn").GetComponent<Button>();

        // ע�ᰴť�¼�
        skipBtn.onClick.AddListener(OnSkipBtnClicked);
    }

    /// <summary>
    /// ��ť����¼�������canvas��ת����canvas
    /// </summary>
    private void OnSkipBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(homeCanvas, infoCanvas);
    }
}

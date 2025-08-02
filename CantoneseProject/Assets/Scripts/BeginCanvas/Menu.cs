using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: start到info 
/// </summary>
public class Menu : MonoBehaviour
{

    [Header("画布引用")]
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private Canvas homeCanvas;
    // [SerializeField] private Canvas characterCanvas;
    [SerializeField] private Canvas selectCanvas;
    // [SerializeField] private Canvas remindCanvas;


    [Header("按钮引用")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        GameObject startCanvasObj = this.gameObject;

        // 获取画布
        startCanvas = startCanvasObj.GetComponent<Canvas>();
        infoCanvas = GameObject.Find("Info Canvas").GetComponent<Canvas>();
        homeCanvas = GameObject.Find("Home Canvas").GetComponent<Canvas>();
        // characterCanvas = GameObject.Find("Character Canvas").GetComponent<Canvas>();
        selectCanvas = GameObject.Find("Select Canvas").GetComponent<Canvas>();

        // 获取按钮
        newGameButton = startCanvasObj.transform.Find("NewGameBtn").GetComponent<Button>();
        exitButton = startCanvasObj.transform.Find("ExitBtn").GetComponent<Button>();

        // 注册按钮事件
        newGameButton.onClick.AddListener(OnNewGameClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    private void Start()   // 获取变量后，其他canvas设置非激活状态
    {
        infoCanvas.gameObject.SetActive(false);
        homeCanvas.gameObject.SetActive(false);
        // characterCanvas.gameObject.SetActive(false);
        selectCanvas.gameObject.SetActive(false);

    }

    /// <summary>
    /// 新游戏按钮点击事件
    /// </summary>
    private void OnNewGameClicked()
    {
        // 渐隐渐出后激活故事背景介绍画布
        LoadManager.Instance.FadeAndActivateCanvas(infoCanvas, startCanvas);
    }

    /// <summary>
    /// 退出按钮点击事件
    /// </summary>
    private void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

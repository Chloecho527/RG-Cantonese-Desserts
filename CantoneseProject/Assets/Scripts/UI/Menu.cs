using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{

    [Header("画布引用")]
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas characterCanvas;

    [Header("按钮引用")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        // 注册按钮事件
        newGameButton.onClick.AddListener(OnNewGameClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    /// <summary>
    /// 新游戏按钮点击事件
    /// </summary>
    private void OnNewGameClicked()
    {
        // 渐隐渐出后激活角色选择画布
        LoadManager.Instance.FadeAndActivateCanvas(characterCanvas, startCanvas);
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

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: info到home 
/// </summary>
public class InfoSkip : MonoBehaviour
{
    [Header("canvas组件")]
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private Canvas homeCanvas;

    [Header("button组件")]
    [SerializeField] private Button skipBtn;


    private void Awake()
    {
        GameObject infoCanvasObj = this.gameObject;
        infoCanvas = infoCanvasObj.GetComponent<Canvas>();
        homeCanvas = GameObject.Find("Home Canvas").GetComponent<Canvas>();

        // 获取按钮
        skipBtn = infoCanvasObj.transform.Find("Skip Btn").GetComponent<Button>();

        // 注册按钮事件
        skipBtn.onClick.AddListener(OnSkipBtnClicked);
    }

    /// <summary>
    /// 按钮点击事件，介绍canvas跳转到家canvas
    /// </summary>
    private void OnSkipBtnClicked()
    {
        LoadManager.Instance.FadeAndActivateCanvas(homeCanvas, infoCanvas);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadManager : Singleton<LoadManager>
{
    [Header("转场设置")]
    [SerializeField] private Canvas fadeCanvas; // 转场用的画布
    [SerializeField] private Image transitionImage; // 转场用的图片
    [SerializeField] private float fadeDuration = 0.5f; // 淡入淡出持续时间

    private Color transparent = new Color(1, 1, 1, 0); // 完全透明
    private Color opaque = new Color(1, 1, 1, 1); // 完全不透明

    protected override void Awake()
    {
        base.Awake();

        // 初始化转场画布状态
        if (fadeCanvas != null)
        {
            fadeCanvas.enabled = true;
            fadeCanvas.worldCamera = Camera.main; // 确保UI可见
        }

        // 确保转场图片初始状态是透明的
        if (transitionImage != null)
        {
            transitionImage.color = transparent;
            transitionImage.raycastTarget = false; // 初始不阻挡点击
        }
    }

    /// <summary>
    /// 渐隐显示转场图片，然后激活目标画布
    /// </summary>
    public void FadeAndActivateCanvas(Canvas targetCanvas, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, targetCanvas, null));
    }

    /// <summary>
    /// 渐隐显示转场图片，然后加载指定地图
    /// </summary>
    public void FadeAndLoadMap(string mapSceneName)
    {
        StartCoroutine(FadeAndLoadSceneRoutine(mapSceneName));
    }

    /// <summary>
    /// 画布切换的渐隐渐出协程
    /// </summary>
    private IEnumerator FadeTransitionRoutine(Canvas currentCanvas, Canvas targetCanvas, string sceneToLoad)
    {
        // 开始渐隐 - 显示转场图片
        transitionImage.raycastTarget = true; // 阻挡点击
        yield return StartCoroutine(FadeRoutine(transparent, opaque));

        // 处理画布切换
        if (currentCanvas != null)
            ////// currentCanvas.enabled = false;
            currentCanvas.gameObject.SetActive(false);

        if (targetCanvas != null)
        {
            Debug.Log(targetCanvas);
            targetCanvas.gameObject.SetActive(true);
            ////// targetCanvas.enabled = true;
            Debug.Log(targetCanvas.enabled);
        }
           
        // 开始渐出 - 隐藏转场图片
        yield return StartCoroutine(FadeRoutine(opaque, transparent));
        transitionImage.raycastTarget = false; // 允许点击
    }

    /// <summary>
    /// 场景加载的渐隐渐出协程
    /// </summary>
    private IEnumerator FadeAndLoadSceneRoutine(string mapSceneName)
    {
        // 开始渐隐 - 显示转场图片
        transitionImage.raycastTarget = true;
        yield return StartCoroutine(FadeRoutine(transparent, opaque));

        // 卸载菜单场景
        yield return SceneManager.UnloadSceneAsync("MenuScene");

        // 加载地图场景
        yield return SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Additive);

        // 设置新场景为活动场景
        Scene newScene = SceneManager.GetSceneByName(mapSceneName);
        if (newScene.isLoaded)
            SceneManager.SetActiveScene(newScene);

        // 开始渐出 - 隐藏转场图片
        yield return StartCoroutine(FadeRoutine(opaque, transparent));
        transitionImage.raycastTarget = false;
    }

    /// <summary>
    /// 渐隐渐出的具体实现
    /// </summary>
    private IEnumerator FadeRoutine(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;
        transitionImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            transitionImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        transitionImage.color = endColor; // 确保最终状态正确
    }
}

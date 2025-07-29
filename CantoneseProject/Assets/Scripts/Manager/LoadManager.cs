using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadManager : Singleton<LoadManager>
{
    [Header("渐隐渐出设置")]
    [SerializeField] private Image fadeImage; // 用于渐隐渐出的全屏Image
    [SerializeField] private float fadeDuration = 0.5f; // 淡入淡出持续时间

    private Color opaqueColor = new Color(0, 0, 0, 1); // 完全不透明（黑色）
    private Color transparentColor = new Color(0, 0, 0, 0); // 完全透明

    protected override void Awake()
    {
        base.Awake();

        // 确保渐隐图片初始状态是透明的
        if (fadeImage != null)
        {
            fadeImage.color = transparentColor;
            fadeImage.raycastTarget = false; // 初始不阻挡点击
        }
    }

    /// <summary>
    /// 屏幕渐隐后激活指定画布
    /// </summary>
    public void FadeAndActivateCanvas(Canvas targetCanvas, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, targetCanvas, null, null));
    }

    /// <summary>
    /// 屏幕渐隐后加载指定场景
    /// </summary>
    public void FadeAndLoadScene(string sceneName, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, null, sceneName, "MenuScene"));
    }

    /// <summary>
    /// 渐隐渐出转场协程
    /// </summary>
    private IEnumerator FadeTransitionRoutine(Canvas currentCanvas, Canvas targetCanvas,
                                              string sceneToLoad, string sceneToUnload)
    {
        // 开始渐隐
        fadeImage.raycastTarget = true; // 防止在转场时点击其他UI
        yield return StartCoroutine(FadeRoutine(transparentColor, opaqueColor));

        // 处理画布切换
        if (currentCanvas != null)
            currentCanvas.gameObject.SetActive(false);

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(true);

        // 处理场景加载/卸载
        if (!string.IsNullOrEmpty(sceneToUnload))
            yield return SceneManager.UnloadSceneAsync(sceneToUnload);

        if (!string.IsNullOrEmpty(sceneToLoad))
            yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // 处理场景加载后的激活
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Scene newScene = SceneManager.GetSceneByName(sceneToLoad);
            if (newScene.isLoaded)
                SceneManager.SetActiveScene(newScene);
        }

        // 开始渐显
        yield return StartCoroutine(FadeRoutine(opaqueColor, transparentColor));
        fadeImage.raycastTarget = false;
    }

    /// <summary>
    /// 渐隐或渐显的具体实现
    /// </summary>
    private IEnumerator FadeRoutine(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        fadeImage.color = endColor; // 确保最终状态正确
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadManager : Singleton<LoadManager>
{
    [Header("������������")]
    [SerializeField] private Image fadeImage; // ���ڽ���������ȫ��Image
    [SerializeField] private float fadeDuration = 0.5f; // ���뵭������ʱ��

    private Color opaqueColor = new Color(0, 0, 0, 1); // ��ȫ��͸������ɫ��
    private Color transparentColor = new Color(0, 0, 0, 0); // ��ȫ͸��

    protected override void Awake()
    {
        base.Awake();

        // ȷ������ͼƬ��ʼ״̬��͸����
        if (fadeImage != null)
        {
            fadeImage.color = transparentColor;
            fadeImage.raycastTarget = false; // ��ʼ���赲���
        }
    }

    /// <summary>
    /// ��Ļ�����󼤻�ָ������
    /// </summary>
    public void FadeAndActivateCanvas(Canvas targetCanvas, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, targetCanvas, null, null));
    }

    /// <summary>
    /// ��Ļ���������ָ������
    /// </summary>
    public void FadeAndLoadScene(string sceneName, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, null, sceneName, "MenuScene"));
    }

    /// <summary>
    /// ��������ת��Э��
    /// </summary>
    private IEnumerator FadeTransitionRoutine(Canvas currentCanvas, Canvas targetCanvas,
                                              string sceneToLoad, string sceneToUnload)
    {
        // ��ʼ����
        fadeImage.raycastTarget = true; // ��ֹ��ת��ʱ�������UI
        yield return StartCoroutine(FadeRoutine(transparentColor, opaqueColor));

        // �������л�
        if (currentCanvas != null)
            currentCanvas.gameObject.SetActive(false);

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(true);

        // ����������/ж��
        if (!string.IsNullOrEmpty(sceneToUnload))
            yield return SceneManager.UnloadSceneAsync(sceneToUnload);

        if (!string.IsNullOrEmpty(sceneToLoad))
            yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // ���������غ�ļ���
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Scene newScene = SceneManager.GetSceneByName(sceneToLoad);
            if (newScene.isLoaded)
                SceneManager.SetActiveScene(newScene);
        }

        // ��ʼ����
        yield return StartCoroutine(FadeRoutine(opaqueColor, transparentColor));
        fadeImage.raycastTarget = false;
    }

    /// <summary>
    /// �������Եľ���ʵ��
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

        fadeImage.color = endColor; // ȷ������״̬��ȷ
    }
}

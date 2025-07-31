using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadManager : Singleton<LoadManager>
{
    [Header("ת������")]
    [SerializeField] private Canvas fadeCanvas; // ת���õĻ���
    [SerializeField] private Image transitionImage; // ת���õ�ͼƬ
    [SerializeField] private float fadeDuration = 0.5f; // ���뵭������ʱ��

    private Color transparent = new Color(1, 1, 1, 0); // ��ȫ͸��
    private Color opaque = new Color(1, 1, 1, 1); // ��ȫ��͸��

    protected override void Awake()
    {
        base.Awake();

        // ��ʼ��ת������״̬
        if (fadeCanvas != null)
        {
            fadeCanvas.enabled = true;
            fadeCanvas.worldCamera = Camera.main; // ȷ��UI�ɼ�
        }

        // ȷ��ת��ͼƬ��ʼ״̬��͸����
        if (transitionImage != null)
        {
            transitionImage.color = transparent;
            transitionImage.raycastTarget = false; // ��ʼ���赲���
        }
    }

    /// <summary>
    /// ������ʾת��ͼƬ��Ȼ�󼤻�Ŀ�껭��
    /// </summary>
    public void FadeAndActivateCanvas(Canvas targetCanvas, Canvas currentCanvas)
    {
        StartCoroutine(FadeTransitionRoutine(currentCanvas, targetCanvas, null));
    }

    /// <summary>
    /// ������ʾת��ͼƬ��Ȼ�����ָ����ͼ
    /// </summary>
    public void FadeAndLoadMap(string mapSceneName)
    {
        StartCoroutine(FadeAndLoadSceneRoutine(mapSceneName));
    }

    /// <summary>
    /// �����л��Ľ�������Э��
    /// </summary>
    private IEnumerator FadeTransitionRoutine(Canvas currentCanvas, Canvas targetCanvas, string sceneToLoad)
    {
        // ��ʼ���� - ��ʾת��ͼƬ
        transitionImage.raycastTarget = true; // �赲���
        yield return StartCoroutine(FadeRoutine(transparent, opaque));

        // �������л�
        if (currentCanvas != null)
            ////// currentCanvas.enabled = false;
            currentCanvas.gameObject.SetActive(false);

        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);
            ////// targetCanvas.enabled = true;
        }
           
        // ��ʼ���� - ����ת��ͼƬ
        yield return StartCoroutine(FadeRoutine(opaque, transparent));
        transitionImage.raycastTarget = false; // ������
    }

    /// <summary>
    /// �������صĽ�������Э��
    /// </summary>
    private IEnumerator FadeAndLoadSceneRoutine(string mapSceneName)
    {
        // ��ʼ���� - ��ʾת��ͼƬ
        transitionImage.raycastTarget = true;
        yield return StartCoroutine(FadeRoutine(transparent, opaque));


        // ��ж��MenuScene����ɾ���������ݣ��������¼��صĿ����ԣ�
        if (SceneManager.GetSceneByName("MenuScene").isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync("MenuScene");
        }
        /////////////////////// ж�ز˵�����
        //yield return SceneManager.UnloadSceneAsync("MenuScene");


        // ���ص�ͼ����
        yield return SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Additive);

        // �����³���Ϊ�����
        Scene newScene = SceneManager.GetSceneByName(mapSceneName);
        if (newScene.isLoaded)
            SceneManager.SetActiveScene(newScene);

        // ��ʼ���� - ����ת��ͼƬ
        yield return StartCoroutine(FadeRoutine(opaque, transparent));
        transitionImage.raycastTarget = false;
    }





    /// <summary>
    /// �ӵ�ͼ���ؼҽ���ķ���
    /// </summary>
    public void ReturnToMainMenu(string currentMapSceneName)
    {
        StartCoroutine(ReturnToMainMenuRoutine(currentMapSceneName));
    }

    /// <summary>
    /// ���ؼҽ����Э��
    /// </summary>
    private IEnumerator ReturnToMainMenuRoutine(string currentMapSceneName)
    {
        // ����Ч��
        transitionImage.raycastTarget = true;
        yield return StartCoroutine(FadeRoutine(transparent, opaque));

        // ж�ص�ǰ��ͼ����
        if (SceneManager.GetSceneByName(currentMapSceneName).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(currentMapSceneName);
        }

        // ���¼���MenuScene
        yield return SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        Scene menuScene = SceneManager.GetSceneByName("MenuScene");
        if (menuScene.isLoaded)
        {
            SceneManager.SetActiveScene(menuScene);
            // ����StartCanvas�����������滭����ΪStartCanvas��
            Canvas startCanvas = GameObject.FindObjectOfType<Menu>().GetComponent<Canvas>();
            if (startCanvas != null)
            {
                startCanvas.gameObject.SetActive(true);
            }
        }

        // ����Ч��
        yield return StartCoroutine(FadeRoutine(opaque, transparent));
        transitionImage.raycastTarget = false;
    }





    /// <summary>
    /// ���������ľ���ʵ��
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

        transitionImage.color = endColor; // ȷ������״̬��ȷ
    }

}

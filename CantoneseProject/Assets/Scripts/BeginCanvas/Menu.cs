using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// canvas: start��info 
/// </summary>
public class Menu : MonoBehaviour
{

    [Header("��������")]
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas infoCanvas;
    [SerializeField] private Canvas homeCanvas;
    // [SerializeField] private Canvas characterCanvas;
    [SerializeField] private Canvas selectCanvas;
    // [SerializeField] private Canvas remindCanvas;


    [Header("��ť����")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        GameObject startCanvasObj = this.gameObject;

        // ��ȡ����
        startCanvas = startCanvasObj.GetComponent<Canvas>();
        infoCanvas = GameObject.Find("Info Canvas").GetComponent<Canvas>();
        homeCanvas = GameObject.Find("Home Canvas").GetComponent<Canvas>();
        // characterCanvas = GameObject.Find("Character Canvas").GetComponent<Canvas>();
        selectCanvas = GameObject.Find("Select Canvas").GetComponent<Canvas>();

        // ��ȡ��ť
        newGameButton = startCanvasObj.transform.Find("NewGameBtn").GetComponent<Button>();
        exitButton = startCanvasObj.transform.Find("ExitBtn").GetComponent<Button>();

        // ע�ᰴť�¼�
        newGameButton.onClick.AddListener(OnNewGameClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    private void Start()   // ��ȡ����������canvas���÷Ǽ���״̬
    {
        infoCanvas.gameObject.SetActive(false);
        homeCanvas.gameObject.SetActive(false);
        // characterCanvas.gameObject.SetActive(false);
        selectCanvas.gameObject.SetActive(false);

    }

    /// <summary>
    /// ����Ϸ��ť����¼�
    /// </summary>
    private void OnNewGameClicked()
    {
        // ���������󼤻���±������ܻ���
        LoadManager.Instance.FadeAndActivateCanvas(infoCanvas, startCanvas);
    }

    /// <summary>
    /// �˳���ť����¼�
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

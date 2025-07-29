using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{

    [Header("��������")]
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas characterCanvas;

    [Header("��ť����")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        // ע�ᰴť�¼�
        newGameButton.onClick.AddListener(OnNewGameClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    /// <summary>
    /// ����Ϸ��ť����¼�
    /// </summary>
    private void OnNewGameClicked()
    {
        // ���������󼤻��ɫѡ�񻭲�
        LoadManager.Instance.FadeAndActivateCanvas(characterCanvas, startCanvas);
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

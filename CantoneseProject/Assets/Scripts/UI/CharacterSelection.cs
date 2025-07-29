using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private Canvas characterCanvas;
    [SerializeField] private Canvas mapCanvas;

    [Header("��ɫ��ť")]
    [SerializeField] private Button[] characterButtons; // 4����ɫ��ť

    private void Awake()
    {
        // Ϊÿ����ɫ��ťע�����¼�
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i; // ����ǰ����
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    /// <summary>
    /// ��ɫѡ���¼�
    /// </summary>
    private void OnCharacterSelected(int characterIndex)
    {
        // ����ѡ�еĽ�ɫ����
        // PlayerData.Instance.SelectedCharacterIndex = characterIndex;

        // ���������󼤻��ͼѡ�񻭲�
        LoadManager.Instance.FadeAndActivateCanvas(mapCanvas, characterCanvas);
    }
}

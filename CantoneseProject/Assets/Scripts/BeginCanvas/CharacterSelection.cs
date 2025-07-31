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
        // ��ʼ����ť����
        characterButtons = new Button[4];

        // �������н�ɫ��ť
        FindCharacterBtns();

        //// Ϊÿ����ɫ��ťע�����¼�
        //for (int i = 0; i < characterButtons.Length; i++)
        //{
        //    int index = i; // ����ǰ����
        //    characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        //}

        // Ϊÿ����ɫ��ťע�����¼�
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // ��鰴ť�Ƿ��ҵ�
            if (characterButtons[i] == null)
            {
                Debug.LogError($"δ�ҵ�����Ϊ {i} �Ľ�ɫ��ť�����鰴ť�����Ƿ���ȷ");
                continue;
            }

            int index = i; // ����ǰ����
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    /// <summary>
    /// ����Canvas�µ��ĸ���ɫ��ť
    /// ���谴ť����Ϊ "C1" �� "C4"
    /// </summary>
    private void FindCharacterBtns()
    {
        // ȷ����ɫ��������
        if (characterCanvas == null)
        {
            Debug.LogError("��ɫ����δ��ֵ");
            return;
        }

        // ����ÿ����ɫ��ť�����谴ťֱ����ΪcharacterCanvas���Ӷ���
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // ��ť���Ƹ�ʽΪ C1 C2
            Transform buttonTransform = characterCanvas.transform.Find($"C{i+1}");

            if (buttonTransform != null)
            {
                characterButtons[i] = buttonTransform.GetComponent<Button>();

                // ����Ƿ���Button���
                if (characterButtons[i] == null)
                {
                    Debug.LogError($"���� {buttonTransform.name} ��û��Button���");
                }
            }
            else
            {
                Debug.LogWarning($"δ�ҵ���Ϊ C{i+1} �Ķ���");
            }
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

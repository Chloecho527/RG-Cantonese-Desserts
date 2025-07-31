using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [Header("画布引用")]
    [SerializeField] private Canvas characterCanvas;
    [SerializeField] private Canvas mapCanvas;

    [Header("角色按钮")]
    [SerializeField] private Button[] characterButtons; // 4个角色按钮

    private void Awake()
    {
        // 初始化按钮数组
        characterButtons = new Button[4];

        // 查找所有角色按钮
        FindCharacterBtns();

        //// 为每个角色按钮注册点击事件
        //for (int i = 0; i < characterButtons.Length; i++)
        //{
        //    int index = i; // 捕获当前索引
        //    characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        //}

        // 为每个角色按钮注册点击事件
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // 检查按钮是否找到
            if (characterButtons[i] == null)
            {
                Debug.LogError($"未找到索引为 {i} 的角色按钮，请检查按钮名称是否正确");
                continue;
            }

            int index = i; // 捕获当前索引
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    /// <summary>
    /// 查找Canvas下的四个角色按钮
    /// 假设按钮名称为 "C1" 到 "C4"
    /// </summary>
    private void FindCharacterBtns()
    {
        // 确保角色画布存在
        if (characterCanvas == null)
        {
            Debug.LogError("角色画布未赋值");
            return;
        }

        // 查找每个角色按钮（假设按钮直接作为characterCanvas的子对象）
        for (int i = 0; i < characterButtons.Length; i++)
        {
            // 按钮名称格式为 C1 C2
            Transform buttonTransform = characterCanvas.transform.Find($"C{i+1}");

            if (buttonTransform != null)
            {
                characterButtons[i] = buttonTransform.GetComponent<Button>();

                // 检查是否有Button组件
                if (characterButtons[i] == null)
                {
                    Debug.LogError($"对象 {buttonTransform.name} 上没有Button组件");
                }
            }
            else
            {
                Debug.LogWarning($"未找到名为 C{i+1} 的对象");
            }
        }
    }




    /// <summary>
    /// 角色选择事件
    /// </summary>
    private void OnCharacterSelected(int characterIndex)
    {
        // 保存选中的角色索引
        // PlayerData.Instance.SelectedCharacterIndex = characterIndex;

        // 渐隐渐出后激活地图选择画布
        LoadManager.Instance.FadeAndActivateCanvas(mapCanvas, characterCanvas);
    }
}

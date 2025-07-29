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
        // 为每个角色按钮注册点击事件
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i; // 捕获当前索引
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
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

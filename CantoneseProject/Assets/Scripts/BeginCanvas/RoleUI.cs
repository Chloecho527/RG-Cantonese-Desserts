using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("组件")]
    public Image backImage;   // 点击选择人物的背景图片（鼠标滑入，背景高亮）
    public Image avatar;   // 角色选择的UI头像
    public Button button;   // 角色选择的button

    public RoleData roleData;

    private void Awake()
    {
        backImage = GetComponent<Image>();   // 选择按钮背景
        avatar = transform.GetChild(0).GetComponent<Image>();   // 选择按钮角色图像
        button = GetComponent<Button>();
    }

    /// <summary>
    /// 传入角色数据
    /// </summary>
    /// <param name="rd"></param>
    public void SetData(RoleData rd)
    {
        this.roleData = rd;

        Sprite loadedSprite = Resources.Load<Sprite>(rd.avatar);
        if (loadedSprite == null)
        {
            Debug.LogError("头像加载失败！路径：" + rd.avatar);
        }

        // 设置选择角色按钮的角色头像
        avatar.sprite = Resources.Load<Sprite>(roleData.avatar);

        // Lambda表达式
        button.onClick.AddListener(() =>
        {
            OnButtonClicked(roleData);
        });
    }

    /// <summary>
    /// 选择角色
    /// </summary>
    /// <param name="r"></param>
    private void OnButtonClicked(RoleData r)
    {
        // 记录已选择的角色信息
        GameManager.Instance.currentData = r;

        // 关闭角色选择UI面板
        RoleSelectPanel.Instance.canvasGroup.alpha = 0;
        RoleSelectPanel.Instance.canvasGroup.blocksRaycasts = false;
        RoleSelectPanel.Instance.canvasGroup.interactable = false;

        // 克隆角色选择UI面板
        Instantiate(RoleSelectPanel.Instance.roleDetails, WeaponSelectPanel.Instance.weaponDetails);

        // 打开武器选择UI面板
        WeaponSelectPanel.Instance.canvasGroup.alpha = 1;
        WeaponSelectPanel.Instance.canvasGroup.blocksRaycasts = true;
        WeaponSelectPanel.Instance.canvasGroup.interactable = true;
    }


    /// <summary>
    /// 鼠标移入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 角色头像背景高亮////////////////////////////////////////////////////////后期根据画面颜色更改
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);

        // 更新角色面板信息
        RenewUI(roleData);
    }
    
    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // 角色头像背景恢复原色/////////////////////////////////////////////////////后期根据画面颜色更改
        backImage.color = new Color(200 / 255f, 80 / 255f, 30 / 255f);

    }

    /// <summary>
    /// 更新角色信息面板
    /// </summary>
    public void RenewUI(RoleData r)
    {
        RoleSelectPanel.Instance.roleName.text = r.name;
        RoleSelectPanel.Instance.roleFaction.text = r.faction;
        RoleSelectPanel.Instance.roleAvatar.sprite = Resources.Load<Sprite>(r.avatar);
        RoleSelectPanel.Instance.roleDescribe.text = r.describe;
    }
}

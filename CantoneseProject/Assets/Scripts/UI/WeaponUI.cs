using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData weaponData;
    
    [Header("组件")]
    public Image backImage;   // 点击选择武器的背景图片（鼠标滑入，背景高亮）
    public Image icon;        // 武器选择的UI图标
    public Button button;     // 武器选择的button
    
    
    private void Awake()
    {
        backImage = GetComponent<Image>();
        button = GetComponent<Button>();
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// 传入武器数据
    /// </summary>
    /// <param name="wd"></param>
    public void SetData(WeaponData wd)
    {
        this.weaponData = wd;
        
        icon.sprite = Resources.Load<Sprite>(weaponData.iconPath);
        
        //点击事件
        button.onClick.AddListener(() =>
        {
            OnButtonClicked(weaponData);
        });
    }
    
    /// <summary>
    /// 点击选择武器
    /// </summary>
    /// <param name="w"></param>
    private void OnButtonClicked(WeaponData w)
    {
        // 记录当前武器
        GameManager.Instance.currentWeapon = w;
        
        // 克隆UI
        GameObject weapon_clone = Instantiate(WeaponSelectPanel.Instance.weaponDetails, MapSelectPanel.Instance.mapContentTrans);
        weapon_clone.transform.SetSiblingIndex(0);
        GameObject role_clone = Instantiate(RoleSelectPanel.Instance.roleDetails, MapSelectPanel.Instance.mapContentTrans);
        role_clone.transform.SetSiblingIndex(0);
        
        // 关闭武器选择UI面板
        WeaponSelectPanel.Instance.canvasGroup.alpha = 0;
        WeaponSelectPanel.Instance.canvasGroup.blocksRaycasts = false;
        WeaponSelectPanel.Instance.canvasGroup.interactable = false;
        
        // 打开地图选择UI面板
        MapSelectPanel.Instance.canvasGroup.alpha = 1;
        MapSelectPanel.Instance.canvasGroup.blocksRaycasts = true;
        MapSelectPanel.Instance.canvasGroup.interactable = true;
    }
    
    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 武器图标背景高亮
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);
        
        // 更新武器面板信息
        RenewUI(weaponData);
    }

    // 鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        // 武器图标背景恢复原色
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);
    }

    /// <summary>
    /// 鼠标滑入按钮，更新武器信息面板
    /// </summary>
    /// <param name="w"></param>
    public void RenewUI(WeaponData w)
    {
        // 修改图标、名称、武器类型、武器描述
        WeaponSelectPanel.Instance.weaponIcon.sprite = Resources.Load<Sprite>(w.iconPath);
        WeaponSelectPanel.Instance.weaponName.text = w.name;
        WeaponSelectPanel.Instance.weaponType.text = w.isLong == 0 ? "近战" : "远攻";
        WeaponSelectPanel.Instance.weaponDescribe.text = w.describe;
    }
}

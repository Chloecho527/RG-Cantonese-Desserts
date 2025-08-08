using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RoleData roleData;
    
    [Header("组件")]
    public Image backImage;   // 点击选择人物的背景图片（鼠标滑入，背景高亮）
    public Image avatar;      // 角色选择的UI头像
    public Button button;     // 角色选择的button
    

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

        Sprite loadedSprite = Resources.Load<Sprite>(rd.avatarPath);
        if (loadedSprite == null)
        {
            Debug.LogError("头像加载失败！路径：" + rd.avatarPath);
        }
        
        // 设置选择角色按钮的角色头像
        avatar.sprite = Resources.Load<Sprite>(roleData.avatarPath);
        
        // Lambda表达式 点击事件
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
        GameManager.Instance.currentRole = r;
        
        // 通知武器选择面板，仅显示对应角色的专武
        WeaponSelectPanel.Instance.FilterWeaponsByRole(r.ID); 

        // 关闭角色选择UI面板
        RoleSelectPanel.Instance.canvasGroup.alpha = 0;
        RoleSelectPanel.Instance.canvasGroup.blocksRaycasts = false;
        RoleSelectPanel.Instance.canvasGroup.interactable = false;

        // 克隆角色选择UI面板
        GameObject go = Instantiate(RoleSelectPanel.Instance.roleDetails, WeaponSelectPanel.Instance.weaponContentTrans);
        go.transform.SetSiblingIndex(0);
        
        // 打开武器选择UI面板
        WeaponSelectPanel.Instance.canvasGroup.alpha = 1;
        WeaponSelectPanel.Instance.canvasGroup.blocksRaycasts = true;
        WeaponSelectPanel.Instance.canvasGroup.interactable = true;
    }


    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 角色头像背景高亮
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);

        // 更新角色面板信息
        RenewUI(roleData);
    }
    
    // 鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        // 角色头像背景恢复原色
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);

    }

    /// <summary>
    /// 鼠标滑入按钮，更新角色信息面板
    /// </summary>
    public void RenewUI(RoleData r)
    {
        // 修改头像、名称、流派、台词描述
        RoleSelectPanel.Instance.roleAvatar.sprite = Resources.Load<Sprite>(r.avatarPath);
        RoleSelectPanel.Instance.roleName.text = r.name;
        RoleSelectPanel.Instance.roleFaction.text = r.faction;
        RoleSelectPanel.Instance.roleDescribe.text = r.describe;
    }
}

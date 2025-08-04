using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MapData mapData;
    
    [Header("组件")]
    public Image backImage;    // 点击选择武器的背景图片（鼠标滑入，背景高亮）
    public Image avatar;       // 地图选择的UI预览图
    public Button button;      // 地图选择的button
    
    private void Awake()
    {
        backImage = GetComponent<Image>();
        button = GetComponent<Button>();
        avatar = transform.GetChild(0).GetComponent<Image>();
    }
    
    /// <summary>
    /// 传入地图数据
    /// </summary>
    /// <param name="wd"></param>
    public void SetData(MapData m)
    {
        this.mapData = m;
        
        avatar.sprite = Resources.Load<Sprite>(mapData.mapPath);
        
        // //点击事件
        // button.onClick.AddListener(() =>
        // {
        //     // 记录选择的地图
        //     GameManager.Instance.currentMap = mapData;
        //     
        //     // 跳转到地图场景
        //     // LoadManager.Instance.FadeAndLoadMap();
        // });
        
        // 点击事件：选择地图后加载对应场景
        button.onClick.AddListener(() =>
        {
            // 记录选择的地图
            GameManager.Instance.currentMap = mapData;
            Debug.Log(mapData.mapSceneName);
            // 调用LoadManager加载地图场景，传入场景名称
            LoadManager.Instance.FadeAndLoadMap(mapData.mapSceneName);
        });
    }

    // private void OnMapSelected()
    // {
    //     if (mapData == null) return;
    //     
    //     // 记录选择的地图
    //     GameManager.Instance.currentMap = mapData;
    //     
    //     // 调用LoadManager加载地图场景，同时卸载菜单场景
    //     LoadManager.Instance.FadeAndLoadMap(mapData.name);
    //     
    //     // 关闭地图选择面板（假设地图选择面板有CanvasGroup组件）
    //     CanvasGroup mapCanvasGroup = GetComponentInParent<CanvasGroup>();
    //     if (mapCanvasGroup != null)
    //     {
    //         mapCanvasGroup.alpha = 0;
    //         mapCanvasGroup.blocksRaycasts = false;
    //         mapCanvasGroup.interactable = false;
    //     }
    // }

    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 武器图标背景高亮
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);
        
        // 更新武器面板信息
        RenewUI(mapData);
    }
    
    // 鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        // 武器图标背景恢复原色
        // TODO 后期根据画面颜色更改
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);
    }
    
    /// <summary>
    /// 鼠标滑入按钮，更新地图信息面板
    /// </summary>
    /// <param name="m"></param>
    private void RenewUI(MapData m)
    {
        // 修改预览图、名称、敌情描述
        MapSelectPanel.Instance.mapName.text = m.name;
        MapSelectPanel.Instance.mapAvatar.sprite = Resources.Load<Sprite>(m.mapPath);
        MapSelectPanel.Instance.mapDescribe.text = m.describe;
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MapData mapData;
    
    [Header("���")]
    public Image backImage;    // ���ѡ�������ı���ͼƬ����껬�룬����������
    public Image avatar;       // ��ͼѡ���UIԤ��ͼ
    public Button button;      // ��ͼѡ���button
    
    private void Awake()
    {
        backImage = GetComponent<Image>();
        button = GetComponent<Button>();
        avatar = transform.GetChild(0).GetComponent<Image>();
    }
    
    /// <summary>
    /// �����ͼ����
    /// </summary>
    /// <param name="wd"></param>
    public void SetData(MapData m)
    {
        this.mapData = m;
        
        avatar.sprite = Resources.Load<Sprite>(mapData.mapPath);
        
        // //����¼�
        // button.onClick.AddListener(() =>
        // {
        //     // ��¼ѡ��ĵ�ͼ
        //     GameManager.Instance.currentMap = mapData;
        //     
        //     // ��ת����ͼ����
        //     // LoadManager.Instance.FadeAndLoadMap();
        // });
        
        // ����¼���ѡ���ͼ����ض�Ӧ����
        button.onClick.AddListener(() =>
        {
            // ��¼ѡ��ĵ�ͼ
            GameManager.Instance.currentMap = mapData;
            Debug.Log(mapData.mapSceneName);
            // ����LoadManager���ص�ͼ���������볡������
            LoadManager.Instance.FadeAndLoadMap(mapData.mapSceneName);
        });
    }

    // private void OnMapSelected()
    // {
    //     if (mapData == null) return;
    //     
    //     // ��¼ѡ��ĵ�ͼ
    //     GameManager.Instance.currentMap = mapData;
    //     
    //     // ����LoadManager���ص�ͼ������ͬʱж�ز˵�����
    //     LoadManager.Instance.FadeAndLoadMap(mapData.name);
    //     
    //     // �رյ�ͼѡ����壨�����ͼѡ�������CanvasGroup�����
    //     CanvasGroup mapCanvasGroup = GetComponentInParent<CanvasGroup>();
    //     if (mapCanvasGroup != null)
    //     {
    //         mapCanvasGroup.alpha = 0;
    //         mapCanvasGroup.blocksRaycasts = false;
    //         mapCanvasGroup.interactable = false;
    //     }
    // }

    // �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ����ͼ�걳������
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);
        
        // �������������Ϣ
        RenewUI(mapData);
    }
    
    // ����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // ����ͼ�걳���ָ�ԭɫ
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);
    }
    
    /// <summary>
    /// ��껬�밴ť�����µ�ͼ��Ϣ���
    /// </summary>
    /// <param name="m"></param>
    private void RenewUI(MapData m)
    {
        // �޸�Ԥ��ͼ�����ơ���������
        MapSelectPanel.Instance.mapName.text = m.name;
        MapSelectPanel.Instance.mapAvatar.sprite = Resources.Load<Sprite>(m.mapPath);
        MapSelectPanel.Instance.mapDescribe.text = m.describe;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData weaponData;
    
    [Header("���")]
    public Image backImage;   // ���ѡ�������ı���ͼƬ����껬�룬����������
    public Image icon;        // ����ѡ���UIͼ��
    public Button button;     // ����ѡ���button
    
    
    private void Awake()
    {
        backImage = GetComponent<Image>();
        button = GetComponent<Button>();
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="wd"></param>
    public void SetData(WeaponData wd)
    {
        this.weaponData = wd;
        
        icon.sprite = Resources.Load<Sprite>(weaponData.iconPath);
        
        //����¼�
        button.onClick.AddListener(() =>
        {
            OnButtonClicked(weaponData);
        });
    }
    
    /// <summary>
    /// ���ѡ������
    /// </summary>
    /// <param name="w"></param>
    private void OnButtonClicked(WeaponData w)
    {
        // ��¼��ǰ����
        GameManager.Instance.currentWeapon = w;
        
        // ��¡UI
        GameObject weapon_clone = Instantiate(WeaponSelectPanel.Instance.weaponDetails, MapSelectPanel.Instance.mapContentTrans);
        weapon_clone.transform.SetSiblingIndex(0);
        GameObject role_clone = Instantiate(RoleSelectPanel.Instance.roleDetails, MapSelectPanel.Instance.mapContentTrans);
        role_clone.transform.SetSiblingIndex(0);
        
        // �ر�����ѡ��UI���
        WeaponSelectPanel.Instance.canvasGroup.alpha = 0;
        WeaponSelectPanel.Instance.canvasGroup.blocksRaycasts = false;
        WeaponSelectPanel.Instance.canvasGroup.interactable = false;
        
        // �򿪵�ͼѡ��UI���
        MapSelectPanel.Instance.canvasGroup.alpha = 1;
        MapSelectPanel.Instance.canvasGroup.blocksRaycasts = true;
        MapSelectPanel.Instance.canvasGroup.interactable = true;
    }
    
    // �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ����ͼ�걳������
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);
        
        // �������������Ϣ
        RenewUI(weaponData);
    }

    // ����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // ����ͼ�걳���ָ�ԭɫ
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);
    }

    /// <summary>
    /// ��껬�밴ť������������Ϣ���
    /// </summary>
    /// <param name="w"></param>
    public void RenewUI(WeaponData w)
    {
        // �޸�ͼ�ꡢ���ơ��������͡���������
        WeaponSelectPanel.Instance.weaponIcon.sprite = Resources.Load<Sprite>(w.iconPath);
        WeaponSelectPanel.Instance.weaponName.text = w.name;
        WeaponSelectPanel.Instance.weaponType.text = w.isLong == 0 ? "��ս" : "Զ��";
        WeaponSelectPanel.Instance.weaponDescribe.text = w.describe;
    }
}

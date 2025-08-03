using System;
using System.Collections;
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
        
    }
    
    // �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾�����////////////////////////////////////////////////////////���ڸ��ݻ�����ɫ����
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);
        
        // �������������Ϣ
        RenewUI(weaponData);
    }

    // ����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾��ָ�ԭɫ/////////////////////////////////////////////////////���ڸ��ݻ�����ɫ����
        backImage.color = new Color(250 / 255f, 250 / 255f, 130 / 255f);
    }

    /// <summary>
    /// ����������Ϣ���
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

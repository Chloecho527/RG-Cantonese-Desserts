using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RoleData roleData;
    
    [Header("���")]
    public Image backImage;   // ���ѡ������ı���ͼƬ����껬�룬����������
    public Image avatar;      // ��ɫѡ���UIͷ��
    public Button button;     // ��ɫѡ���button
    

    private void Awake()
    {
        backImage = GetComponent<Image>();   // ѡ��ť����
        avatar = transform.GetChild(0).GetComponent<Image>();   // ѡ��ť��ɫͼ��
        button = GetComponent<Button>();
    }

    /// <summary>
    /// �����ɫ����
    /// </summary>
    /// <param name="rd"></param>
    public void SetData(RoleData rd)
    {
        this.roleData = rd;

        Sprite loadedSprite = Resources.Load<Sprite>(rd.avatarPath);
        if (loadedSprite == null)
        {
            Debug.LogError("ͷ�����ʧ�ܣ�·����" + rd.avatarPath);
        }
        
        // ����ѡ���ɫ��ť�Ľ�ɫͷ��
        avatar.sprite = Resources.Load<Sprite>(roleData.avatarPath);
        
        // Lambda���ʽ ����¼�
        button.onClick.AddListener(() =>
        {
            OnButtonClicked(roleData);
        });
    }

    /// <summary>
    /// ѡ���ɫ
    /// </summary>
    /// <param name="r"></param>
    private void OnButtonClicked(RoleData r)
    {
        // ��¼��ѡ��Ľ�ɫ��Ϣ
        GameManager.Instance.currentRole = r;
        
        // ֪ͨ����ѡ����壬����ʾ��Ӧ��ɫ��ר��
        WeaponSelectPanel.Instance.FilterWeaponsByRole(r.ID); 

        // �رս�ɫѡ��UI���
        RoleSelectPanel.Instance.canvasGroup.alpha = 0;
        RoleSelectPanel.Instance.canvasGroup.blocksRaycasts = false;
        RoleSelectPanel.Instance.canvasGroup.interactable = false;

        // ��¡��ɫѡ��UI���
        GameObject go = Instantiate(RoleSelectPanel.Instance.roleDetails, WeaponSelectPanel.Instance.weaponContent);
        go.transform.SetSiblingIndex(0);
        
        // ������ѡ��UI���
        WeaponSelectPanel.Instance.canvasGroup.alpha = 1;
        WeaponSelectPanel.Instance.canvasGroup.blocksRaycasts = true;
        WeaponSelectPanel.Instance.canvasGroup.interactable = true;
    }


    // �������
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾�����
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);

        // ���½�ɫ�����Ϣ
        RenewUI(roleData);
    }
    
    // ����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾��ָ�ԭɫ
        // TODO ���ڸ��ݻ�����ɫ����
        backImage.color = new Color(250 / 255f, 130 / 255f, 130 / 255f);

    }

    /// <summary>
    /// ���½�ɫ��Ϣ���
    /// </summary>
    public void RenewUI(RoleData r)
    {
        // �޸�ͷ�����ơ����ɡ�̨������
        RoleSelectPanel.Instance.roleAvatar.sprite = Resources.Load<Sprite>(r.avatarPath);
        RoleSelectPanel.Instance.roleName.text = r.name;
        RoleSelectPanel.Instance.roleFaction.text = r.faction;
        RoleSelectPanel.Instance.roleDescribe.text = r.describe;
    }
}

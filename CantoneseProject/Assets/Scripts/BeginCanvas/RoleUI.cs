using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backImage;   // ���ѡ������ı���ͼƬ����껬�룬����������
    public Image avatar;   // ��ɫѡ���UIͷ��
    public Button button;   // ��ɫѡ���button

    public RoleData roleData;

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

        Sprite loadedSprite = Resources.Load<Sprite>(rd.avatar);
        if (loadedSprite == null)
        {
            Debug.LogError("ͷ�����ʧ�ܣ�·����" + rd.avatar);
        }

        // ����ѡ���ɫ��ť�Ľ�ɫͷ��
        avatar.sprite = Resources.Load<Sprite>(roleData.avatar);
        
    }


    /// <summary>
    /// �������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾�����////////////////////////////////////////////////////////���ڸ��ݻ�����ɫ����
        backImage.color = new Color(100 / 255f, 70 / 255f, 60 / 255f);

        // ���½�ɫ�����Ϣ
        RenewUI(roleData);
    }
    
    /// <summary>
    /// ����Ƴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ɫͷ�񱳾��ָ�ԭɫ/////////////////////////////////////////////////////���ڸ��ݻ�����ɫ����
        backImage.color = new Color(200 / 255f, 80 / 255f, 30 / 255f);

    }

    /// <summary>
    /// ���½�ɫ��Ϣ���
    /// </summary>
    public void RenewUI(RoleData r)
    {
        RoleSelectPanel.Instance.roleName.text = r.name;
        RoleSelectPanel.Instance.roleFaction.text = r.faction;
        RoleSelectPanel.Instance.roleAvatar.sprite = Resources.Load<Sprite>(r.avatar);
        RoleSelectPanel.Instance.roleDescribe.text = r.describe;
    }
}

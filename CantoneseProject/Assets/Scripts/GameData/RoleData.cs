using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // ����JSON���л�/�����л�
public class RoleData
{
    public int ID;               // ��ɫID
    
    [Header("��ɫѡ���������")]
    public string name;          // ��ɫ����
    public string faction;       // ����
    public string avatarPath;    // ��ɫͷ��
    public string describe;      // ��ɫ̨�ʺ�����
    public int slot;             // �������
    
    [Header("���ؿ�����")]
    public string animatorController;  // ��ͬ��ɫ�Ķ�����
        
}

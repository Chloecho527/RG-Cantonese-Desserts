using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // ����JSON���л�/�����л�
public class RoleData
{
    public int ID;
    public string name;   // ��ɫ����
    public string faction;   // ����
    public string avatar;   // ��ɫͷ��
    public string describe;   // ��ɫ̨�ʺ�����
    public int slot;   // �������
}

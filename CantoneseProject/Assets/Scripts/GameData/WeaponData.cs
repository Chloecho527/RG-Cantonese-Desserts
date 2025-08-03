using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    public int ID;                              // ����ΨһID
    public int roleID;                          // �����Ľ�ɫID������ɸѡ��
    public string name;                         // ��������
    public int isLong;                          // �Ƿ�ΪԶ�̹���
    public string iconPath;                     // ����ͼ��·����Resources�£�
    public string describe;                     // ������������"����10%������"��
    public float attack;                        // ����������
    public int range;                           // ����������Χ
    public float cooling;                       // ��ȴʱ��
    public int repel;                           // ����Ч��
    public float criticalStrikeProbability;     // ��������
    public float criticalStrikeMultiple;        // ��������
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // ����JSON���л�/�����л�
public class MapData
{
    public int ID;                // ��ͼID
    public string name;           // UI��ʾ������
    public string mapSceneName;   // ��ͼ��������     
    public string mapPath;        // ��ͼԤ��ͼ·��
    public string describe;       // ��������
}

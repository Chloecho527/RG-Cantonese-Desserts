using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectPanel : Singleton<WeaponSelectPanel>
{
    public CanvasGroup canvasGroup;
    public Transform weaponDetails;

    protected override void Awake()
    {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        Debug.Log(canvasGroup);
        weaponDetails = GameObject.Find("WeaponDetails").transform;
    }
}

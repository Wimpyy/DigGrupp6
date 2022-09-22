using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GunClass : MonoBehaviour
{
    public AmmoTypeClass[] ammoTypes;
    int currentAmmo;

    public GameObject ammoSlotHolder;
    public GameObject ammoSlotCursor;

    [HideInInspector]
    public AmmoTypeClass activeAmmo;
    int activeSlotIndex;

    GameObject[] ammoSlots;

    private void Start()
    {
        ammoSlots = new GameObject[ammoSlotHolder.transform.childCount];
        for (int i = 0; i < ammoSlots.Length; i++)
        {
            ammoSlots[i] = ammoSlotHolder.transform.GetChild(i).gameObject;
            ammoSlots[i].GetComponentInChildren<Image>().sprite = ammoTypes[i].ammoSprite;
        }

        RefreshUI();
    }

    private void Update()
    {
        HotbarInputs();
        activeAmmo = ammoTypes[activeSlotIndex];
        ammoSlotCursor.transform.position = ammoSlots[activeSlotIndex].transform.position;

        if (currentAmmo != ammoTypes[activeSlotIndex].ammoAmmount)
        {
            RefreshUI();
            currentAmmo = ammoTypes[activeSlotIndex].ammoAmmount;
        }
    }

    void HotbarInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeSlotIndex = 1;
            activeSlotIndex--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeSlotIndex = 2;
            activeSlotIndex--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeSlotIndex = 3;
            activeSlotIndex--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeSlotIndex = 4;
            activeSlotIndex--;
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < ammoSlots.Length; i++)
        {
            ammoSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = ammoTypes[i].ammoAmmount.ToString();
        }
    }
}

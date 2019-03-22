using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public List<GameObject> equipment;
    public GameObject EquippedItem;

    void Start()
    {
        this.equipment = new List<GameObject>();
    }

    void Update()
    {
    }


    public GameObject GetEquippedItem()
    {
        return this.EquippedItem;
    }
}

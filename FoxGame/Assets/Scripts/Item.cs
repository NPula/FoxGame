using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected enum ItemType
    {
        Food,
        Scraps,
        Equipment
    };

    [SerializeField] private ItemType m_itemType = ItemType.Food;
}

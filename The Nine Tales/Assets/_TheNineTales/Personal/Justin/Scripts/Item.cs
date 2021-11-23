using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject
{
    private static int objectCount; 

    [Tooltip("The name of the item. This is shown to the player. Ideally should be unique from all other items, but does not need to be.")]
    new public string name;
    [Tooltip("The number of items in this stack.")]
    public int count;
    public Sprite icon;
    [Tooltip("The item id is used to check if an inventory contains the item. ids should be unique from all other items to ensure there is no confusion.")]
    public int id;

    void OnEnable()
    {
        name = "Item";
        id = objectCount;
        objectCount++;
    }

    public bool Equals(Item i)
    {
        return id == i.id;
    }
    public bool Equals(ItemStack i)
    {
        return id == i.id;
    }
}
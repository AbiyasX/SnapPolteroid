using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Item/Items")]
public class Items : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public GameObject itemGameObjcet;
    [TextArea(10,5)]
    [SerializeField] public string itemDescription;
    [SerializeField] itemType itemType;
}

public enum itemType
{
    playerItem,
    MissionItem
}

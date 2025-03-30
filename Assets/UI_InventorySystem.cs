using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class UI_InventorySystem : MonoBehaviour
{
    [SerializeField] Image inventorySprite;
    [SerializeField] Sprite CloseBag_sprite;
    [SerializeField] Sprite OpenBag_sprite;
    [SerializeField] GameObject ItemBar;

    [SerializeField] Items[] playerItem;
    [SerializeField] Image[] itemSlots;
    [SerializeField] bool isOpen;

    public bool isInventoryFull = false;

    public static UI_InventorySystem instance;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        UpdateInventoryUI();
    }

    public void additem(Items addItem)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (playerItem[i] == null) 
            {
                playerItem[i] = addItem;
                UpdateInventoryUI(); 
                Debug.Log(addItem.itemName + " added to inventory!");
                return;
            }
        }
        Debug.Log("Inventory is full!");
        isInventoryFull = true;
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < playerItem.Length && playerItem[i] != null)
            {
                itemSlots[i].sprite = playerItem[i].itemIcon;
                itemSlots[i].color = Color.white;
            }
            else
            {
                itemSlots[i].sprite = null;
                itemSlots[i].color = new Color(1, 1, 1, 0); 
            }
        }
    }

    public void Toggle_Inventory()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            inventorySprite.sprite = OpenBag_sprite;
            ItemBar.SetActive(true);
        }
        else
        {
            inventorySprite.sprite = CloseBag_sprite;
            ItemBar.SetActive(false);
        }
    }
        
}

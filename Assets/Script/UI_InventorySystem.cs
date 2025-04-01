using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySystem : MonoBehaviour
{
    [SerializeField] Image inventorySprite;
    [SerializeField] Sprite CloseBag_sprite;
    [SerializeField] Sprite OpenBag_sprite;
    [SerializeField] GameObject ItemBar;
    [SerializeField] Transform ObtainItemHolder;
    [SerializeField] ParticleSystem ObtainVFX;

    [SerializeField] Items[] playerItem;
    [SerializeField] Image[] itemSlots;

    [Header("Status")]
    [SerializeField] bool isOpen;
    [SerializeField] bool isPlaying;
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
                ObtainedItemFX(addItem.itemGameObjcet);
                return;
            }
        }
        Debug.Log("Inventory is full!");
        isInventoryFull = true;
    }
    
    public void ObtainedItemFX(GameObject itemModel)
    {
        GameObject item = Instantiate(itemModel, ObtainItemHolder);
        item.transform.SetParent(ObtainItemHolder);
    }

    public void VFX()
    {
        ObtainVFX.Play();
        ObtainItemHolder.DOLocalMoveY(2, 1f);
        ObtainItemHolder.DOScale(Vector3.one * 1, 1f).SetEase(Ease.OutElastic);
        ObtainItemHolder.DORotate(new Vector3(0, 180, 0), .8f, RotateMode.FastBeyond360).SetEase(Ease.OutElastic);
        StartCoroutine(finishVFX());
    }

    IEnumerator finishVFX()
    {
        yield return new WaitForSeconds(5f);
        ObtainVFX.Stop();
        ObtainItemHolder.DOLocalMoveY(0, 1f);
        ObtainItemHolder.DOScale(Vector3.one * 0, 0.5f).SetEase(Ease.InBounce);
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


    public void RemoveItem(Items removeItem)
    {
        for (int i = 0; i < playerItem.Length; i++)
        {
            if (playerItem[i] == removeItem)
            {
                playerItem[i] = null;
                UpdateInventoryUI();
                Debug.Log(removeItem.itemName + " removed from inventory!");

                isInventoryFull = !HasEmptySlot();
                return;
            }
        }
        Debug.Log("Item not found in inventory!");
    }

    private bool HasEmptySlot()
    {
        foreach (var item in playerItem)
        {
            if (item == null) return true;
        }
        return false;
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

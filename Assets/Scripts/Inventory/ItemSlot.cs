using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 64;
    
    // Interactive Item Slot
    [SerializeField] public GameObject SelectedPanel;
    public bool isItemSelected;
    private InventoryManager InventoryManager;

    // Menampilkan Details ke Panel
    public ItemDetails ItemDetails => itemDetails;
    private ItemDetails itemDetails;

    public void Awake() {
        InventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    public void AddItem(ItemDetails item)
    {

        itemDetails = item; 

        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        AddQuantity(item.Quantity);

    }

    public bool IsEmpty => itemQuantity <= 0;


    public bool CanStackWith(string otherItemName){
         return !isFull && itemName == otherItemName;
    
    }

    public void AddQuantity(int amount)
    {
        itemQuantity += amount;
        isFull = itemQuantity >= maxStackSize;
        
        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;
        itemImage.enabled = true;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    private void LeftClick()
    {
        Debug.Log("Left Clicked on ");
        // InventoryManager.DesselectAllSlot();
        // SelectedPanel.SetActive(true);
        // isItemSelected = true;

        if(IsEmpty || InventoryManager == null)
        {
            return;
        }

        InventoryManager.SelectSlot(this);
    }

    private void RightClick()
    {
        
        Debug.Log("Right Clicked on ");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField] private int xSpaceBetweenItems;
    [SerializeField] private int ySpaceBetweenItems;
    [SerializeField] private int xStart;
    [SerializeField] private int yStart;
    [SerializeField] private int collums;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].item.name + " " + inventory.container[i].amount;
            itemsDisplayed.Add(inventory.container[i], obj);

        }


    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                itemsDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].item.name + " " + inventory.container[i].amount;
            }
            else
            {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].item.name + " " + inventory.container[i].amount;
                itemsDisplayed.Add(inventory.container[i], obj);
            }
        }
    }

    public Vector3 GetPosition(int position)
    {
        Vector3 location = Vector2.zero;
        location.x = xStart + (xSpaceBetweenItems * (position % collums));
        location.y = yStart + (-ySpaceBetweenItems * (position / collums));

        return location;
    }
}

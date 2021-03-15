using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backPack : MonoBehaviour
{
    public GameManager gameManager;

    public bool accessingPack;

    public List<Item> items = new List<Item>();

    public int maxItems;

    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;

    [SerializeField]
    float itemsX;
    [SerializeField]
    float itemsY1;
    [SerializeField]
    float itemsY2;
    [SerializeField]
    float itemsY3;
    [SerializeField]
    float itemsY4;

    // Start is called before the first frame update
    void Start()
    {
        accessingPack = false;

        pos1 = new Vector2(itemsX, itemsY1);
        pos2 = new Vector2(itemsX, itemsY2);
        pos3 = new Vector2(itemsX, itemsY3);
        pos4 = new Vector2(itemsX, itemsY4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addToPack(Item item)
    {
        if (item.gameObject.tag == "items" && items.Count < maxItems)
        {
            items[items.Count - 1] = item;
        }
    }

    public void drawItem(Item item)
    {
        if (item.gameObject.tag == "items")
        {
            if (items.Count >= maxItems)
            {
                Debug.Log("pack full"); //to change to text latter
                gameManager.showPopText("My backpack is full");
            } else
            {
                addToPack(item);
                int itemCount = items.Count;

                switch (itemCount) {
                    case 1:
                        GameObject drawing1 = Instantiate(item.gameObject, pos1, transform.rotation);
                        break;

                    case 2:
                        GameObject drawing2 = Instantiate(item.gameObject, pos2, transform.rotation);
                        break;

                    case 3:
                        GameObject drawing3 = Instantiate(item.gameObject, pos3, transform.rotation);
                        break;

                    case 4:
                        GameObject drawing4 = Instantiate(item.gameObject, pos4, transform.rotation);
                        break;
                }
                
            }
        }
    }
}

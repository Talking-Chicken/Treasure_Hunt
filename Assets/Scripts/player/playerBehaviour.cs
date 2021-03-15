using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour
{
    //game manager
    public GameManager gameManager;

    //movement control and collision
    public float speed;

    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    float moveDir = 1;

    //item interaction
    bool handEmpty; //this is used to check whether player picked up the item. HOWEVER now it's useless
    bool pickedUp;
    bool isUsing;

    public float sightDist; //raycasting distance
    Vector3 dir; //direction of player that is facing at

    GameObject detectingItem; //detectingItem is the object that is in range to pick up
    GameObject item; //item is the object that holding in hand

    //back pack
    public List<GameObject> backPack = new List<GameObject>();

    public int maxCapacity; //max number of objects that can be put in back pack
    public GameObject backPackBG;
    GameObject outline;
    public GameObject indicator; //since outline is not working righ now, using a indicator first
    
    [SerializeField]
    [Range(0.1f, 2.0f)]
    float gapBetweenItems;

    bool backPackSearch; //to check whether payer is looking at back pack
    int element; //this indicates which element player is looking at inside the back pack
    int indicatorNum; //to check whether player switch to another object to look at inside the back pack. HOWEVER useless right now
    bool drawingOuline; //to prevent drawing outline multiple times
    [SerializeField]
    [Range(0.1f,5.0f)]
    public float outlineThickness; //useless right now....

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();

        handEmpty = true;
        //pickedUp = false;
        //isUsing = false;

        backPackSearch = false;
        element = 0;
        drawingOuline = false;
    }

    // Update is called once per frame
    void Update()
    {
        detectItem();
        pickUpItem();
        putInPack();
        putDownItem();
        searchBackPack();
        itemPosition();
    }

    private void FixedUpdate()
    {
        checkKeys();
        if (!backPackSearch)
        {
            handleMovement();
        }
    }

    // detect directions
    void checkKeys()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDir = 1;
            dir = new Vector3(0, 1, 0);

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = 2;
                dir = new Vector3(-1, 1, 0);
            } else if (Input.GetKey(KeyCode.D))
            {
                moveDir = 4;
                dir = new Vector3(1, 1, 0);
            }

        } else if (Input.GetKey(KeyCode.S))
        {
            moveDir = 3;
            dir = new Vector3(0, -1, 0);

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = 6;
                dir = new Vector3(-1, -1, 0);
            } else if (Input.GetKey(KeyCode.D))
            {
                moveDir = 8;
                dir = new Vector3(1, -1, 0);
            }

        } else if (Input.GetKey(KeyCode.A))
        {
            moveDir = 5;
            dir = new Vector3(-1, 0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                moveDir = 2;
                dir = new Vector3(-1, 1, 0);
            } else if (Input.GetKey(KeyCode.S))
            {
                moveDir = 6;
                dir = new Vector3(-1, -1, 0);
            }

        } else if (Input.GetKey(KeyCode.D))
        {
            moveDir = 7;
            dir = new Vector3(1, 0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                moveDir = 4;
                dir = new Vector3(1, 1, 0);
            } else if (Input.GetKey(KeyCode.S))
            {
                moveDir = 8;
                dir = new Vector3(1, -1, 0);
            }

        } else
        {
            moveDir = 0;
        }
    }

    //8 directions movement
    void handleMovement()
    {
        switch (moveDir)
        {
            case 1:
                myBody.velocity = new Vector3(0, 1) * speed;
                break;
            
            case 3:
                myBody.velocity = new Vector3(0, -1) * speed;
                break;

            case 5:
                myBody.velocity = new Vector3(-1, 0) * speed;
                break;

            case 7:
                myBody.velocity = new Vector3(1, 0) * speed;
                break;

            case 2:
                myBody.velocity = new Vector3(-0.5f, 0.5f) * speed;
                break;

            case 4:
                myBody.velocity = new Vector3(0.5f, 0.5f) * speed;
                break;

            case 6:
                myBody.velocity = new Vector3(-0.5f, -0.5f) * speed;
                break;

            case 8:
                myBody.velocity = new Vector3(0.5f, -0.5f) * speed;
                break;

            case 0:
                myBody.velocity = new Vector3(0, 0);
                break;
        }
    }

    public void detectItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDist);
        Debug.DrawRay(transform.position, dir, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "item")
            {
                if (detectingItem == null)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    detectingItem = hit.collider.gameObject;
                } else if (hit.collider.gameObject != detectingItem)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    detectingItem = hit.collider.gameObject;
                }
            }
        } else
        {
            detectingItem = null;
        }
    }

    void pickUpItem()
    {
        if (detectingItem != null)
        {
            if (Input.GetKeyUp(KeyCode.Space) && item == null)
            {
                item = detectingItem;
                item.layer = 2;
                detectingItem = null;
            }
            putDownItem();
        }

        if (item != null)
        {
            item.transform.position = gameObject.transform.position;
            handEmpty = false;
        }
    }

    void putInPack()
    {
        if (item != null && Input.GetKeyDown(KeyCode.Q))
        {
            if (backPack.Count >= maxCapacity)
            {
                //Debug.Log("max");
                gameManager.showPopText("My backpack is full");
            } else
            {
                //add item to the backpack
                backPack.Add(item);
                gameManager.showPopText(item.name + " is in backpack");
                //when item is in the backpack, it is not holding in hand
                item = null;
                handEmpty = true;
            }
        }

        //draw icon of items in backpack UI
        for (int i = 0; i < backPack.Count; i++)
        {
            backPack[i].transform.position = new Vector3(backPackBG.transform.position.x, 
                (backPackBG.transform.position.y + backPackBG.transform.localScale.y) -(i * gapBetweenItems), 
                backPackBG.transform.position.z);
            backPack[i].GetComponent<SpriteRenderer>().sortingOrder = 12;
        }
    }

    void putDownItem() //here's problem that player needs to hold SPACE for a while to leave the item
    {
        if (item != null && !handEmpty)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                item.layer = 8;
                item = null;
            }
        }
    }

    void searchBackPack()
    {
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.I)) && backPack.Count >0)
        {
            backPackSearch = true;
            indicator.SetActive(true);
            //Debug.Log(element);
            //Debug.Log(backPackSearch);
            gameManager.showPopText("W and S to select items" + "\n" + "Q to take the item out" + "\n" + "ESC to exit backpack");
        }

        if (backPackSearch)
        {
            if (Input.GetKeyDown(KeyCode.W) && element > 0)
            {
                element--;
            }
            else if (Input.GetKeyDown(KeyCode.S) && element < Mathf.Min(maxCapacity - 1, backPack.Count - 1))
            {
                element++;
            }

            drawOutline(backPack[element], element);
            takeOutFromPack(backPack[element], element);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backPackSearch = false;
                indicator.SetActive(false);
            }
        }
    }

    void drawOutline(GameObject g, int i) //draw outline for items in back pack that player is looking at
    {
        Vector3 pos = new Vector3(g.gameObject.transform.position.x - 1, g.gameObject.transform.position.y);
        if (!drawingOuline) {
            indicator.SetActive(true);
            /*outline = Instantiate(g, pos, g.transform.rotation);
            SpriteRenderer sr = outline.gameObject.GetComponent<SpriteRenderer>();
            outline.transform.localScale *= outlineThickness;
            outline.transform.position = g.transform.position;
            sr.color = Color.black;
            sr.sortingOrder = 20;*/
            indicator.transform.position = pos;
            drawingOuline = true;
        } else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                //outline.transform.position = g.transform.position;
                indicator.transform.position = pos;
                    //indicator.SetActive(false);
                    //drawingOuline = false;
            }
        }
    }
    
    void takeOutFromPack(GameObject g, int i)
    {
        if (backPack.Count >0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && item == null)
            {
                item = g;
                backPack.Remove(item);
                handEmpty = false;
                element = 0;
                backPackSearch = false;
                indicator.SetActive(false);
            } else if (item != null)
            {
                GameObject temp = item;
                item = g;
                backPack[i] = temp;
                element = 0;
                backPackSearch = false;
                indicator.SetActive(false);
            }
        }
    }

    void itemPosition()
    {
        if (item != null)
        {
            item.transform.position = gameObject.transform.position;
            item.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    //interaction between keys and gates
    void keyToGate(Collision2D c)
    {
        if (item != null)
        {
            switch (item.name)
            {
                case "Red Key":
                    if (c.gameObject.name == "Gate Red")
                    {
                        Destroy(c.gameObject);
                        Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Red gate opened");
                    }
                    break;

                case "Blue Key":
                    if (c.gameObject.name == "Gate Blue")
                    {
                        Destroy(c.gameObject);
                        Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Blue gate opened");
                    }
                    break;

                case "Yellow Key":
                    if (c.gameObject.name == "Gate Yellow")
                    {
                        Destroy(c.gameObject);
                        Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Yellow gate opened");
                    }
                    break;

                case "Green Key":
                    if (c.gameObject.name == "Gate Green")
                    {
                        Destroy(c.gameObject);
                        Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Green gate opened");
                    }
                    break;

                case "Pink Key":
                    if (c.gameObject.name == "Gate Pink")
                    {
                        Destroy(c.gameObject);
                        Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Pink gate opened");
                    }
                    break;
            }
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gate")
        {
            keyToGate(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "coin")
        {
            Debug.Log("found");
            gameManager.showPopText("YOU FIND THE COIN!");
        }
    }

    /*
    void handleMovement()
    {
        switch (moveDir)
        {
            case 1:
                transform.Translate(new Vector3(0, 1) * Time.deltaTime * speed);
                break;

            case 3:
                transform.Translate(new Vector3(0, -1) * Time.deltaTime * speed);
                break;

            case 5:
                transform.Translate(new Vector3(-1, 0) * Time.deltaTime * speed);
                break;

            case 7:
                transform.Translate(new Vector3(1, 0) * Time.deltaTime * speed);
                break;

            case 0:

                break;
        }
    }
    */
}

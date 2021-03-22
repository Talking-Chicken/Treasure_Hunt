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
    bool isPutDown = true;
    bool isUsing;

    public float sightDist; //raycasting distance
    Vector3 dir; //direction of player that is facing at

    GameObject detectingItem; //detectingItem is the object that is in range to pick up
    public GameObject item; //item is the object that holding in hand

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
    public float outlineThickness; //the thickness of the outline

    bool outlineDrawed = false;

    //talking with NPCs
    public List<List<string>> dialogues = new List<List<string>>();

    public int talkingWith;

    public bool isClockGetted = false;

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

        // set item stats when NOT in backpack searching
        if (!backPackSearch)
        {
            if (outline != null)
            {
                outline.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        checkKeys();
        if (!backPackSearch && !gameManager.isSittingInChair)
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
                myBody.velocity = new Vector3(-0.75f, 0.75f) * speed;
                break;

            case 4:
                myBody.velocity = new Vector3(0.75f, 0.75f) * speed;
                break;

            case 6:
                myBody.velocity = new Vector3(-0.75f, -0.75f) * speed;
                break;

            case 8:
                myBody.velocity = new Vector3(0.75f, -0.75f) * speed;
                break;

            case 0:
                myBody.velocity = new Vector3(0, 0);
                break;
        }
    }

    public void detectItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDist);
        //Debug.DrawRay(transform.position, dir, Color.red);
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

    public GameObject detectNPC()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDist);
        Debug.DrawRay(transform.position, dir, Color.blue);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "npc")
            {
                return hit.collider.gameObject;
            } else
            {
                return this.gameObject;
            }
        } else
        {
            return this.gameObject;
        }
    }

    public GameObject detectChair()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, sightDist);
        Debug.DrawRay(transform.position, dir, Color.blue);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "chair")
            {
                return hit.collider.gameObject;
            }
            else
            {
                return this.gameObject;
            }
        }
        else
        {
            return this.gameObject;
        }
    }

    void pickUpItem()
    {
        if (detectingItem != null)
        {
            if (Input.GetKeyUp(KeyCode.Space) && item == null && isPutDown)
            {
                item = detectingItem;
                item.layer = 2;
                detectingItem = null;
                isPutDown = false;
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
                gameManager.showPopText("My backpack is full");
            } else
            {
                //add item to the backpack
                backPack.Add(item);
                isPutDown = true;
                gameManager.showPopText(item.name + " is in backpack");
                //when item is in the backpack, it is no longer in looping list
                gameManager.deleteFromLoop(item);
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

            if (backPack.Count > 0)
            {
                drawOutline(backPack[element], element);
                takeOutFromPack(backPack[element], element);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backPackSearch = false;
                indicator.SetActive(false);
                drawingOuline = false;
            }
        }
    }

    void drawOutline(GameObject g, int i) //draw outline for items in back pack that player is looking at
    {
        Vector3 pos = new Vector3(g.gameObject.transform.position.x - 1, g.gameObject.transform.position.y);
        if (!drawingOuline) {
            indicator.SetActive(true);
            //outline = Instantiate(g, pos, g.transform.rotation);
            //outline.SetActive(true);
            //SpriteRenderer sr = outline.GetComponent<SpriteRenderer>();
            //sr.color = Color.white;
            if (!outlineDrawed)
            {
                //outline.transform.localScale *= outlineThickness;
                //outlineDrawed = true;
            }
            indicator.transform.position = pos;
            drawingOuline = true;
        } else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                //outline.SetActive(true);
                //outline.transform.position = pos;
                indicator.transform.position = pos;
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

                //when taken out from backpack the object back to loop list
                gameManager.addToLoop(g, i);

            } /*else if (item != null)
            {
                GameObject temp = item;
                item = g;
                backPack[i] = temp;
                element = 0;
                backPack.Remove(g);
                backPackSearch = false;
                indicator.SetActive(false);

                //when taken out from backpack the object back to loop list, the object that switched to the backpack needs to be removed from loop list
                gameManager.addToLoop(g, i);
                gameManager.deleteFromLoop(temp);
            }*/
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

    //interaction between keys and gates [since they cannot be destroyed from loop list, I need to make them inactive]
    void keyToGate(Collision2D c)
    {
        if (item != null)
        {
            switch (item.name)
            {
                case "Red Key":
                    if (c.gameObject.name == "Gate Red")
                    {
                        c.gameObject.SetActive(false);
                        item.SetActive(false);
                        //Destroy(c.gameObject);
                        //Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Red gate opened");
                    }
                    break;

                case "Blue Key":
                    if (c.gameObject.name == "Gate Blue")
                    {
                        c.gameObject.SetActive(false);
                        item.SetActive(false);
                        //Destroy(c.gameObject);
                        //Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Blue gate opened");
                    }
                    break;

                case "Yellow Key":
                    if (c.gameObject.name == "Gate Yellow")
                    {
                        c.gameObject.SetActive(false);
                        item.SetActive(false);
                        //Destroy(c.gameObject);
                        //Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Yellow gate opened");
                    }
                    break;

                case "Green Key":
                    if (c.gameObject.name == "Gate Green")
                    {
                        c.gameObject.SetActive(false);
                        item.SetActive(false);
                        //Destroy(c.gameObject);
                        //Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Green gate opened");
                    }
                    break;

                case "Pink Key":
                    if (c.gameObject.name == "Gate Pink")
                    {
                        c.gameObject.SetActive(false);
                        item.SetActive(false);
                        //Destroy(c.gameObject);
                        //Destroy(item);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("Pink gate opened");
                    }
                    break;

                case "coin":
                    Debug.Log("1");
                    if (c.gameObject.name == "test")
                    {
                        Debug.Log("111");
                        item.SetActive(false);
                        item = null;
                        handEmpty = true;
                        gameManager.showPopText("clock Shows at left conor");
                        isClockGetted = true;
                    }
                    break;
            }
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gate" || collision.gameObject.tag == "npc")
        {
            keyToGate(collision);
            isPutDown = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "coin")
        {
            gameManager.showPopText("YOU FIND THE COIN!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            isPutDown = true;
            Debug.Log("out");
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

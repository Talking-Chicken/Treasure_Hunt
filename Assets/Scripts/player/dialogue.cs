using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    public GameManager gameManager;
    public playerBehaviour player;

    public GameObject textBox;
    public GameObject textBoxBG;
    public Text textBoxComponent;

    GameObject NPC;

    public List<string> testDialogue = new List<string>();

    int test;
    int currentLine = 0; //show the current line of dialogue that NPC is speaking

    int break1 = 9; //this is the line that seperate NPC of taking basic control and hints 

    [SerializeField]
    float above; //distance of text box above NPC

    // Start is called before the first frame update
    void Start()
    {
        test = 0;
        player.dialogues.Add(testDialogue);
        textBox.SetActive(false);
        textBoxBG.SetActive(false);
        testAddDia();
    }

    // Update is called once per frame
    void Update()
    {
        NPC = player.detectNPC();
        switch (NPC.name)
        {
            case "test":
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (currentLine < break1)
                    {
                        if (player.isClockGetted)
                        {
                            currentLine = break1;
                        }
                        textBox.SetActive(true);
                        textBoxBG.SetActive(true);
                        textBox.transform.position = new Vector2(NPC.transform.position.x, NPC.transform.position.y + above);
                        textBoxBG.transform.position = new Vector2(NPC.transform.position.x, NPC.transform.position.y + above);
                        textBoxComponent.text = testDialogue[currentLine];
                        currentLine++;
                    }
                    else if (!player.isClockGetted)
                    {
                        textBox.SetActive(false);
                        textBoxBG.SetActive(false);
                        currentLine = 0;
                    }
                    else
                    {
                        if (currentLine <= testDialogue.Count - 1)
                        {
                            Debug.Log(currentLine);
                            textBox.SetActive(true);
                            textBoxBG.SetActive(true);
                            textBox.transform.position = new Vector2(NPC.transform.position.x, NPC.transform.position.y + above);
                            textBoxBG.transform.position = new Vector2(NPC.transform.position.x, NPC.transform.position.y + above);
                            textBoxComponent.text = testDialogue[currentLine];
                            currentLine++;
                        } else
                        {
                            textBox.SetActive(false);
                            textBoxBG.SetActive(false);
                            currentLine = break1;
                        }
                    }
                }
                /*
                for (int i = 0; i < testDialogue.Count + 1;)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (i <= testDialogue.Count - 1)
                        {
                            textBox.SetActive(true);
                            textBoxBG.SetActive(true);
                            textBox.gameObject.transform.position = new Vector2(NPC.transform.position.x, NPC.transform.position.y + above);
                            textBoxComponent.text = testDialogue[i];
                            i++;
                        } else
                        {
                            textBox.SetActive(false);
                            textBoxBG.SetActive(false);
                        }
                    }
                }*/
                break;


            case "player":
                textBox.SetActive(false);
                textBoxBG.SetActive(false);
                currentLine = 0;
                break;
        }
    }

    void testAddDia()
    {
        testDialogue.Add("Hi, folk!");
        testDialogue.Add("is this the first time i talk to you?");
        testDialogue.Add("we are stucked in a time loop");
        testDialogue.Add("and we need to collect 5 batteries to recharge the time machine");
        testDialogue.Add("but first, give me the coin at the east, so that i can show you the time");
        testDialogue.Add("press space to pick it up");
        testDialogue.Add("you can press Q to pack it to your bag");
        testDialogue.Add("press I or F to search your bag");
        testDialogue.Add("whenever you want to exit press esc");

        //break 1
        testDialogue.Add("I remeber that I lost 2 batteries at the east");
        testDialogue.Add("2 batteries at the west");
        testDialogue.Add("and 1 at the south in the ocean");
        testDialogue.Add("if you feel tired");
        testDialogue.Add("press space to sit in chair");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bound")
        {
            gameManager.showPopText("There's no point go beyond");
        }
    }

}

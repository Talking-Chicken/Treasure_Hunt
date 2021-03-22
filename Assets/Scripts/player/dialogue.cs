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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentLine <= testDialogue.Count - 1)
                    {
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
                        currentLine = 0;
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
        }
    }

    void testAddDia()
    {
        testDialogue.Add("Hi, folk!");
        testDialogue.Add("can you do me a favor");
        testDialogue.Add("there's a coin at the east");
        testDialogue.Add("bring me that coin, i'll give you my watch");
    }

}

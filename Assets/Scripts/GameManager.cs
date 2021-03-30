using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //texting purpose
    public GameObject key;

    //pop up text
    public GameObject popUpText;
    public GameObject popUpTextBG;

    public Text popTextComponent;

    public float textTimeReset;
    float textTime;
    bool countDown = false;

    //time loop
    public List<GameObject> loopingObjs = new List<GameObject>();

    [SerializeField]
    List<Vector2> initialPos = new List<Vector2>();

    [SerializeField]
    List<Vector2> backpackInitialPos = new List<Vector2>();

    public float loopTimeReset;
    float loopTimeNormReset;
    float loopTimeChairReset;
    float loopTimeRevertReset;
    float loopTime;
    bool loopTimeCountDown = false;

    public int clockResetTimeHr, clockResetTimeMin, clockStartTimeHr, clockStartTimeMin, clockEndTimeHr, clockEndTimeMin;

    //time loop clock showing on screen
    public GameObject clockText;
    public GameObject clockTextBG;

    public Text clockTextComponent;

    //time loop increase speed while sitting on chair
    public playerBehaviour player;
    GameObject chair;
    public bool isSittingInChair = false;
    public bool detectedChair = false; //when raycast detect the chair it becomes true

    //time loop shows the death screen
    public GameObject deathScreen;
    Animator screenAnim;

    public GameObject clockReverse;
    public Text clockReverseComponent;

    public bool isReseting = false;

    // Start is called before the first frame update
    void Start()
    {
        //pop up text
        textTime = textTimeReset;

        //time loop
        recordInitialPost();
        loopTime = loopTimeReset;
        loopTimeNormReset = loopTimeReset;
        loopTimeChairReset = loopTimeNormReset / 2;
        loopTimeRevertReset = loopTimeNormReset / 12;
        clockStartTimeHr = clockResetTimeHr;
        clockStartTimeMin = clockResetTimeMin;

        loopTimeCountDown = true;

        for (int i = 0; i < initialPos.Count; i++)
        {
            //GameObject go = Instantiate(key, initialPos[i], gameObject.transform.rotation);
            //go.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //screen black out
        screenAnim = deathScreen.gameObject.GetComponent<Animator>();
        clockReverseComponent.text = clockTextComponent.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            textTime -= Time.deltaTime;
            if (textTime <= 0 )
            {
                popUpTextBG.SetActive(false);
                popUpText.SetActive(false);
                textTime = textTimeReset;
                countDown = false;
            }
        }

        //time loop
        clockCountDown();

        if (isReseting)
        {
            isSittingInChair = false;
            resetPos();
            //clockStartTimeHr = clockResetTimeHr;
            //clockStartTimeMin = clockResetTimeMin;

            //screen black out also need to reset
            deathScreen.SetActive(true);
            screenAnim.SetInteger("time", 0);
        }

        if (player.isClockGetted)
        {
            showClockText();
        }

        sittingInChair();
        screenBlackOut();
        showRevetTime();
        clockReverseComponent.text = clockTextComponent.text;
    }

    public void showPopText(string textToShow)
    {
        popUpTextBG.SetActive(true);
        popUpText.SetActive(true);
        popTextComponent.text = textToShow;
        countDown = true;
    }

    void recordInitialPost() //to record all object's initial position, in order to bring them back to those positions every loop of time
    {
        if (loopingObjs.Count > 0)
        {
            for (int i = 0; i < loopingObjs.Count; i++)
            {
                initialPos.Add(loopingObjs[i].transform.position);
            }
        }
    }

    public void deleteFromLoop(GameObject g) //all objects in backpack will be deleted from the loop list
    {
        if (loopingObjs.Count > 0 && g != null)
        {
            int element = loopingObjs.IndexOf(g);
            loopingObjs.Remove(g);
            backpackInitialPos.Add(initialPos[element]);
            initialPos.RemoveAt(element);
        }
    }

    public void addToLoop(GameObject g, int pos) // all objects that are taken from backpack will be added to the loop list
    {
        if (g != null)
        {
            loopingObjs.Add(g);
            initialPos.Add(backpackInitialPos[pos]);
            backpackInitialPos.Remove(backpackInitialPos[pos]);
        }
    }

    void clockCountDown()
    {
        if (loopTimeCountDown)
        {
            loopTime -= Time.deltaTime;
            if (loopTime <= 0 && !isReseting) //if loopTime ends, 1 minute is spent, which means clock time + 1 min
            {
                if (clockStartTimeMin < 59)
                {
                    clockStartTimeMin++;
                } else
                {
                    clockStartTimeMin = 0;
                    clockStartTimeHr++;
                }

                loopTime = loopTimeReset;
            }
        }
    }

    void resetPos() //to reset all objects that in loop list to their initial position
    {
        if (player.item != null)
        {
            player.item = null;
            player.handEmpty = true;
            player.isPutDown = true;
        }
        for (int i = 0; i < loopingObjs.Count; i++)
        {
            loopingObjs[i].SetActive(true);
            loopingObjs[i].transform.position = initialPos[i];

            if (loopingObjs[i].tag == "item") //set all items to item layer
            {
                loopingObjs[i].layer = 8;
            }
        }
        loopTimeReset = loopTimeNormReset;
    }

    void showClockText()
    {
        //clockText.SetActive(true);
        //clockTextBG.SetActive(true);
        if (clockStartTimeMin < 10)
        {
            clockTextComponent.text = clockStartTimeHr.ToString() + ":" + "0" + clockStartTimeMin.ToString();
        } else
        {
            clockTextComponent.text = clockStartTimeHr.ToString() + ":" + clockStartTimeMin.ToString();
        }
    }

    void sittingInChair()
    {
        if (player.detectChair().tag == "chair")
        {
            chair = player.detectChair().gameObject;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSittingInChair = true;
            }
        }

        if (isSittingInChair)
        {
            player.transform.position = new Vector2(chair.transform.position.x, chair.transform.position.y);
            loopTimeReset = loopTimeChairReset;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                loopTimeReset = loopTimeNormReset;
                isSittingInChair = false;
            }
        }
    }

    void screenBlackOut()
    {
        if (clockEndTimeHr - clockStartTimeHr <= 1)
        {
            deathScreen.SetActive(true);
            screenAnim.SetInteger("time", clockStartTimeMin);
            if (clockStartTimeMin == clockEndTimeMin)
            {
                isReseting = true;
            }
        }
    }

    void showRevetTime()
    {
        if (isReseting)
        {
            screenAnim.SetBool("resetComplete", true);
            clockReverse.SetActive(true);
            loopTimeReset = loopTimeRevertReset;
            if (loopTime <= 0)
            {
                if (clockStartTimeMin >= 0)
                {
                    clockStartTimeMin--;
                }
                else
                {
                    clockStartTimeHr--;
                    clockStartTimeMin = 59;
                }
                loopTime = loopTimeReset;
            }

            if (clockStartTimeHr <= 10 && clockStartTimeMin == 0)
            {
                isReseting = false;
                clockReverse.SetActive(false);
                loopTimeReset = loopTimeNormReset;
                screenAnim.SetBool("resetComplete", false);
                deathScreen.SetActive(false);
                player.score++;
            }
        }
    }
}

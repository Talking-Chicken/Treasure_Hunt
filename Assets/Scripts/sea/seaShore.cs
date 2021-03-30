using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seaShore : MonoBehaviour
{
    public GameManager gameManager;

    int highHr1 = 12;
    int highHr2 = 24;
    int lowHr = 18;
    int riseNum; //count how many times the water rised
    int lowNum; //count how many times the water lowered

    Vector2 startPos;

    public float riseY;

    public GameObject obstacle1;
    public GameObject obstacle2;

    // Start is called before the first frame update
    void Start()
    {

        startPos = gameObject.transform.position;
        riseNum = gameManager.clockStartTimeHr;
        lowNum = highHr1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isReseting)
        {
            if (checkRise())
            {
                rise();
            }
            else
            {
                low();
            }
        } else
        {
            riseNum = 0;
            lowNum = highHr1;
            gameObject.transform.position = startPos;
        }
    }

    bool checkRise() //return true when sea need to rise (<12 || >18<24)
    {
        if(gameManager.clockStartTimeHr <= highHr1)
        {
            return true;
        } else if (gameManager.clockStartTimeHr >= lowHr && gameManager.clockStartTimeHr <= highHr2)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void rise()
    {
        if (riseNum < gameManager.clockStartTimeHr)
        {
            gameObject.transform.position = new Vector2 (transform.position.x, transform.position.y + riseY);
            riseNum = gameManager.clockStartTimeHr;
        }
    }

    void low()
    {
        if (lowNum < gameManager.clockStartTimeHr)
        {
            gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - riseY);
            lowNum = gameManager.clockStartTimeHr;
        }
    }
}

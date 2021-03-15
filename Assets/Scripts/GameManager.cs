using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject popUpText;
    public GameObject popUpTextBG;

    public Text popTextComponent;

    public float textTimeReset;

    float textTime;

    bool countDown = false;

    // Start is called before the first frame update
    void Start()
    {
        textTime = textTimeReset;
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
    }

    public void showPopText(string textToShow)
    {
        popUpTextBG.SetActive(true);
        popUpText.SetActive(true);
        popTextComponent.text = textToShow;
        countDown = true;
    }
}

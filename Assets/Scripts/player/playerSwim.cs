using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSwim : MonoBehaviour
{
    public bool isSwimming;

    public float ResetSwimTime;
    [SerializeField]
    float swimTime;
    public int maxTotalSwim;
    public int currentTotalSwim; //this is the total time that player can stay in water

    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        swimTime = ResetSwimTime;
        currentTotalSwim = maxTotalSwim;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwimming)
        {
            gameManager.showPopText(currentTotalSwim.ToString());
            swimTime -= Time.deltaTime;
            if (swimTime <= 0)
            {
                currentTotalSwim--;
                swimTime = ResetSwimTime;
            }
        } else
        {
            currentTotalSwim = maxTotalSwim;
            swimTime = ResetSwimTime;
        }

        if (currentTotalSwim <= 0)
        {
            gameManager.isReseting = true;
            swimTime = ResetSwimTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sea")
        {
            isSwimming = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sea")
        {
            isSwimming = false;
        }
    }
}

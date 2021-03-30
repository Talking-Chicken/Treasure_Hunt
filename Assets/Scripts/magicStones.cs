using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class magicStones : MonoBehaviour
{
    public GameManager gameManager;
    public playerBehaviour player;


    public GameObject stone1;
    public GameObject stone2;
    public GameObject stone3;
    public GameObject stone4;
    public GameObject stone5;

    [SerializeField]
    bool stone1Get, stone2Get, stone3Get, stone4Get, stone5Get;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stone1Get && stone2Get && stone3Get && stone4Get && stone5Get)
        {
            SceneManager.LoadScene(1);
        }

        if (gameManager.isReseting)
        {
            Animator myAnim1 = stone1.gameObject.GetComponent<Animator>();
            myAnim1.SetBool("placed", false);
            Animator myAnim2 = stone2.gameObject.GetComponent<Animator>();
            myAnim2.SetBool("placed", false);
            Animator myAnim3 = stone3.gameObject.GetComponent<Animator>();
            myAnim3.SetBool("placed", false);
            Animator myAnim4 = stone4.gameObject.GetComponent<Animator>();
            myAnim4.SetBool("placed", false);
            Animator myAnim5 = stone5.gameObject.GetComponent<Animator>();
            myAnim5.SetBool("placed", false);

            stone1Get = false;
            stone2Get = false;
            stone3Get = false;
            stone4Get = false;
            stone5Get = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if(player.item == stone1)
            {
                stone1Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            } else if (player.item == stone2)
            {
                stone2Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone3)
            {
                stone3Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone4)
            {
                stone4Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone5)
            {
                stone5Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (player.item == stone1)
            {
                stone1Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone2)
            {
                stone2Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone3)
            {
                stone3Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone4)
            {
                stone4Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
            else if (player.item == stone5)
            {
                stone5Get = true;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (player.item == stone1)
            {
                stone1Get = false;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", false);
            } else if (player.item == stone2)
            {
                stone2Get = false ;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", false);
            } else if (player.item == stone3)
            {
                stone3Get = false;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", false);
            } else if (player.item == stone4)
            {
                stone4Get = false;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", false);
            }
            else if (player.item == stone5)
            {
                stone5Get = false;
                Animator myAnim = player.item.GetComponent<Animator>();
                myAnim.SetBool("placed", false);
            }
        }
    }
}

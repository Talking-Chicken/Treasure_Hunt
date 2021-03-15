using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractItems : MonoBehaviour
{
    bool handEmpty;
    bool pickedUp;
    bool isUsing;

     //raycasting distance

    GameObject interactingGameObject;

    // Start is called before the first frame update
    void Start()
    {
        handEmpty = true;
        pickedUp = false;
        isUsing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //raycasting to detect the item
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, );


        if (Input.GetKeyDown(KeyCode.Space) && interactingGameObject != null)
        {
            //try to put the item into the back pack
            if (interactingGameObject.tag == "item")
            {
            }

            //old code
            interactingGameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
            pickedUp = true;
            handEmpty = false;
            //interactingGameObject.layer = gameObject.layer - 1;
        }

        if (pickedUp && interactingGameObject != null)
        {
            interactingGameObject.transform.position = gameObject.transform.position;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log(isUsing);
            isUsing = true;
        } else
        {
            isUsing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject g;
        if (collision.gameObject.tag == "gate")
        {
            if (pickedUp && interactingGameObject != null) //here to insert isUsing in the future
            {
                g = collision.gameObject;
                Destroy(g);
                Destroy(interactingGameObject);
                pickedUp = false;
                handEmpty = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (handEmpty && collision.gameObject.tag == "item")
        {
            interactingGameObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (handEmpty && collision.gameObject.tag == "item")
        {
            interactingGameObject = null;
        }
    }

}

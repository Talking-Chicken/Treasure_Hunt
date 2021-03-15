using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractingSwitch : MonoBehaviour
{
    bool isTouching;
    public GameObject connectingBlock;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching && connectingBlock != null && speed>=0)
        {
            connectingBlock.transform.Translate(Vector3.up * Time.deltaTime * speed);
            speed -= 0.3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "switch") //notice that there is name not tag
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "switch") //notice that there is name not tag
        {
            isTouching = false;
        }
    }
}

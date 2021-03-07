using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    public float speed;

    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    float moveDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        checkKeys();
        handleMovement();
    }

    void checkKeys()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDir = 1;
            Debug.Log("W get");

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = 2;
            } else if (Input.GetKey(KeyCode.D))
            {
                moveDir = 4;
            }

        } else if (Input.GetKey(KeyCode.S))
        {
            moveDir = 3;
            Debug.Log("S get");

            if (Input.GetKey(KeyCode.A))
            {
                moveDir = 6;
            } else if (Input.GetKey(KeyCode.D))
            {
                moveDir = 8;
            }

        } else if (Input.GetKey(KeyCode.A))
        {
            moveDir = 5;
            Debug.Log("A get");

            if (Input.GetKey(KeyCode.W))
            {
                moveDir = 2;
            } else if (Input.GetKey(KeyCode.S))
            {
                moveDir = 6;
            }

        } else if (Input.GetKey(KeyCode.D))
        {
            moveDir = 7;
            Debug.Log("D get");

            if (Input.GetKey(KeyCode.W))
            {
                moveDir = 4;
            } else if (Input.GetKey(KeyCode.S))
            {
                moveDir = 8;
            }

        } else
        {
            moveDir = 0;
        }
    }

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
                myBody.velocity = new Vector3(-0.5f, 0.5f) * speed;
                break;

            case 4:
                myBody.velocity = new Vector3(0.5f, 0.5f) * speed;
                break;

            case 6:
                myBody.velocity = new Vector3(-0.5f, -0.5f) * speed;
                break;

            case 8:
                myBody.velocity = new Vector3(0.5f, -0.5f) * speed;
                break;

            case 0:
                myBody.velocity = new Vector3(0, 0);
                break;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTextDisplay : MonoBehaviour
{
    public Text textBox;
    bool found;

    // Start is called before the first frame update
    void Start()
    {
        found = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (found)
        {
            textBox.text = "You Find The Coin!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.name == "coin")
        {
            Debug.Log("found");
            found = true;
        }
    }
}

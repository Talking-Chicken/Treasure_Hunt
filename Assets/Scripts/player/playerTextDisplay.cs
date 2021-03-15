using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTextDisplay : MonoBehaviour
{
    public Text textBox;
    public Text layerCheck;
    bool found;

    int layer1;
    int layer2;
    int layer3;

    public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    // Start is called before the first frame update
    void Start()
    {
        layer1 = l1.layer;
        layer2 = l2.layer;
        layer3 = l3.layer;
        found = false;
    }

    // Update is called once per frame
    void Update()
    {
        //layerCheck.text = layer1.ToString()  + "\n" + layer2.ToString() +"\n"+ layer3.ToString() + "\n";

        if (found)
        {
            textBox.text = "You Find The Coin!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "coin")
        {
            found = true;
        }
    }
}

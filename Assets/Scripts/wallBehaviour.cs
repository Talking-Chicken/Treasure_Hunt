using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // make the wall transparent
        Color temp;
        temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0f;
        GetComponent<SpriteRenderer>().color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    SpriteRenderer sr;

    public int layerNum;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        layerNum = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        layerNum = gameObject.layer;
    }
}

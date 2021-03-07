using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selectionSort(gameObjects);
        setLayer(gameObjects);
    }

    float getYPos (GameObject other)
    {
        return other.transform.position.y;   
    }

    float getYBottom (GameObject other)
    {
        float yPos;
        float ySize;
        yPos = getYPos(other);
        ySize = other.transform.localScale.y;
        return yPos - (ySize/2);
    }

    void selectionSort(List<GameObject> unsortedList)
    {
        int min;
        GameObject temp;

        for (int i = 0; i < unsortedList.Count; i++)
        {
            min = i;

            for (int j = i+1; j < unsortedList.Count; j++)
            {
                if (getYBottom(unsortedList[i]) > getYBottom(unsortedList[j]))
                {
                    min = j;
                }

                if (min != i)
                {
                    temp = unsortedList[i];
                    unsortedList[i] = unsortedList[j];
                    unsortedList[j] = temp;
                }
            }
        }
    }

    void setLayer(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].layer = i;
        } 
    }
}

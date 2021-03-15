using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class layerManager : MonoBehaviour
{
    //test
    public Text t1;
    SpriteRenderer sr;
    GameObject temp1;
    GameObject temp2;
    GameObject temp3;
    GameObject temp4;

    public List<GameObject> gameObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        temp1 = gameObjects[0];
        temp2 = gameObjects[1];
        temp3 = gameObjects[2];
        temp4 = gameObjects[3];
        sr = temp1.gameObject.GetComponent<SpriteRenderer>();
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
                if (getYBottom(unsortedList[i]) < getYBottom(unsortedList[j]))
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
            list[i].GetComponent<SpriteRenderer>().sortingOrder = i;
            //list[i].transform.localScale = new Vector3(i+1,i+1);
            int pos1 = gameObjects.IndexOf(temp1);
            int pos2 = gameObjects.IndexOf(temp2);
            int pos3 = gameObjects.IndexOf(temp3);
            int pos4 = gameObjects.IndexOf(temp4);
            t1.text = "order in list:"+ pos1.ToString() + "\t" + pos2.ToString() + "\t" + pos3.ToString() +"\t"+pos4.ToString() +  "\n";
            t1.text += "y-axis: " + getYBottom(temp1).ToString() + "\t" + getYBottom(temp2).ToString() + "\t" + getYBottom(temp3).ToString()+"\t" +getYBottom(temp4).ToString()+ "\n";
            t1.text += "layer: " + temp1.layer.ToString() + "\t" + temp2.layer.ToString() + "\t" + temp3.layer.ToString()+"\t"+temp4.layer.ToString();
        } 
    }
}

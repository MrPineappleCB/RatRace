
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    [SerializeField] private float levelLength = 0f;
    public static List<GameObject> myListObjects = new List<GameObject>();
    public int genCounter = 0;
    float savedwidth = 0;
    float totaldist = 0;
  
    void Start()
    {
        Object[] subListObjects = Resources.LoadAll("LevelPrefabs", typeof(GameObject));

        foreach (GameObject subListObject in subListObjects)
        {
            GameObject lo = (GameObject)subListObject;
            myListObjects.Add(lo);
        }

        while (genCounter < levelLength)
        {
            int randSegment = Random.Range(0, myListObjects.Count);
            GameObject myObj = Instantiate(myListObjects[randSegment]) as GameObject;
            float width = myObj.GetComponent<SpriteRenderer>().bounds.size.x;
            transform.position = new Vector3((width/2 + savedwidth/2) + totaldist, -8, 0);
            myObj.transform.position = transform.position;
            totaldist = width / 2 + savedwidth / 2 + totaldist;
            savedwidth = width;
            genCounter++;
        }    
    }
}
    

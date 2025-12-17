
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class LevelGen : MonoBehaviour
{
    [SerializeField] public float levelLength = 0f;
    public static List<GameObject> myListObjects = new List<GameObject>();
    public static List<GameObject> lapList = new List<GameObject>();
    public int genCounter = 0;
    public int lapCounter = 1;
    public float savedwidth = 0;
    public float totaldist = 0;
    public float finaldist = 0f;
    public float savdist;
    float lapdist;
    float enddist = 7.5f;
    public GameObject gamemanager;
    public MainMenu mainMenu;
  
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        mainMenu = gamemanager.GetComponent<MainMenu>();
        levelLength = mainMenu.laplength;

        Object[] subListObjects = Resources.LoadAll("LevelPrefabs", typeof(GameObject));

        foreach (GameObject subListObject in subListObjects)
        {
            GameObject lo = (GameObject)subListObject;
            myListObjects.Add(lo);
        }

        while (genCounter < levelLength)
        {
            //generates random number
            int randSegment = Random.Range(0, myListObjects.Count -2);
            //Instantiates level segment prefab
            GameObject myObj = Instantiate(myListObjects[randSegment]) as GameObject;
            //saves level segment prefab to list for further lap generation
            lapList.Add(myObj);
            //gets width of segment
            float width = myObj.GetComponent<SpriteRenderer>().bounds.size.x;
            //sets destination for level segment at the end of the last segment
            transform.position = new Vector3((width / 2 + savedwidth / 2) + totaldist + 15, -8, 0);
            //sets position for level segment
            myObj.transform.position = transform.position;
            //calculations for position of next level segment
            totaldist = (width / 2) + (savedwidth / 2) + totaldist;
            savedwidth = width;
            finaldist = myObj.transform.position.x + (savedwidth / 2) + enddist;
            savdist = finaldist;
            //increment counter to generate next piece
            genCounter++;
        }
        if (mainMenu.lap != 1)
        {
            GameObject end = Instantiate(myListObjects[8]);
            end.transform.position = new Vector3(finaldist,-8,0);
        }

        while (lapCounter < mainMenu.lap)
        {
            for (int i=0; i< lapList.Count; i++)
            {
                //instatiates saved lap segment in order
                GameObject lapObj = Instantiate(lapList[i]) as GameObject;
                //width of each segment
                float width = lapObj.GetComponent<SpriteRenderer>().bounds.size.x;
                //sets destination for level segment at the end of the last segment
                lapObj.transform.position = new Vector3(lapObj.transform.position.x + savdist - enddist, -8, 0);
                //calculations for position of next level segment
                lapdist = lapObj.transform.position.x + (width/2) + enddist;
            }
            //generates end of lap segment
            savdist = lapdist;
            GameObject lapend = Instantiate(myListObjects[8]);
            lapend.transform.position = new Vector3(savdist,-8,0);
            lapCounter++;
        }

        


        AstarPath.active.Scan();    
    }
}
    

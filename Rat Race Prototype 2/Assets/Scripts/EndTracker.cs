using Unity.VisualScripting;
using UnityEngine;

public class EndTracker : MonoBehaviour
{
    private LevelGen levelGenScript;
    public GameObject levelGenerator;
    public MainMenu mainMenu;
    public GameObject gamemanager;
    //float distance = levelGenerator.GetComponent<LevelGen>.totaldist;
    void Start()
    {
        levelGenScript = levelGenerator.GetComponent<LevelGen>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        mainMenu = gamemanager.GetComponent<MainMenu>();
    }

    
    void Update()
    {
        float distance = levelGenScript.finaldist;
        float lapdist = levelGenScript.savdist;
        //transform.position = new Vector3(distance, -8, 0);

        if (mainMenu.lap == 1)
        {
            transform.position = new Vector3(distance,-8,0);
        }
        else //if (mainMenu.lap == 3)
        {
            transform.position = new Vector3(lapdist,-8,0);
        }
        //else if (mainMenu.lap == 5)
        //{
            //transform.position = new Vector3(lapdist,-8,0);
        //}
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class EndTracker : MonoBehaviour
{
    private LevelGen levelGenScript;
    public GameObject levelGenerator;
    //float distance = levelGenerator.GetComponent<LevelGen>.totaldist;
    void Start()
    {
        levelGenScript = levelGenerator.GetComponent<LevelGen>();
    }

    
    void Update()
    {
        float distance = levelGenScript.finaldist;
        transform.position = new Vector3(distance, -8, 0);
    }
}

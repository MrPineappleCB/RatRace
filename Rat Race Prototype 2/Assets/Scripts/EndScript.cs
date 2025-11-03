using UnityEngine;
using UnityEngine.Events;

public class EndScript : MonoBehaviour
{

    public Animator rickyanimator;
    public Animator marthanimator;

    public UnityEvent enteredTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            enemyWin();
        }
    }

    void enemyWin()
    {
        rickyanimator.SetBool("", true);
    }
    
    void playerWin()
    {
        
    }
}

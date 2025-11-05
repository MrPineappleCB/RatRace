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
        if (other.gameObject.tag == "Enemy")
        {
            enemyWin();
            enteredTrigger.Invoke();
        }

        else if (other.gameObject.tag == "Player")
        {
            playerWin();
            enteredTrigger.Invoke();
        }
    }

    void enemyWin()
    {
        rickyanimator.SetBool("Lose", true);
    }
    
    void playerWin()
    {
        rickyanimator.SetBool("Win", true);
        marthanimator.SetBool("Lose", true);
    }
}

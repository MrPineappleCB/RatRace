using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EndScript : MonoBehaviour
{

    public Animator rickyanimator;
    public Animator marthanimator;
    public GameObject endMenu;
    public Enemy enemy;
    public TMP_Text endText;

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
        endText.text = ("You Lose");
        rickyanimator.SetBool("Lose", true);
        StartCoroutine(RickyDeath());

    }
    
    void playerWin()
    {
        endText.text = ("You Win!");
        endMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator RickyDeath()
    {
        yield return new WaitForSeconds(2f);
        endMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}

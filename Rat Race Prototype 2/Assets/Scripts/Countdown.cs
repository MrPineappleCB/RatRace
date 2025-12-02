using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private TMP_Text text;
    public float remainingtime = 5;

    void Start()
    {
        enemy.GetComponent<Enemy>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
    }

    void Update()
    {
        remainingtime -= Time.deltaTime;
        int timeint = Mathf.FloorToInt(remainingtime);
        string timetext = timeint.ToString();
        text.text = timetext;
        if (remainingtime <= 0)
        {
            enemy.GetComponent<Enemy>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            text.enabled = false;
        }
    }
}
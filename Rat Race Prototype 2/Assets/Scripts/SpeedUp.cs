using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public GameObject player;
    public float speedIncrease = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<PlayerController>().walkSpeed += speedIncrease;
        player.GetComponent<PlayerController>().savedSpeed += speedIncrease;

        
        Destroy(gameObject);
    }
}

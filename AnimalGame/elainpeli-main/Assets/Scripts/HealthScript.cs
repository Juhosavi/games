using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int playerHealth = 100;
    private Rigidbody2D rb;
    public int playerScore = 0;
    public Slider slider;
    [SerializeField] private AudioSource damageSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
            var enemyPos = new Vector2(collision.transform.position.x, collision.transform.position.y);
            gameObject.GetComponent<PlayerController>().PushBack(enemyPos);
            slider.value = slider.value - 20;
            damageSound.Play();
        }

    }
    public void GainHealth(int healAmount)
    {
        if (playerHealth < 100)
        { playerHealth = playerHealth + healAmount; }

        playerScore = playerScore + 1;

        if (slider.value < 100)
        {
            slider.value = slider.value + 20;
        }
    }
    void TakeDamage(int damage)
    {
        playerHealth = playerHealth - damage;
        if (playerHealth <= 0)
        {
            this.GetComponent<GameOverScript>().GameOver(playerScore);
        }
    }
}

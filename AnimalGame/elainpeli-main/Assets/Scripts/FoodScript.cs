using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] private AudioSource healingSound;

    public GameObject playerWallaby;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healingSound.Play();
            Debug.Log("Collision!");
            playerWallaby.GetComponent<HealthScript>().GainHealth(20);
            StartCoroutine(DelayTimer());
            
        }
    }
    private IEnumerator DelayTimer(){
            while(true){
                yield return new WaitForSeconds(0.1f);
                Destroy(gameObject);
            }
    }
    void Update()
    {
        transform.Rotate(0f, 0f, 0.4f, Space.Self);
    }
}

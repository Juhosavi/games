using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, jumpSound, enemyDeathSound, diamondSound, gameOverSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        // Ladataan ‰‰nitiedostot resursseista ja asetetaan ne vastaaviin muuttujiin
        diamondSound = Resources.Load<AudioClip>("itemSound");
        playerHitSound = Resources.Load<AudioClip>("PlayerHit");
        enemyDeathSound = Resources.Load<AudioClip>("EnemyHit");
        gameOverSound = Resources.Load<AudioClip>("GameOver");
        jumpSound = Resources.Load<AudioClip>("Bell");


        audioSrc = GetComponent<AudioSource>(); // Haetaan AudioSource-komponentti

    }

    // Update is called once per frame
    void Update()
    {


    }
    public static void PlaySound(string clip)  // Staattinen metodi, jolla voidaan toistaa ‰‰ni‰ annetun clip-nimen perusteella
    {
        switch (clip)
        {
            case "itemSound":
                audioSrc.PlayOneShot(diamondSound);
                break;
            case "PlayerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "EnemyHit":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "GameOver":
                audioSrc.PlayOneShot(gameOverSound);
                break;
            case "Bell":
                audioSrc.PlayOneShot(jumpSound);
                break;

        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public Animator animator;
    public Rigidbody2D playerRB;

    public bool grounded;

    public Transform groundCheck;
    public LayerMask groundLayer;



    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GroundDetect(); //tarkistaa onko hahmo maassa 
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0); //liikutetaan pelaajaa vaakasuunnassa.
        if (Input.GetAxisRaw("Horizontal") != 0) // jos pelaaja liikkuu
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1); //liikutaan suuntaan pelaajaan painamaan suuntaan.
            animator.SetBool("Walk", true); //toistetaan k‰vely animaatio

        }
        else //pys‰hdytty
        {
            animator.SetBool("Walk", false); //lopetetaan k‰vely animaatio 
        }

        if (Input.GetButtonDown("Jump") && grounded) //pelaaja painaa hyppy‰ ja on maassa.
        {
            SoundManagerScript.PlaySound("Bell"); //soitetaan ‰‰ni
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); //hyp‰t‰‰n
            animator.SetBool("Jump", true); //toistetaan hyppy animaatio
        }
        //hyppy muunnos
        if (playerRB.velocity.y < 0) 
        {
            playerRB.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Going to Main Menu");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Diamond"))  //poimitaan / osutaan timanttiin.
        {
            SoundManagerScript.PlaySound("itemSound"); //soitetaan itemsound
            SoundManagerScript.PlaySound("PlayerHit"); //soiteaan osuma‰‰ni.

            Destroy(other.gameObject); // tuhoa timantti
        }
        if (other.gameObject.CompareTag("LevelEnd")) // jos osutaan LevelEndiin
        {
            SceneManager.LoadScene("Level2"); //siirryt‰‰n LVL 2
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // osutaan viholliseen 
        {
            if (transform.position.y > collision.transform.position.y + collision.transform.localScale.y) // katsotaan ett‰ onko osuma vihollisen yl‰puolelta "p‰‰h‰n"
            {
                collision.gameObject.GetComponent<Enemy>().Die(); //kutsutaan Enemyn Die metodia 
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce * 0.4f); //osumasta "popmpataan" pikkuisen ylˆsp‰in.
                SoundManagerScript.PlaySound("EnemyHit"); //soitetaan enemyhit ‰‰ni.
            }
            else
            {
                CatDie(); //pelaaja kuolee

            }
        }
        if (collision.gameObject.CompareTag("Alien")) // osutaan Alien viholliseen 
        {

            if (transform.position.y > collision.transform.position.y + collision.transform.localScale.y) // katsotaan ett‰ onko osuma vihollisen yl‰puolelta "p‰‰h‰n"
            {

                collision.gameObject.GetComponent<Alien>().Die(); //kutsutaan Alienin Die metodia
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce * 0.4f); //osumasta "popmpataan" pikkuisen ylˆsp‰in.
                SoundManagerScript.PlaySound("EnemyHit");//soitetaan enemyhit ‰‰ni.
            }
            else
            {

                CatDie();
            }

        }

        if (collision.gameObject.CompareTag("FallDeath")) // jos osutaan FallDeath rajalle eli tiputaan alas kent‰st‰.
        {
            CatDie();
        }
    }
    public void CatDie() //pelaajan kuole,a 
    {

        animator.SetTrigger("Die"); //toistetaan animaatio Die 
        playerRB.velocity = new Vector2(0, 9); // Pelaaja "heitt‰ytyy" ylˆsp‰in
        Destroy(GetComponent<BoxCollider2D>()); //tuhotaan Boxcollider
        moveSpeed = 0; //pelaajan liikkumis nopeus nollaksi
        Destroy(gameObject, 6); //tuhotaan pelaaja gameobjet 6 sekunnin kuluttua
        StartCoroutine("ContinueTime"); //k‰ynnistet‰‰n Courutine Continue Time
        Time.timeScale = 0; // pys‰ytet‰‰n peli
        SoundManagerScript.PlaySound("GameOver"); //toistetaan gameover ‰‰ni.

    }
    IEnumerator ContinueTime()
    {
        yield return new WaitForSecondsRealtime(1); // odotetaan 1 sekuntti
        Time.timeScale = 1; //jatketaan peli‰
        yield return new WaitForSecondsRealtime(2); //odotetaan 2 sekuntia
        RestartLevel(); //k‰ynnistet‰‰n peli uudestaan
    }
    void RestartLevel()//k‰ynnistet‰‰n peli uudestaan
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Ladataan kyseinen scene miss‰ ollaan
    }


    void GroundDetect()
    {


        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); //tarkistaa osuuko pelaaaja maahan 
        animator.SetBool("isGrounded", grounded);

    }
}

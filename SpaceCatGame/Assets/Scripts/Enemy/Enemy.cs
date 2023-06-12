using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public GameObject detectionPoint; // Piste, jolla tarkistetaan maanpinnan kosketus
    public Animator animator;

    [SerializeField]
    private float direction; // Liikkumissuunta (-1 tai 1)

    [SerializeField]
    private bool changeDir; // Bool-arvo, joka kertoo, pit‰‰kˆ suuntaa vaihtaa

    [SerializeField]
    private LayerMask groundLayer;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction, 0, 0); // vihollinen k‰velee liikkumisnopeuden ja suunnan perusteella
        transform.localScale = new Vector3(direction, 1, 1); // liikkuu m‰‰ritettyyn suuntaan
    }

    private void LateUpdate()
    {
        Debug.DrawRay(detectionPoint.transform.position, Vector2.down, Color.green); // Piirt‰‰ raycastin alasp‰in tarkastaen onko alla maata

        RaycastHit2D hit = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, 1, groundLayer); // raycast tarkistaa maanpinnan

        if (hit.collider == null)
        {
            ChangeDirection(); // Jos ei havaita maata, vaihdetaan suuntaa
        }

        Debug.DrawRay(detectionPoint.transform.position, Vector2.right * direction * 0.2f, Color.blue); // Piirt‰‰ raycastin oikealle vihollisen suuntaan

        RaycastHit2D hit2 = Physics2D.Raycast(detectionPoint.transform.position, Vector2.right * direction, 0.2f, groundLayer); // Suorittaa raycastin oikealle vihollisen suuntaan

        if (hit2.collider != null)
        {
            ChangeDirection(); // Jos havaitaan este oikealla, vaihdetaan suuntaa
        }
    }

    void ChangeDirection()
    {
        Debug.Log("Suunnanvaihto");
        direction *= -1; // Vaihtaa liikkumissuunnan kertomalla sen -1:ll‰S
    }

    public void Die()
    {
        animator.SetTrigger("Die"); // Asettaa animatorissa "Die" triggerin p‰‰lle, jolloin vihollisen kuolemaan liittyv‰ animaatio k‰ynnistyy
        moveSpeed = 0; // Nollaa liikkumisnopeuden, jolloin vihollinen pys‰htyy
        Destroy(GetComponent<Rigidbody2D>()); // Poistaa Rigidbody2D-komponentin, jotta vihollinen ei en‰‰ reagoi fysiikkaan
        Destroy(GetComponent<BoxCollider2D>()); // Poistaa BoxCollider2D-komponentin, jotta
    }
}

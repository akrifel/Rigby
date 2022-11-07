using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpforce = 10f;
    [SerializeField] LayerMask Ground;

    [Header("Player Health")]
    [SerializeField] int health = 1;

    [Header("Bools")]
    [SerializeField] bool isGrounded;   

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        if (health <= 0)
            Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += Move * Time.deltaTime * speed;

        Jump();

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&  isGrounded == true)
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") 
        {
            isGrounded = true;
        } 

    }


    private void OnCollisionExit2D(Collision2D col)
    {

        isGrounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All different color states for the character
public enum ColorState{
    Regular,
    Red,
    Yellow,
    Blue, 
    Black
}
public class playerController : MonoBehaviour
{
    //possibly have multiple sprites for each color mouse variation.

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpforce = 10f;
    [SerializeField] LayerMask Ground;

    [Header("Player Health")]
    [SerializeField] int health = 1;

    [Header("Bools")]
    [SerializeField] bool isGrounded;
    private bool isFacingRight = true; 
    private Vector3 Move; 
    private Rigidbody2D rigidbody2d;
    private Animator anim;

    [SerializeField]

    private ColorState currentColor = ColorState.Regular;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (health <= 0)
            Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += Move * Time.deltaTime * speed;
        anim.SetFloat("speed", Mathf.Abs(Move.x));
        Jump();
        Flip();
        ChangeColor();
        ChangeSprite();

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&  isGrounded == true)
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }

    }

    //Changes the color state of the character based on
    private void ChangeColor(){
        if (Input.GetKey(KeyCode.Z))
        {
            currentColor = ColorState.Red;
        }
        if (Input.GetKey(KeyCode.X))
        {
            currentColor = ColorState.Blue;
        }
        if (Input.GetKey(KeyCode.C))
        {
            currentColor = ColorState.Yellow;
        }
        if (Input.GetKey(KeyCode.V))
        {
            currentColor = ColorState.Black;
        }
        if (Input.GetKey(KeyCode.B))
        {
            currentColor = ColorState.Regular;
        }
    }

    private void ChangeSprite(){
        //set weight of all layers in animator to zero
        for (int i = 0; i < anim.layerCount; i ++){
            anim.SetLayerWeight(i,0);
        }
        //sets the weight of desired layer to 1
      if(currentColor == ColorState.Red){
        anim.SetLayerWeight(1,1);
      }
      if(currentColor == ColorState.Regular){
        anim.SetLayerWeight(0,1);
      }
    }

    //flips sprite on local scale
    public void Flip(){
        if(isFacingRight && Move.x < 0f || !isFacingRight && Move.x > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") 
        {
            anim.SetBool("isJumping", false);
            isGrounded = true;
        } 
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        anim.SetBool("isJumping", true);
        isGrounded = false;
    }
    
    
}

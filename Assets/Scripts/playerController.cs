using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//All different color states for the character
public enum ColorState{
    Regular,
    Red,
    Blue,
    Yellow,
    Green,
    Black,
    
}
public class playerController : MonoBehaviour
{
    //possibly have multiple sprites for each color mouse variation.

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpforce = 10f;
    float oldspeed;
    float oldJump;
    [SerializeField] float yellowJF = 15f;
    [SerializeField] float yellowSpeed = 7f;

    [Header("Player Health")]
    [SerializeField] int health = 1;


    [Header("Player Health")]
    [SerializeField] int redLimit = 3;
    [SerializeField] int blueLimit = 3;
    [SerializeField] int yellowLimit = 3;
    //[SerializeField] int greenLimit = 1;
    //[SerializeField] int blackLimit = 1;

    [Header("Bools")]
    [SerializeField] bool isGrounded;
    private bool isFacingRight = true; 
    private Vector3 Move; 
    private Rigidbody2D rigidbody2d;
    private Animator anim;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform feetPosition;

    [SerializeField]
    private float checkRadius;

    [SerializeField]
    private Image[] diamonds;

    [SerializeField]
    private Sprite[] status;

    [SerializeField]
    private Sprite[] numbers;


    [SerializeField]

    public ColorState currentColor = ColorState.Regular;
    private int colorIndex;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oldspeed = speed;
        oldJump = jumpforce;

        diamonds[1].sprite = numbers[redLimit];
        diamonds[2].sprite = numbers[blueLimit];
        diamonds[3].sprite = numbers[yellowLimit];
    }
    private void FixedUpdate()
    {
        if (health <= 0)
            Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
        GroundCheck();
        Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += Move * Time.deltaTime * speed;
        anim.SetFloat("speed", Mathf.Abs(Move.x));
        Jump();
        Flip();
        ChangeColor();
        ChangeSprite();

        yellowVoid();

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rigidbody2d.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }
    }

    //Changes the color state of the character based on
    private void ChangeColor(){
        if (Input.GetKey(KeyCode.Z) && redLimit > 0 && currentColor != ColorState.Red)
        {
            currentColor = ColorState.Red;
            redLimit--;
            diamonds[1].sprite = numbers[redLimit];
            
        }
        if (Input.GetKey(KeyCode.X) && blueLimit > 0 && currentColor != ColorState.Blue)
        {
            currentColor = ColorState.Blue;
            blueLimit--;
            diamonds[2].sprite = numbers[blueLimit];
            
        }
        if (Input.GetKey(KeyCode.C) && yellowLimit > 0 && currentColor != ColorState.Yellow)
        {
            currentColor = ColorState.Yellow;
            yellowLimit--;
            diamonds[3].sprite = numbers[yellowLimit];
        }
        /*
        if (Input.GetKey(KeyCode.V))
        {
            currentColor = ColorState.Green;
        }
        if (Input.GetKey(KeyCode.B))
        {
            currentColor = ColorState.Black;
        }
        */
        if (Input.GetKey(KeyCode.N))
        {
            currentColor = ColorState.Regular;
        }
        diamonds[0].sprite = status[(int)currentColor];
    }
    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Blue Floor" && currentColor != ColorState.Blue)
        {
            GameManager.isGameOver = true;
        }
    }


    private void ChangeSprite(){
        //set weight of all layers in animator to zero
        for (int i = 0; i < anim.layerCount; i ++){
            anim.SetLayerWeight(i,0);
        }
        //sets the weight of desired layer to 1
        anim.SetLayerWeight((int)currentColor, 1);
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

    void GroundCheck(){
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);
        anim.SetBool("isJumping",!isGrounded);
    }

    void yellowVoid()
    {

        if (currentColor == ColorState.Yellow)
        {
            speed = yellowSpeed;
            jumpforce = yellowJF;
        }
        else
        {
            speed = oldspeed;
            jumpforce = oldJump;
        }

    }
}

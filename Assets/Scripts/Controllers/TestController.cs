using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputAction moveInput;
    //[SerializeField] InputAction jumpInput;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] bool isGrounded;
    [SerializeField] SpriteRenderer spriteRenderer;

    

    private void Awake()
    {
        moveInput = playerInput.actions["Move"];
        //jumpInput = playerInput.actions["Jump"];
    }

    private void OnMove(InputValue inputValue)
    {
       // rb.velocity = new Vector2(inputValue.Get<Vector2>().x, rb.velocity.y);
    }
    private void OnJump(InputValue inputValue)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower);

        }
        else
        {
            return;
        }

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script has run one time");
        
    }

    private void FixedUpdate()
    {
        Debug.Log("Script has run every fixed frame");
       // rb.velocity = new Vector2(rb.velocity.x, 5f);
       rb.velocity = new Vector2(moveInput.ReadValue<Vector2>().x * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Script has run every frame");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else
        {
            return;
        }
    }



}

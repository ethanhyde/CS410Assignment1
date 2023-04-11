using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
// jumps
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);

   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    // Double jump
    // Found here: https://u3ds.blogspot.com/2021/10/double-jump-rigidbody-unity-game-engine.html
    public float jumpForce = 5;
    public float groundDistance = 0.5f;
 
    Rigidbody rigidBody;
    bool canDoubleJump;
 
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
 
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded())
            {
                rigidBody.velocity = Vector3.up * jumpForce;
                canDoubleJump = true;
            }
            else if(canDoubleJump)
            {
                rigidBody.velocity = Vector3.up * jumpForce;
                canDoubleJump = false;
            }
        }
    }
}


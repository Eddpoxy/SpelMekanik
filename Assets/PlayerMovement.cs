using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")] 
    public float moveSpeed;
    public float groundDrag;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded; 

    public Transform oritentation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    bool TouchingGround;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        if (grounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        } 
        if (Input.GetKeyDown(KeyCode.Space) && TouchingGround == true)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            TouchingGround = false;
        }
        
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical"); 

    } 
    private void MovePlayer()
    {
        moveDirection = oritentation.forward * verticalInput + oritentation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain" || collision.gameObject.name.Contains("Item"))
        {
            TouchingGround = true;
        } 
    }
}

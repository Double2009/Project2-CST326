using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;
    public bool feetInContactWithGround;

    [Header("Debug Stuff")]
    public bool isGrounded;


    Rigidbody rb;
    Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void UpdateAnimation(){
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    // Update is called once per frame
    void Update()
    {


        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);

        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Math.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;


        Collider collider = GetComponent<Collider>();
        Vector3 startPoint = transform.position;
        float castDistance = collider.bounds.extents.y + 0.01f;

        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

        animator.SetBool("In Air", !isGrounded);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){

            if(isGrounded){
                rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
            }
        }
        else if(Input.GetKey(KeyCode.Space) && !isGrounded){
            if(rb.linearVelocity.y > 0){
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
        }

        if(horizontalAmount == 0){
            Vector3 decayedVelocity = rb.linearVelocity;
            newVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayedVelocity;
        }
        else{
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }

        UpdateAnimation();  

    }

     void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("QuestionBlock")){

            if(rb.linearVelocity.y > 0.1f){
                if (collision.gameObject.CompareTag("Brick")){
                    GameManager.Instance.AddScore(100);
                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.CompareTag("QuestionBlock")){
                    GameManager.Instance.AddScore(100);
                    GameManager.Instance.AddCoin();
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
     if(other.gameObject.CompareTag("Water")) {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    else if(other.gameObject.CompareTag("Goal")){
            GameManager.Instance.GameWon();
        }

   }
}

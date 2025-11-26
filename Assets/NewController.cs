using EasyTalk.Controller;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float horizontalInput;
    private Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    public float forwardInput;
    private Vector3 m_EulerAngleVelocity;
    public GameObject dialogueSystem;
    DialogueController dialogueController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dialogueController = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();

    }

    void Update()
    {
        /*float horizontalInput = Input.GetAxis("Horizontal"); // For A/D keys or left/right arrow keys
        // Or for mouse input:
        // float mouseX = Input.GetAxis("Mouse X");

        // Rotate around the Y-axis (up axis)
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // For mouse input, you might use:
        // transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);*/
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        // Optional: Check for a specific tag or layer if you only want certain objects to interact
        if (other.CompareTag("Door"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            dialogueController.PlayDialogue("Door1");
            Debug.Log("Hit the exit");
        }
    }
    private void FixedUpdate()

    {
        /*//float horizontalInput = Input.GetAxis("Horizontal");
        //Quaternion deltaRotation = Quaternion.Euler(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
        Vector3 forwardInput = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //rb.MoveRotation(rb.rotation*deltaRotation);
        rb.MovePosition(transform.position + forwardInput * Time.fixedDeltaTime * speed);
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrows
        float rotateInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Calculate rotation
        float rotation = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Calculate movement in the forward direction
        Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);*/
        
        
            float moveInput = Input.GetAxis("Vertical");
            float rotateInput = Input.GetAxis("Horizontal");

            // Rotation
            float rotation = rotateInput * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);

            // Movement using velocity (respects collisions)
            Vector3 movement = transform.forward * moveInput * speed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        
    }

}

using UnityEngine;

public class BullyScript : MonoBehaviour
{
    public Transform targetObject; // Assign your target object in the Inspector
    public float speed = 1f; // Adjust the speed of movement
    private bool isChassing;
    public GameObject homework;
    private Animator anim;


    void Update()
    {
        // Move the current object towards the target object's position
        if (isChassing)
        {
            Debug.Log("isChasing");
            

            // Get target position but keep bully's Y position (height)
            Vector3 targetPosition = new Vector3(targetObject.position.x, transform.position.y, targetObject.position.z);

            Vector3 direction = (targetPosition - transform.position).normalized;

            // Move towards player
            transform.position += direction * speed * Time.deltaTime;

            // Make the object face the player (but only rotate on Y axis)
            Vector3 lookPosition = new Vector3(targetObject.position.x, transform.position.y, targetObject.position.z);
            transform.LookAt(lookPosition);
        }
    }
    public void Homework()
    {
        homework.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isChassing = false;
        anim = GetComponent<Animator>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
    public void Chase()
    {
        Debug.Log("isChasing is on");
        anim.SetBool("Chase", true);

        isChassing = true;
    }
    public void ChaseOff()
    {
        isChassing = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Chase();


        }
    }
}

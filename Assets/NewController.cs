using EasyTalk.Controller;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

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
    public string teacherCurrent;
    public bool isTalking;
    public GameObject objectiveUI;
    public TextMeshProUGUI objectiveText;
    public GameObject cafe;
    public GameObject library,roof;
    public GameObject classRoom;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject Clock;
    public GameObject roofCam1, roofCam2,roofTrigger,endCamera,EvilTeacher,Bully;
    private GameObject currentCamera;
    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCamera = camera1;
        rb = GetComponent<Rigidbody>();
        dialogueController = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();
        teacherCurrent = "Teacher1";
        isTalking = false;
        objectiveText.text = "先生と話す";

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
        

        Debug.Log("You have hit something");
        // Optional: Check for a specific tag or layer if you only want certain objects to interact
        if (other.CompareTag("Door"))
        {
            objectiveUI.SetActive(false);
            // rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            isTalking = true;
           // rb.constraints = RigidbodyConstraints.None;
            dialogueController.PlayDialogue("Door1");
            Debug.Log("Hit the exit");
        }
        else if (other.CompareTag("Teacher"))
        {
            Debug.Log("You have hit teacher");
            objectiveUI.SetActive(false);
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            isTalking = true;
           // rb.constraints = RigidbodyConstraints.None;
            dialogueController.PlayDialogue(teacherCurrent);
            Debug.Log("teacher is talking");
        }
        else if (other.CompareTag("Friend"))
        {
            Debug.Log("talking to friend");
            objectiveUI.SetActive(false);
            isTalking = true;
            dialogueController.PlayDialogue("Friend");
            Debug.Log("friend is talking");

        }
        else if (other.CompareTag("CafeExit"))
        {
            Debug.Log("hit the cafe exit");
            objectiveUI.SetActive(false);
            isTalking = true;
            dialogueController.PlayDialogue("CafeExit");
            Debug.Log("Choosing where to go");
        }
        else if (other.CompareTag("Librarian"))
        {
            Debug.Log("Talking to Librarian");
            objectiveUI.SetActive(false);
            isTalking = true;
            dialogueController.PlayDialogue("Librarian");
            Debug.Log("Find Clock");
            Clock.SetActive(true);
        }
        else if (other.CompareTag("Clock"))
        {
            Debug.Log("Interacting with Clock");
            objectiveUI.SetActive(false);
            isTalking = true;
            dialogueController.PlayDialogue("Clock");
            Debug.Log("Found Clock");
        }
        else if (other.CompareTag("EvilTeacher"))
        {
            Debug.Log("Game Over");
            objectiveUI.SetActive(false);
            isTalking = true;
            other.GetComponent<TeacherChaseScript>().ChaseOff();
            GameOver();
            dialogueController.PlayDialogue("Game Over");
            Debug.Log("GameOverScreen Time");
        }
        else if (other.CompareTag("Bully"))
        {
            Debug.Log("Game Over");
            objectiveUI.SetActive(false);
            isTalking = true;
            other.GetComponent<BullyScript>().ChaseOff();
            GameOver();
            dialogueController.PlayDialogue("Game Over");
            Debug.Log("GameOverScreen Time");
        }
        else if (other.CompareTag("LibraryDoor"))
        {
            StopChase();
            Debug.Log("leaving libary");
            TeleportTo("Roof","SOmething");
            Debug.Log("Leaving Library");
        }
        else if (other.CompareTag("RoofTrigger"))
        {
            Debug.Log("RoofCutscene Started");
            objectiveUI.SetActive(false);
            isTalking = true;
            dialogueController.PlayDialogue("Roof");
            roofTrigger.SetActive(false);
        }
        else if (other.CompareTag("Homework"))
        {
            StopChase();
            Debug.Log("You Win");
            objectiveUI.SetActive(false);
            isTalking = true;
            WinCamera();
            dialogueController.PlayDialogue("Homework");
            roofTrigger.SetActive(false);
        }


    }
    public void StopChase()
    {
        EvilTeacher.SetActive(false);
        Bully.GetComponent<BullyScript>().ChaseOff();
    }
    
        
    
    public void SetObjectiveText(string message)
    {
        Debug.Log("message was changed");
        
    }
    public void UpdateBool(string message) 
    {
        Debug.Log("updater was hit");
        objectiveText.text = message;
        objectiveUI.SetActive(true);
        isTalking = false;
        rb.isKinematic = false;
    }
    private string pendingMessage;

    public void TeleportTo(string location, string message1)
    {
        if (location == "cafe")
        {
            transform.position = cafe.transform.position;
        }
        else if (location == "Library")
        {
            transform.position = library.transform.position;
        }
        else if (location == "Class")
        {
            transform.position = classRoom.transform.position;
        }
        else if (location == "Roof")
        {
            transform.position = roof.transform.position;
        }

        //Invoke("UpdateBoolDelayed", 2f);
    }

    private void UpdateBoolDelayed()
    {
        UpdateBool(pendingMessage);
    }
    private void GameOver()
    {

        camera1.SetActive(false);
        camera2.SetActive(true);
    }
    public void Reset()
    {
        Debug.Log("Reset was hit");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GetOutPls()
    {
       
        
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
        
    }
    public void RoofCam1()
    {
        currentCamera.SetActive(false);
        roofCam1.SetActive(true);
        currentCamera = roofCam1;
    }
    public void RoofCam2()
    {
        currentCamera.SetActive(false);

        roofCam2.SetActive(true);
        currentCamera = roofCam2;
    }
    public void MainCamera()
    {
        currentCamera.SetActive(false);
        camera1.SetActive(true);
        currentCamera = camera1;
    }
    public void WinCamera()
    {
        currentCamera.SetActive(false);
        endCamera.SetActive(true);
        currentCamera = endCamera;
    }

    private void FixedUpdate()

    {
        if(!isTalking)
        {
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

}

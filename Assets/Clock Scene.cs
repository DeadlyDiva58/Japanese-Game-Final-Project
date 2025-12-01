using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    public CinemachineBrain Brain;
    public GameObject Teacher1;
    public GameObject camera1;
    public GameObject camera2;
    

    void Start()
    {
        // Set initial camera priorities
       
    }

    public void StartScence()
    {
        Debug.Log("StartScene played");
        Teacher1.SetActive(true);
        camera1.SetActive(false);
        camera2.SetActive(true);
        
    }

    public void EndScene()
    {
        //Teacher1.GetComponent<ChaseScript>();
        //makes run
        camera1.SetActive(true);
        camera2.SetActive(false);
        Teacher1.GetComponent<TeacherChaseScript>().Chase();
    }
}
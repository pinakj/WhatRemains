using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public GameObject Cam;

    public Transform Finishpoint;
    private Vector3 Finish;
    private Vector3 StartPoint;
    public bool Repreatable = false;
    public float speed = 1.0f;

    float startTime, totalDistance;
    //Vector3 startCameraPosition;

    private bool canTrans = false;
    // Start is called before the first frame update

    void Start()
    {
        Vector3 Finish = Finishpoint.transform.position;
        //startCameraPosition = Cam.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (canTrans)
        {
            float currentDuration = (Time.time - startTime);
            float journeyFraction = currentDuration / 1f;
            Cam.transform.position = Vector3.Lerp(StartPoint, Finishpoint.position, journeyFraction);
            //print(journeyFraction + "," + Cam.transform.position);
        }

        //canTrans = false;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("isTransition", true);

            canTrans = true;
            startTime = Time.time;
            StartPoint = Cam.transform.position;
            totalDistance = Vector3.Distance(StartPoint, Finishpoint.position);
            print("yo");
        }
    }

    

}

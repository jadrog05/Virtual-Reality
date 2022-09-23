//using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Security.Cryptography;
using UnityEngine;

public class CarRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route; 
    public int routeNumber = 0;
    public int targetWP = 0;
    public float dist;  
    public Rigidbody rb;
    public bool go = false; 
    public float initialDelay;
    public float velocity_adjuster = 8.5f;

    public Transform t1;
    public Transform t2;
    public Transform t3;

    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        wps = new List<Transform>(); 
        GameObject wp; 
        
        wp = GameObject.Find("CWP1"); 
        wps.Add(wp.transform); 
         
        wp = GameObject.Find("CWP2"); 
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP3"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP4"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP5"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP6"); 
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP7"); 
        wps.Add(wp.transform);        

        wp = GameObject.Find("CWP8"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP9"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP10"); 
        wps.Add(wp.transform); 

        wp = GameObject.Find("CWP11"); 
        wps.Add(wp.transform);

        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");

        
        t1green = t1.FindChild("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;


        SetRoute();

        initialDelay = Random.Range(2.0f, 12.0f); 
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go) 
        { 
            initialDelay -= Time.deltaTime; 
            if (initialDelay <= 0.0f) 
            { 
                go = true; 
                SetRoute(); 
            } 
        else return; 
        }

        Vector3 displacement = route[targetWP].position - transform.position; 
        displacement.y = 0; 
        float dist = displacement.magnitude;

        if (dist < 0.1f) 
        { 
            targetWP++; 
            if (targetWP >= route.Count) 
            { 
                SetRoute(); 
                return; 
            } 
        } 

        //calculate velocity for this frame 
        Vector3 velocity = displacement; 
        velocity.Normalize(); 
        velocity *= velocity_adjuster; 
        //apply velocity 
        Vector3 newPosition = transform.position; 
        newPosition += velocity * Time.deltaTime; 
        newPosition.y = 1f;
        rb.MovePosition(newPosition); 

        //align to velocity 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 
        10.0f * Time.deltaTime, 0f); 
        Quaternion rotation = Quaternion.LookRotation(desiredForward); 
        rb.MoveRotation(rotation);
    }

    void SetRoute() 
    { 
    //randomise the next route 
    routeNumber = Random.Range(0, 4); 
 
    //set the route waypoints 
    if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1], 
    wps[2] }; 
    else if (routeNumber == 1) route = new List<Transform> { wps[0], wps[1], 
    wps[3] }; 
    else if (routeNumber == 2) route = new List<Transform> { wps[6], wps[4], 
    wps[7] };
    else if (routeNumber == 3) route = new List<Transform> { wps[8], wps[5], 
    wps[9] };
    else if (routeNumber == 4) route = new List<Transform> { wps[6], wps[10], 
    wps[3] };
     
            //initialise position and waypoint counter 
            transform.position = new Vector3(route[0].position.x, 0.0f, 
    route[0].position.z); 
            targetWP = 1; 
    }

    void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.name == "Pedestrian") || (other.gameObject.name == "Car 5"))
        //Object name is the name of the GameObject you want to check for collisions with.
        {
            //What you want to do on trigger enter
            velocity_adjuster = 0f;
            //Debug.Log("Trigger detected");
        }

        if ((other.gameObject.name == "TL1"))
        {
            Debug.Log("Traffic light detected");

            while (t1red.activeSelf == true)
            {
                velocity_adjuster = 0f;
            };
            velocity_adjuster = 8.5f;
        }

        if ((other.gameObject.name == "TL2"))
        {
            Debug.Log("Traffic light detected");

            while (t2red.activeSelf == true)
            {
                velocity_adjuster = 0f;
            };
            velocity_adjuster = 8.5f;
        }

        if ((other.gameObject.name == "TL3"))
        {
            Debug.Log("Traffic light detected");

            while (t3red.activeSelf == true)
            {
                velocity_adjuster = 0f;
            };
            velocity_adjuster = 8.5f;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.name == "Pedestrian") || (other.gameObject.name == "Car 5"))
        //Object name is the name of the GameObject you want to check for collisions with.
        {
            //What you want to do on trigger enter
            velocity_adjuster = 8.5f;
            //Debug.Log("Trigger exited");
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour {
    public Rigidbody2D asteroid;
    public float resetSpeed=0.025f;
    float resetSpeedSqr;
    public SpringJoint2D spring;
    new Collider2D collider2D;
    float initialX;
    float finalX;
    float scrollDistance;
    public Camera cam;
    Vector3 initialCamPosition;
    Vector3 newCamPosition;
    bool firstTime=true;
    float roundedX;
    float roundedY;
    float finalXRounded;
    float initialXRounded;
    float scrollDistanceRounded;
    public Camera stationaryCam;
    public Transform leftMarker;
    public Transform rightMarker;
    public Bird bird;
    int frameCount;
    void Awake () {
        collider2D = asteroid.GetComponent<Collider2D>();
        resetSpeedSqr = resetSpeed * resetSpeed; 
    }
	
	// Update is called once per frame
	void Update () {
        if (!bird.clickedOn)
        {
            if (Input.GetMouseButton(0))
            {
                frameCount++;
                if (firstTime)
                {
                    initialX = stationaryCam.ScreenToWorldPoint(Input.mousePosition).x;
                    initialXRounded = Mathf.Round(initialX);
                    initialCamPosition = cam.transform.position;
                    roundedX = Mathf.Round(initialCamPosition.x);
                    roundedY = Mathf.Round(initialCamPosition.y);
                    initialCamPosition = new Vector3(roundedX, roundedY, initialCamPosition.z);
                    firstTime = false;
                }
                else
                {
                    Debug.Log("Hi");
                    finalX = stationaryCam.ScreenToWorldPoint(Input.mousePosition).x;
                    scrollDistance = initialXRounded - finalX;
                    Debug.Log("scrollDistance " + scrollDistance);
                    newCamPosition = new Vector3(initialCamPosition.x + scrollDistance, initialCamPosition.y, initialCamPosition.z);
                    newCamPosition.x = Mathf.Clamp(newCamPosition.x, leftMarker.position.x, rightMarker.position.x);
                    cam.transform.position = newCamPosition;
                }

            }
            if (!Input.GetMouseButton(0))
            {
                firstTime = true;
                frameCount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
            ResetGame();
        //if(spring == null && asteroid.velocity.sqrMagnitude < resetSpeedSqr)
        //    ResetGame();
    }
    
    //private void OnTriggerExit2D(Collider2D collider)
    //{
    //    if (collider == collider2D)
    //        StartCoroutine("MyCoroutine");


    //}
    //IEnumerator MyCoroutine()
    // {
    //     yield return new WaitForSecondsRealtime(3f);
    //     ResetGame();
    // }
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

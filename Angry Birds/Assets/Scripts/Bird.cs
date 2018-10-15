using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    public float maxStretch;
    float maxStretchSqr;
    public LineRenderer catapultLineBack;
    public LineRenderer catapultLineFront;
    SpringJoint2D spring;
    public Rigidbody2D rightAnchor;
    public Rigidbody2D leftAnchor;
    Rigidbody2D rb;
    public bool clickedOn;
    Ray rayToMouse;
    Vector2 prevVelocity;
    Vector2 initialPosition;
    public bool released;
    CircleCollider2D circleCollider2D;




    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = 1.5f;
        maxStretchSqr = maxStretch * maxStretch;
        catapultLineBack.startWidth=0.3f;
        catapultLineFront.startWidth=0.3f;
        rightAnchor.isKinematic = true;
        leftAnchor.isKinematic = true;
        spring = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rayToMouse = new Ray(leftAnchor.position,Vector3.zero);
        initialPosition = transform.position;

    }
   
    void Start () {
        LineRendererSetUp();
        //spring.distance = 1f;
	}
	
	void Update () {

        if (clickedOn)
            Dragging();
        if (spring != null)
        {
            if (!rb.isKinematic && rb.velocity.sqrMagnitude < prevVelocity.sqrMagnitude)
            {
                Destroy(spring);
                rb.velocity = prevVelocity;
            }
            if (!clickedOn)
                prevVelocity = rb.velocity;
            LineRendererUpdate();
        }
        else
        {
            catapultLineBack.enabled = false;
            catapultLineFront.enabled = false;
        }



    }
    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vectorToMouse = mouseWorldPoint- leftAnchor.transform.position;
        if (vectorToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = vectorToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0;
        transform.position = mouseWorldPoint;
    }

    void LineRendererSetUp()
    {
        catapultLineBack.SetPosition(0, rightAnchor.transform.position);
        catapultLineFront.SetPosition(0, leftAnchor.transform.position);

        //catapultLineBack.sortingLayerName = "Ground";
        //catapultLineFront.sortingLayerName = "Ground";
        //catapultLineBack.sortingOrder = 0;
        //catapultLineFront.sortingOrder = 3;

    }

    void LineRendererUpdate()
    {
        catapultLineBack.SetPosition(1, transform.position);
        catapultLineFront.SetPosition(1, transform.position);
    }
    private void OnMouseDown()
    {

        spring.enabled = false;
        clickedOn = true;
    }
    private void OnMouseUp()
    {
        clickedOn = false;

        if (Vector2.Distance(transform.position, leftAnchor.position) > spring.distance)
        {
            spring.enabled = true;
            rb.isKinematic = false;
            released = true;
            circleCollider2D.radius = 0.3711638f;
        }
        else
            transform.position = initialPosition;
    }
}

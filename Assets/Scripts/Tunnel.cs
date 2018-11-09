using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {
    private float directionOfTravelInDegrees;
    private Vector3 velocity;
    private float halfX;
    private float halfY;
    private GameObject tunnelInFront;
    private Shape shapeCreator;

    private Renderer rendererComponent;

	private void Start ()
    {
        rendererComponent = GetComponent<Renderer>();
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        halfX = -world.x;
        halfY = -world.y;

        shapeCreator = GetComponentInChildren<Shape>();
    }

    private void OnEnable()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    public void Initialize(float directionOfTravel, GameObject tunnel)
    {
        gameObject.SetActive(true);
        directionOfTravelInDegrees = directionOfTravel;
        transform.localScale = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, directionOfTravelInDegrees);
        transform.position = Vector3.zero;
        tunnelInFront = tunnel;

        // set the velocity in the direction of travel
        float radians = Mathf.Deg2Rad * directionOfTravelInDegrees;
        velocity = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }

    public void UpdateTunnelPosition(float deltaTime)
    {
        // use the speed to shift tunnel, velocity is held in game constants
        transform.position += (velocity.normalized * Time.deltaTime);

        // scale the tunnel
        float newScale = GameConstants.scalingValue * transform.position.magnitude;
        transform.localScale = new Vector3(newScale, newScale, 0);
    }

    public void DrawRoad()
    {
        if (tunnelInFront == null) 
        {
            return;
        }

        Vector2[] shapeVertices = new Vector2[4];

        shapeVertices[0] = transform.TransformPoint(transform.Find("RightSide").position);
        shapeVertices[1] = transform.TransformPoint(transform.Find("LeftSide").position);
        shapeVertices[2] = transform.TransformPoint(tunnelInFront.transform.Find("LeftSide").position);
        shapeVertices[3] = transform.TransformPoint(tunnelInFront.transform.Find("RightSide").position);

        shapeCreator.DrawShape(shapeVertices);
    }

    //private void OnDrawGizmos()
    //{
    //    // set up the vertices to create the road
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.Find("LeftSide").position, .05f);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.Find("RightSide").position, .05f);

    //    if (tunnelInFront != null)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(tunnelInFront.transform.Find("LeftSide").position, .01f);
    //    }
    //}

    public bool IsTunnelOutOfView()
    {
        if (transform.position.x > halfX || transform.position.x < -halfX)
        {
            return true;
        }

        if (transform.position.y > halfY || transform.position.y < -halfY)
        {
            return true;
        }

        return false;
    }

    private float DistanceFromCenter()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2));
    }
}

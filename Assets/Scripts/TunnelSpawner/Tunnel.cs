using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {
    private float directionOfTravelInDegrees;
    private Vector3 velocity;
    private GameObject tunnelInFront;
    private GameObject tunnelBehind;
    private Shape shapeCreator;

    private Renderer rendererComponent;

	private void Start ()
    {
        rendererComponent = GetComponent<Renderer>();
        rendererComponent.enabled = false;
        shapeCreator = GetComponentInChildren<Shape>();
    }

    private void OnEnable()
    {
        rendererComponent = GetComponent<Renderer>();
        rendererComponent.enabled = false;
    }

    public void Initialize(float directionOfTravel, GameObject tunnelFront)
    {
        gameObject.SetActive(true);
        directionOfTravelInDegrees = directionOfTravel;
        transform.localScale = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, directionOfTravelInDegrees);
        transform.position = Vector3.zero;
        tunnelInFront = tunnelFront;

        // set the velocity in the direction of travel
        float radians = Mathf.Deg2Rad * directionOfTravelInDegrees;
        velocity = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }

    public void DeInitialize()
    {
        gameObject.SetActive(false);

        // remove this tunnel from its neighbors
        if (tunnelInFront != null)
        {
            tunnelInFront.GetComponent<Tunnel>().SetTunnelBehind(null);
        }

        if (tunnelBehind != null)
        {
            tunnelBehind.GetComponent<Tunnel>().SetTunnelInFront(null);
        }

        tunnelInFront = null;
        tunnelBehind = null;
    }

    public void SetTunnelInFront(GameObject tunnel)
    {
        tunnelInFront = tunnel;
    }

    public GameObject GetTunnelInFront()
    {
        return tunnelInFront;
    }

    public void SetTunnelBehind(GameObject tunnel)
    {
        tunnelBehind = tunnel;
    }

    public GameObject GetTunnelBehind()
    {
        return tunnelBehind;
    }

    public Vector3 GetLeftSide()
    {
        return transform.Find("LeftSide").position;
    } 

    public Vector3 GetRightSide()
    {
        return transform.Find("RightSide").position;
    }

    public void UpdateTunnelPosition(float deltaTime)
    {
        // use the speed to shift tunnel, velocity is held in game constants
        transform.position += (velocity.normalized * Time.deltaTime *LevelSelectData._levelSelect._tunnelVelocity);

        // scale the tunnel

        float newScale = transform.position.magnitude * GameConstants.scalingValue;
        transform.localScale = new Vector3(newScale, newScale, 0);
    }

    public void DrawRoad()
    {
        if (tunnelInFront == null) 
        {
            return;
        }

        Vector2[] shapeVertices = new Vector2[4];

        // make the positions relative to world space
        shapeVertices[0] = transform.TransformPoint(transform.Find("RightSide").position);
        shapeVertices[1] = transform.TransformPoint(transform.Find("LeftSide").position);
        shapeVertices[2] = transform.TransformPoint(tunnelInFront.transform.Find("LeftSide").position);
        shapeVertices[3] = transform.TransformPoint(tunnelInFront.transform.Find("RightSide").position);

        shapeCreator.DrawShape(shapeVertices);
    }

    public bool IsTunnelOutOfBounds()
    {
        // tunnel must be outside of the game area but must also be out of view
        // if we just do isVisible, we run into the problem where the component is sometimes
        // small enough that the method will return true
        return DistanceFromCenter() > GameConstants.outOfBoundsValue;
    }

    public bool NeighboringTunnelsOutOfView()
    {
        if (tunnelInFront != null && !tunnelInFront.GetComponent<Tunnel>().IsTunnelOutOfBounds())
        {
            return false;
        }

        if (tunnelBehind != null && !tunnelBehind.GetComponent<Tunnel>().IsTunnelOutOfBounds())
        {
            return false;
        }

        return true;
    }

    private float DistanceFromCenter()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2));
    }
}

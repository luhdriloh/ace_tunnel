using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {
    private float directionOfTravelInDegrees;
    private Vector3 velocity;
    private float halfX;
    private float halfY;

    private Renderer rendererComponent;

	private void Start ()
    {
        rendererComponent = GetComponent<Renderer>();
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        halfX = -world.x;
        halfY = -world.y;
    }

    private void OnEnable()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    public void Initialize(float directionOfTravel)
    {
        gameObject.SetActive(true);
        directionOfTravelInDegrees = directionOfTravel;
        transform.localScale = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0, 0, directionOfTravelInDegrees);
        transform.position = Vector3.zero;

        // set the velocity in the direction of travel
        float radians = Mathf.Deg2Rad * directionOfTravelInDegrees;
        velocity = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }

    public void updateTunnelPosition(float deltaTime)
    {
        //if (IsTunnelOutOfView()) {
        //    gameObject.SetActive(false);
        //    return;
        //}

        // use the speed to shift tunnel, velocity is held in game constants
        transform.position += (velocity.normalized * Time.deltaTime);

        // scale the tunnel
        float newScale = GameConstants.scalingValue * transform.position.magnitude;
        transform.localScale = new Vector3(newScale, newScale, 0);
    }

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

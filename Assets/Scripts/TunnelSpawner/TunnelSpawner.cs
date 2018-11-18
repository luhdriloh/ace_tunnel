using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TunnelSpawner : MonoBehaviour 
{
    public GameObject tunnelObjectPrototype;

    // objects to keep track of the tunnels that we spawn
    private Queue<GameObject> tunnelObjectsNotInUse;
    private Queue<GameObject> tunnelObjectsInUse;

    // scripts that this gameobject will use
    private Shape shapeCreator;
    private MovementCreation movement;

    // the edge collider that we will put onto the path created
    private EdgeCollider2D edgeCollider;

    // the current position in angles, and the tunnel in the back
    private float currentPathPosition;
    private GameObject tunnelInBack;

    private void Start ()
    {
        tunnelObjectsNotInUse = new Queue<GameObject>();
        tunnelObjectsInUse = new Queue<GameObject>();

        // get the components that we need
        edgeCollider = GetComponent<EdgeCollider2D>();

        shapeCreator = GetComponentInChildren<Shape>();
        movement = new MovementCreation();

        // instantiate our tunnel objects 
        for (int i = 0; i < GameConstants.numberOfTunnelBuffers; i++)
        {
            tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
        }

        currentPathPosition = 90;
        tunnelInBack = null;

        CreateStartTunnels();
        CreateNextTunnel();
	}
	

	private void Update()
    {
        // we will need to draw the line from each to tunnel to the next
        // update each tunnel in use
        float deltatime = Time.deltaTime;

        // update collider and draw the tunnels
        UpdateTunnelPositions(deltatime);
        RemoveOutOfBoundsTunnels();

        Vector2[] vertices = GetTunnelPoints();
        DrawTunnels(vertices);
        UpdateColliders(vertices);
    }


    /// <summary>
    /// Updates the tunnel positions given the delta time.
    /// </summary>
    /// <param name="deltaTime">The delta from the last frame</param>
    private void UpdateTunnelPositions(float deltatime)
    {
        // update all the tunnels
        foreach (GameObject tunnelObject in tunnelObjectsInUse)
        {
            Tunnel tunnel = tunnelObject.GetComponent<Tunnel>();
            tunnel.UpdateTunnelPosition(deltatime);
        }
    }


    /// <summary>
    /// Removes tunnels that are out of bounds.
    /// </summary>
    private void RemoveOutOfBoundsTunnels()
    {
        Tunnel tunnel = tunnelObjectsInUse.Peek().GetComponent<Tunnel>();
        bool outOfBounds = tunnel.IsTunnelOutOfBounds();

        // make sure that the tunnel is actually out of bounds

        while (outOfBounds)
        {
            // get the tunnel that is out of bounds and deinitialize it
            GameObject tunnelThatIsOutOfBounds = tunnelObjectsInUse.Dequeue();
            tunnel.DeInitialize();

            tunnelObjectsNotInUse.Enqueue(tunnelThatIsOutOfBounds);

            // set the new tunnels up
            tunnel = tunnelObjectsInUse.Peek().GetComponent<Tunnel>();
            outOfBounds = tunnel == null ? false : tunnel.IsTunnelOutOfBounds();
        }
    }

    /// <summary>
    /// Get all the tunnel paths and connect them together to create a road
    /// </summary>
    private void DrawTunnels(Vector2[] vertices)
    {
        shapeCreator.DrawShape(vertices);
    }

    /// <summary>
    /// Updates our edge colliders with the edges of our path
    /// </summary>
    private void UpdateColliders(Vector2[] vertices)
    {
        edgeCollider.points = vertices;
    }

    /// <summary>
    /// Return a vector of points that specifies the path created
    /// </summary>
    /// <returns>The tunnel points.</returns>
    private Vector2[] GetTunnelPoints()
    {
        GameObject tunnel = tunnelInBack;
        Tunnel tunnelComponent = tunnel != null ? tunnel.GetComponent<Tunnel>() : null;
        List<Vector2> leftSide = new List<Vector2>();
        List<Vector2> rightSide = new List<Vector2>();

        // if tunnel is not null and if tunnel is still in view
        while (tunnel != null && !tunnelComponent.IsTunnelOutOfBounds())
        {
            leftSide.Add(tunnelComponent.GetLeftSide());
            rightSide.Add(tunnelComponent.GetRightSide());

            tunnel = tunnelComponent.GetTunnelInFront();
            tunnelComponent = tunnel != null ? tunnel.GetComponent<Tunnel>() : null;
        }

        // create an array of vertices, we will these all together
        rightSide.Reverse();
        leftSide.AddRange(rightSide);

        return leftSide.ToArray();
    }

    /// <summary>
    /// Creates the starting tunnels so that the player starts within tunnels
    /// </summary>
    private void CreateStartTunnels()
    {
        // we want to create tunnels all the way to the out of bounds values
        float secondsForTunnelToGetOutOfBounds = GameConstants.outOfBoundsValue / GameConstants.tunnelVelocity;
        int numberOfTunnelsToCreate = (int)Mathf.Ceil(secondsForTunnelToGetOutOfBounds / GameConstants.tunnelSpawnConstantNormal);
        float yDelta = GameConstants.outOfBoundsValue / numberOfTunnelsToCreate;

        for (int i = numberOfTunnelsToCreate - 1; i > 0; i--)
        {
            GameObject tunnelAdded = AddTunnelToEnd();
            tunnelAdded.transform.position += new Vector3(0f, i * yDelta, 0);

            float newScale = tunnelAdded.transform.position.magnitude * GameConstants.scalingValue;
            tunnelAdded.transform.localScale = new Vector3(newScale, newScale, 0);
        }
    }

    /// <summary>
    /// Creates the next tunnel. This is called at a specified time interval. The
    ///     smaller the time interval the smoother our road will be as it has more
    ///     points, thus higher resolution
    /// </summary>
    private void CreateNextTunnel()
    {
        // retrieve the next tunnel to use
        if (tunnelObjectsNotInUse.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
            }
        }

        UpdateTunnelDirection();
        AddTunnelToEnd();
        Invoke("CreateNextTunnel", GameConstants.tunnelSpawnConstantNormal);
    }

    private GameObject AddTunnelToEnd()
    {
        GameObject newTunnelInTheBack = tunnelObjectsNotInUse.Dequeue();
        tunnelObjectsInUse.Enqueue(newTunnelInTheBack);

        if (tunnelInBack != null)
        {
            tunnelInBack.GetComponent<Tunnel>().SetTunnelBehind(newTunnelInTheBack);
        }

        // initialize tunnel with position and previous tunnel, update lastCreatedTunnel
        newTunnelInTheBack.GetComponent<Tunnel>().Initialize(currentPathPosition, tunnelInBack);
        tunnelInBack = newTunnelInTheBack;

        return tunnelInBack;
    }


    private void UpdateTunnelDirection()
    {
        // update the position of the new tunnel
        int movementDirection = movement.ReturnDirection();
        currentPathPosition += (GameConstants.anglesPerSecond * GameConstants.tunnelVelocity * movementDirection) * GameConstants.tunnelSpawnConstantNormal;
    }
}

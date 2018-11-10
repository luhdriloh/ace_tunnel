using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TunnelSpawner : MonoBehaviour 
{
    public Queue<GameObject> tunnelObjectsNotInUse;
    public List<GameObject> tunnelObjectsInUse;
    public GameObject tunnelObjectPrototype;

    private Shape shapeCreator;
    private float currentPathPosition;
    private MovementCreation movement;
    private GameObject tunnelInBack;

    private void Start ()
    {
        currentPathPosition = 90;
        tunnelObjectsNotInUse = new Queue<GameObject>();
        tunnelObjectsInUse = new List<GameObject>();

        for (int i = 0; i < GameConstants.numberOfTunnelBuffers; i++)
        {
            tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
        }

        shapeCreator = GetComponentInChildren<Shape>();
        movement = new MovementCreation();
        tunnelInBack = null;
        CreateNextSetOfTunnels();
	}
	
	private void Update ()
    {
        // we will need to draw the line from each to tunnel to the next
        // update each tunnel in use
        float deltatime = Time.deltaTime;
        List<GameObject> tunnelsToRemove = new List<GameObject>();

        foreach (GameObject tunnelObject in tunnelObjectsInUse)
        {
            Tunnel tunnel = tunnelObject.GetComponent<Tunnel>();

            // check if the tunnel is out of view as well as its neighbors
            if (tunnel.IsTunnelOutOfView() && tunnel.NeighboringTunnelsOutOfView())
            {
                tunnelsToRemove.Add(tunnelObject);
            }

            tunnel.UpdateTunnelPosition(deltatime);
        }

        foreach (GameObject tunnel in tunnelsToRemove)
        {
            // remove the unneeded tunnels from the list
            tunnel.GetComponent<Tunnel>().DeInitialize();
            tunnelObjectsInUse.Remove(tunnel);

            tunnelObjectsNotInUse.Enqueue(tunnel);
        }

        DrawTunnels();

        //foreach (GameObject tunnelObject in tunnelObjectsInUse)
        //{
        //    Tunnel tunnel = tunnelObject.GetComponent<Tunnel>();
        //    tunnel.DrawRoad();
        //}
    }

    private void DrawTunnels()
    {
        // start from the tunnel in the back all the way up until the first out of view tunnel
        // after which continue on with the rest

        GameObject tunnel = tunnelInBack;
        Tunnel tunnelComponent = tunnel != null ? tunnel.GetComponent<Tunnel>() : null;
        List<Vector2> leftSide = new List<Vector2>();
        List<Vector2> rightSide = new List<Vector2>();

        // if tunnel is not null and if tunnel is still in view
        while (tunnel != null && !tunnelComponent.IsTunnelOutOfView())
        {
            leftSide.Add(tunnelComponent.GetLeftSide());
            rightSide.Add(tunnelComponent.GetRightSide());

            tunnel = tunnelComponent.GetTunnelInFront();
            tunnelComponent = tunnel != null ? tunnel.GetComponent<Tunnel>() : null;
        }

        // create an array of vertices, we will these all together
        rightSide.Reverse();
        leftSide.AddRange(rightSide);

        shapeCreator.DrawShape(leftSide.ToArray());
    }

    private void CreateNextSetOfTunnels()
    {
        // retrieve the next tunnel to use
        if (tunnelObjectsNotInUse.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
            }
        }

        GameObject newTunnelInTheBack = tunnelObjectsNotInUse.Dequeue();
        tunnelObjectsInUse.Add(newTunnelInTheBack);

        // update the position of the new tunnel
        int movementDirection = movement.ReturnDirection();
        currentPathPosition += (GameConstants.anglesPerSecond * movementDirection) * GameConstants.tunnelSpawnConstantNormal;

       
        if (tunnelInBack != null)
        {
            tunnelInBack.GetComponent<Tunnel>().SetTunnelBehind(newTunnelInTheBack);
        }

        // initialize tunnel with position and previous tunnel, update lastCreatedTunnel
        newTunnelInTheBack.GetComponent<Tunnel>().Initialize(currentPathPosition, tunnelInBack);
        tunnelInBack = newTunnelInTheBack;

        Invoke("CreateNextSetOfTunnels", GameConstants.tunnelSpawnConstantNormal);
    }
}

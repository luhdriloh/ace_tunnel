using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Path : MonoBehaviour 
{
    public Queue<GameObject> tunnelObjectsNotInUse;
    public List<GameObject> tunnelObjectsInUse;
    public GameObject tunnelObjectPrototype;


    // what if we keep track of the last spawned tunnel then we can attach to
    //    it the newly created one, if the path created is a turning point then
    //    don't change its angle


    // use the midpoint formula to create our tunnel object
    // we will keep track of the next 5 seconds worth of tunnel
    // at 4 seconds we will create the next 4 seconds and so on and so forth
    private float currentPathPosition;
    private Movement movement;
    private GameObject lastCreatedTunnel;

    private void Start ()
    {
        currentPathPosition = 90;
        tunnelObjectsNotInUse = new Queue<GameObject>();

        for (int i = 0; i < GameConstants.numberOfTunnelBuffers; i++)
        {
            tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
        }

        movement = new Movement();
        lastCreatedTunnel = null;
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

            // check if the tunnel is out of view
            if (tunnel.IsTunnelOutOfView())
            {
                tunnelsToRemove.Add(tunnelObject);
            }

            tunnel.UpdateTunnelPosition(deltatime);
        }

        foreach (GameObject tunnelObject in tunnelObjectsInUse)
        {
            Tunnel tunnel = tunnelObject.GetComponent<Tunnel>();
            tunnel.DrawRoad();
        }

        foreach (GameObject tunnelToRemove in tunnelsToRemove)
        {
            tunnelObjectsInUse.Remove(tunnelToRemove);
            tunnelToRemove.gameObject.SetActive(false);
            tunnelObjectsNotInUse.Enqueue(tunnelToRemove);
        }
    }

    private void CreateNextSetOfTunnels()
    {
        // retrieve the next tunnel to use
        GameObject newTunnel = tunnelObjectsNotInUse.Dequeue();
        tunnelObjectsInUse.Add(newTunnel);

        // update the position of the new tunnel
        int movementDirection = movement.ReturnDirection();
        currentPathPosition += (GameConstants.anglesPerSecond * movementDirection) * GameConstants.tunnelSpawnConstantNormal;

        // initialize tunnel with position and previous tunnel, update lastCreatedTunnel
        GameObject aRef = lastCreatedTunnel;
        newTunnel.GetComponent<Tunnel>().Initialize(currentPathPosition, aRef);
        lastCreatedTunnel = newTunnel;

        Invoke("CreateNextSetOfTunnels", GameConstants.tunnelSpawnConstantNormal);
    }

    private void connectTunnels()
    {

    }


}

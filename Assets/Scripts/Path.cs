using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Path : MonoBehaviour 
{
    public Queue<GameObject> tunnelObjectsNotInUse;
    public List<GameObject> tunnelObjectsInUse;
    public GameObject tunnelObjectPrototype;

    // what if we keep track of the last spawned tunnel then we can attach to
    //    it the newly created one
    

    // ***** the best way to create the paths is simply to go left or right
    // we simply need to know the points at which to stop or at which to create 

    // use the midpoint formula to create our tunnel object
    // we will keep track of the next 5 seconds worth of tunnel
    // at 4 seconds we will create the next 4 seconds and so on and so forth
    private float currentPathPosition;
    private Movement movement;

    private void Start ()
    {
        currentPathPosition = 90;
        tunnelObjectsNotInUse = new Queue<GameObject>();

        for (int i = 0; i < GameConstants.numberOfTunnelBuffers; i++)
        {
            tunnelObjectsNotInUse.Enqueue(Instantiate(tunnelObjectPrototype, transform.position + new Vector3(0, -10.0f, -10.0f), Quaternion.identity));
        }

        movement = new Movement();
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

            tunnel.updateTunnelPosition(deltatime);
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
        tunnelObjectsInUse.Add(tunnelObjectsNotInUse.Dequeue());

        int movementDirection = movement.ReturnDirection();
        currentPathPosition += (GameConstants.anglesPerSecond * movementDirection) * GameConstants.tunnelSpawnConstantNormal;


        tunnelObjectsInUse[tunnelObjectsInUse.Count - 1].GetComponent<Tunnel>().Initialize(currentPathPosition);
        Invoke("CreateNextSetOfTunnels", GameConstants.tunnelSpawnConstantNormal);
    }

    private void connectTunnels()
    {

    }

    private void SetNextTurnPoint()
    {

    }
}

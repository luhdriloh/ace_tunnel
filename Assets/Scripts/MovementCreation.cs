using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TravelDirection { Left=-1, Straight=0, Right=1 };

public class MovementCreation
{
    private TravelDirection m_direction;
    private int m_timeToTravelInDirection;

    public MovementCreation()
    {
        m_direction = TravelDirection.Straight;
        m_timeToTravelInDirection = 8;
    }

    public MovementCreation(TravelDirection direction, int timeToTravelInDirection)
    {
        m_direction = TravelDirection.Straight;
        m_timeToTravelInDirection = timeToTravelInDirection;
    }

    public int ReturnDirection()
    {
        int directionToReturn = (int)m_direction;

        m_timeToTravelInDirection--;
        if (m_timeToTravelInDirection == 0)
        {
            ChangeMovement();
        }

        return directionToReturn;
    }

    private void ChangeMovement()
    {
        float newMovementType = Random.Range(0, 10);

        if (newMovementType <= 1)
        {
            m_direction = TravelDirection.Straight;
        }
        else if (newMovementType <= 5)
        {
            m_direction = TravelDirection.Left;
        }
        else
        {
            m_direction = TravelDirection.Right;
        }

        float secondsToMoveInDirection = Random.Range(GameConstants.tunnelSpawnConstantNormal, 4f);
        m_timeToTravelInDirection = (int)(secondsToMoveInDirection / GameConstants.tunnelSpawnConstantNormal);
    }
}

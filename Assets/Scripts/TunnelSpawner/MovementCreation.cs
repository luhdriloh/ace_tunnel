using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TravelDirection { Left=-1, Straight=0, Right=1 };

public class MovementCreation
{
    private System.Random _random;
    private TravelDirection _direction;
    private int _timeToTravelInDirection;

    public MovementCreation()
    {
        _random = new System.Random(GameConstants.randomSeed);
        _direction = TravelDirection.Straight;
        _timeToTravelInDirection = 8;
    }

    public MovementCreation(TravelDirection direction, int timeToTravelInDirection)
    {
        _direction = TravelDirection.Straight;
        _timeToTravelInDirection = timeToTravelInDirection;
    }

    public int ReturnDirection()
    {
        int directionToReturn = (int)_direction;

        _timeToTravelInDirection--;
        if (_timeToTravelInDirection == 0)
        {
            ChangeMovement();
        }

        return directionToReturn;
    }

    private void ChangeMovement()
    {
        float newMovementType = Mathf.FloorToInt((float)_random.NextDouble() * 10);

        if (newMovementType <= 1)
        {
            _direction = TravelDirection.Straight;
        }
        else if (newMovementType <= 5)
        {
            _direction = TravelDirection.Left;
        }
        else
        {
            _direction = TravelDirection.Right;
        }

        float secondsToMoveInDirection = Random.Range(2f, 4f);
        _timeToTravelInDirection = (int)(secondsToMoveInDirection / GameConstants.tunnelSpawnConstantNormal);
    }
}

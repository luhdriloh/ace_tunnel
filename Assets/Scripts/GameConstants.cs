using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    public static float playerDistanceFromCenter = 4.0f;
    public static float anglesPerSecond = 60;
    public static int minAngleChange = 30;
    public static int maxAngleChange = 720;

    public static int numberOfTunnelBuffers = 100;
    public static float tunnelSpawnConstantNormal = .1f;
    public static float tunnelVelocity = 1.25f;
    public static float scalingValue = .1f;
    public static float outOfBoundsValue = 5.2f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShipDataSO : ScriptableObject
{
    public float pitchSpeed;
    public float rollSpeed;
    public float yawSpeed;
    public float flySpeed;

    public void StartNewGame()
    {
        pitchSpeed = 50;
        rollSpeed = 50;
        yawSpeed = 50;
        flySpeed = 2;
    }
}

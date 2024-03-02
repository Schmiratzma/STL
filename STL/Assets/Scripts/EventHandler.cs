using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EventHandler
{
    public delegate void shipPositionDelegate(Vector3 position);
    public static event shipPositionDelegate UpdateChunkEvent;

    public static void OnUpdateChunk(Vector3 position)
    {
        UpdateChunkEvent?.Invoke(position);
    }

}

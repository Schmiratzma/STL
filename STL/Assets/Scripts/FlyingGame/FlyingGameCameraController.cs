using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGameCameraController : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] Vector3 offset;

    [SerializeField] float speed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, follow.position, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, follow.rotation, speed);
    }
}

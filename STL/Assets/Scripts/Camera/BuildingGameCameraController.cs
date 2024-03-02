using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BuildingGameCameraController: MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;

    [SerializeField] InputActionReference moveCameraIAR;

    [SerializeField] InputActionReference rotateCameraIAR;

    [SerializeField] InputActionReference zoomCameraIAR;

    [SerializeField] float moveSpeed;

    [SerializeField] float rotateSpeed;

    [SerializeField] float zoomSpeed;

    [SerializeField] Vector2 xBounds;
    [SerializeField] Vector2 zBounds;

    Camera cam;
    Plane groundPlane;

    Coroutine moveCamCoroutine;
    Coroutine rotateCamCoroutine;
    Coroutine zoomCamCoroutine;



    // Start is called before the first frame update
    void Start()
    {
        groundPlane = new Plane(Vector3.up,Vector3.zero);
        cam = GetComponentInChildren<Camera>();

        inputActionAsset.Enable();

        moveCameraIAR.action.performed += MoveCamera;
        rotateCameraIAR.action.performed += RotateCamera;
        zoomCameraIAR.action.performed += ZoomCamera;
    }

    private void OnDestroy()
    {
        moveCameraIAR.action.performed -= MoveCamera;
        rotateCameraIAR.action.performed -= RotateCamera;
        zoomCameraIAR.action.performed -= ZoomCamera;
    }

    private void ZoomCamera(InputAction.CallbackContext context)
    {
        zoomCamCoroutine = StartCoroutine(zoomCameraCoroutine());
    }

    private void RotateCamera(InputAction.CallbackContext context)
    {
        rotateCamCoroutine = StartCoroutine(rotateCameraCoroutine());
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        moveCamCoroutine = StartCoroutine(moveCameraCoroutine());
    }

    public IEnumerator moveCameraCoroutine()
    {
        Vector2 previousPointerPos = Pointer.current.position.ReadValue();
        while (moveCameraIAR.action.IsPressed())
        {
            Ray pointerWorldRay = cam.ScreenPointToRay(previousPointerPos);
            groundPlane.Raycast(pointerWorldRay, out float enterDistance);
            Vector3 previousPointerWorldPos = pointerWorldRay.GetPoint(enterDistance);

            Vector2 currentPointerPos = Pointer.current.position.ReadValue();
            pointerWorldRay = cam.ScreenPointToRay(currentPointerPos);
            groundPlane.Raycast(pointerWorldRay, out enterDistance);
            Vector3 currentPointerWorldPos = pointerWorldRay.GetPoint(enterDistance);

            Vector3 delta = previousPointerWorldPos - currentPointerWorldPos;

            transform.position += delta;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xBounds.x, xBounds.y), transform.position.y, Mathf.Clamp(transform.position.z, zBounds.x, zBounds.y));
            previousPointerPos = currentPointerPos;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator rotateCameraCoroutine()
    {
        Vector2 previousPointerPos = Pointer.current.position.ReadValue();
        while (rotateCameraIAR.action.IsPressed())
        {
            Vector2 currentPointerPos = Pointer.current.position.ReadValue();

            float xDelta = (previousPointerPos.x - currentPointerPos.x) / Screen.width;

            previousPointerPos = currentPointerPos;

            transform.rotation *= Quaternion.Euler(0, -1 * xDelta * rotateSpeed, 0);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator zoomCameraCoroutine()
    {
        while (zoomCameraIAR.action.IsPressed())
        {

            yield return null;
        }
        yield return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitControler : MonoBehaviour
{
    Plane floorPlane = new Plane(Vector3.up, Vector3.zero);
    public static UnitControler Instance;

    [SerializeField] InputActionAsset inputAction;

    [SerializeField] InputActionReference moveIAR;

    public event Action<Vector3> onUnitsCommanded;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputAction.Enable();

        moveIAR.action.canceled += CommandUnits;
    }

    private void CommandUnits(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());

        floorPlane.Raycast(ray, out float enterDistance);

        Vector3 hit = ray.GetPoint(enterDistance);

        Vector2Int targetGridCoord = GridUtil.WorlPosToGridCoord(hit,GridManager.Instance.tilePrefab.tileDimensions);

        hit = GridUtil.GridCoordToWorldPos(targetGridCoord, GridManager.Instance.tilePrefab.tileDimensions);

        onUnitsCommanded?.Invoke(hit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

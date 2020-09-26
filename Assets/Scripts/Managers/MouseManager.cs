using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ClickAction { None, PutDebugSphere };

public class MouseManager : MonoBehaviour
{
    private ClickAction _currentAction;

    public GameObject debugSphere;
    public GameObject hoverGridCube;

    public static MouseManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _currentAction = ClickAction.PutDebugSphere;
    }

    // Update is called once per frame
    void Update()
    {
        ManageMouseHover();
        ManageMouseClick();
    }

    private void ManageMouseHover()
    {
        switch(_currentAction)
        {
            case ClickAction.None:
                break;
            case ClickAction.PutDebugSphere:
                ManageGridHover();
                break;
        }
    }

    private void ManageMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(_currentAction)
            {
                case ClickAction.None:
                    break;
                case ClickAction.PutDebugSphere:
                    PutDebugSphere();
                    break;
            }
        }
    }

    private Vector3 GetWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void PutDebugSphere()
    {
        Vector3 mousePos = GetWorldPosition();
        Vector2Int gridPos = GridManager.Instance.GetPositionOnGrid(mousePos);
        if (GridManager.Instance.CanAddElementAtPos(gridPos))
        {
            GameObject newObj = Instantiate(debugSphere, Vector3.zero, Quaternion.identity, transform);
            newObj.transform.position = GridManager.Instance.GetSnappedPosition(mousePos);
            GridManager.Instance.AddElementAtPos(gridPos, newObj);
        }
    }

    private void ManageGridHover()
    {
        Vector3 mousePos = GetWorldPosition();
        Vector3 snappedPos = GridManager.Instance.GetSnappedPosition(mousePos);
        Debug.Log(mousePos + " - " + GridManager.Instance.GetPositionOnGrid(snappedPos));
        hoverGridCube.transform.position = snappedPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPutObjects : MonoBehaviour
{
    public GameObject objectToInstantiateOnClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(objectToInstantiateOnClick != null)
            {
                GameObject newObject = Instantiate(objectToInstantiateOnClick, Vector3.zero, Quaternion.identity, transform);
                newObject.transform.localScale = Vector3.one * 5;
                newObject.transform.position = GetSnappedPosition(GetWorldPosition());
            }
        }
    }

    private Vector3 GetWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("hit");
            return hit.point;
        }
        return Vector3.zero;
    }

    private Vector3 GetSnappedPosition(Vector3 position)
    {
        float gridSize = 10.0f;
        float halfGridSize = gridSize * 0.5f;
        Vector3 newPos = new Vector3(Mathf.Floor(position.x / gridSize) * gridSize + halfGridSize, position.y, Mathf.Floor(position.z / gridSize) * gridSize + halfGridSize);
        return newPos;
    }
}

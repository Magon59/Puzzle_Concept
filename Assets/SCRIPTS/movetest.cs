using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest : MonoBehaviour
{
    private Vector3 currentCell;
    private Vector3 targetCell;

    private float cellSizeX = 1;
    private float cellSizeZ = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetCell = currentCell + Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            targetCell = currentCell + Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            targetCell = currentCell + Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetCell = currentCell + Vector3.right;
        }

        Vector3 targetPosition = CalculateWorldPosition(targetCell.x, targetCell.z); // Convertir les coordonnées de la case en position réelle dans le monde
        GetComponent<Rigidbody>().MovePosition(targetPosition);

        currentCell = targetCell;

    }

    private Vector3 CalculateWorldPosition(float x, float z)
    {
        // Calculer la position réelle dans le monde en utilisant les coordonnées x et z de la case
        Vector3 worldPosition = new Vector3(x * cellSizeX, 0f, z * cellSizeZ);
        return worldPosition;
    }

}

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

    private bool isMoving = false;
    private float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            // Calculer la direction du mouvement
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Calculer la case cible en utilisant la direction du mouvement
            targetCell = currentCell + movementDirection;

            // Effectuer le d�placement vers la case cible si elle est valide
            if (IsCellValid(targetCell))
            {
                StartCoroutine(MoveToTarget());
            }
        }
    }

    // Coroutine pour effectuer le d�placement case par case
    IEnumerator MoveToTarget()
    {
        isMoving = true;

        Vector3 targetPosition = CalculateWorldPosition(targetCell.x, targetCell.z);
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        currentCell = targetCell;
        isMoving = false;
    }


    private Vector3 CalculateWorldPosition(float x, float z)
    {
        // Calculer la position r�elle dans le monde en utilisant les coordonn�es x et z de la case
        Vector3 worldPosition = new Vector3(x * cellSizeX, 0f, z * cellSizeZ);
        return worldPosition;
    }

    // V�rifie si la case cible est valide (peut �tre utilis�e pour le d�placement)
    private bool IsCellValid(Vector3 cell)
    {
        // Ici, vous pouvez ajouter votre propre logique de validation des cases
        // V�rifiez si la case est accessible, si elle est libre, etc.
        // Par exemple, vous pouvez v�rifier si la case existe dans votre grille,
        // si elle n'est pas occup�e par un autre objet, etc.

        // Retourne true si la case est valide, sinon retourne false
        return true; // � adapter selon votre logique de jeu
    }

    // Convertit les coordonn�es de la case en position r�elle dans le monde


}

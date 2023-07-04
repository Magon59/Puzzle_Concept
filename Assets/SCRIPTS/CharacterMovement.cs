using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement du personnage
    public float cellSize = 1f; // Taille d'une case

    private Vector3 targetPosition; // Position cible du d�placement
    private bool isMoving = false; // Indicateur si le personnage est en mouvement

    private void Update()
    {
        if (isMoving)
        {
            // Si le personnage est en mouvement, on met � jour sa position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Si la position cible est atteinte, on arr�te le mouvement
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        else
        {
            // Si le personnage n'est pas en mouvement, on d�tecte l'entr�e de d�placement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if ((horizontalInput != 0f && verticalInput == 0f) || (horizontalInput == 0f && verticalInput != 0f))
            {
                // Le personnage se d�place uniquement horizontalement ou verticalement, pas en diagonale
                // On calcule la position cible du d�placement
                Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                targetPosition = transform.position + movementDirection * cellSize;

                // On v�rifie si la position cible est valide
                if (IsPositionValid(targetPosition))
                {
                    // On d�marre le mouvement vers la position cible
                    isMoving = true;
                }
            }
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f); // On r�cup�re les objets � proximit� de la position

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacles"))
            {
                // Si un objet avec le tag "Obstacles" est pr�sent � la position, le d�placement est invalide
                return false;
            }

            if (collider.CompareTag("ObjetToPush"))
            {
                // Si un objet avec le tag "ObjetToPush" est pr�sent � la position, le d�placement est valide
                // Vous pouvez ajouter ici la logique pour pousser l'objet si n�cessaire

                // V�rifions si l'objet peut �tre pouss�
                PushableBox pushableBox = collider.GetComponent<PushableBox>();
                if (pushableBox != null && pushableBox.CanBePushed)
                {
                    // Le box est poussable, nous le d�placerons manuellement
                    Vector3 direction = position - transform.position;
                    Vector3 pushPosition = position + direction;

                    // V�rifions si la position de pouss�e est valide
                    if (IsPositionValid(pushPosition))
                    {
                        // D�pla�ons l'objet � la position de pouss�e
                        pushableBox.MoveToPosition(pushPosition);

                        return true;
                    }
                }

                // Si l'objet ne peut pas �tre pouss� ou la position de pouss�e est invalide, le d�placement est invalide
                return false;
            }
        }

        // Si aucun objet avec les tags "Obstacles" ou "ObjetToPush" n'est pr�sent � la position, le d�placement est valide
        return true;
    }


}


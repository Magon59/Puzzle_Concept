using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du personnage
    public float cellSize = 1f; // Taille d'une case

    private Vector3 targetPosition; // Position cible du déplacement
    private bool isMoving = false; // Indicateur si le personnage est en mouvement

    private void Update()
    {
        if (isMoving)
        {
            // Si le personnage est en mouvement, on met à jour sa position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Si la position cible est atteinte, on arrête le mouvement
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        else
        {
            // Si le personnage n'est pas en mouvement, on détecte l'entrée de déplacement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if ((horizontalInput != 0f && verticalInput == 0f) || (horizontalInput == 0f && verticalInput != 0f))
            {
                // Le personnage se déplace uniquement horizontalement ou verticalement, pas en diagonale
                // On calcule la position cible du déplacement
                Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                targetPosition = transform.position + movementDirection * cellSize;

                // On vérifie si la position cible est valide
                if (IsPositionValid(targetPosition))
                {
                    // On démarre le mouvement vers la position cible
                    isMoving = true;
                }
            }
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f); // On récupère les objets à proximité de la position

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacles"))
            {
                // Si un objet avec le tag "Obstacles" est présent à la position, le déplacement est invalide
                return false;
            }

            if (collider.CompareTag("ObjetToPush"))
            {
                // Si un objet avec le tag "ObjetToPush" est présent à la position, le déplacement est valide
                // Vous pouvez ajouter ici la logique pour pousser l'objet si nécessaire

                // Vérifions si l'objet peut être poussé
                PushableBox pushableBox = collider.GetComponent<PushableBox>();
                if (pushableBox != null && pushableBox.CanBePushed)
                {
                    // Le box est poussable, nous le déplacerons manuellement
                    Vector3 direction = position - transform.position;
                    Vector3 pushPosition = position + direction;

                    // Vérifions si la position de poussée est valide
                    if (IsPositionValid(pushPosition))
                    {
                        // Déplaçons l'objet à la position de poussée
                        pushableBox.MoveToPosition(pushPosition);

                        return true;
                    }
                }

                // Si l'objet ne peut pas être poussé ou la position de poussée est invalide, le déplacement est invalide
                return false;
            }
        }

        // Si aucun objet avec les tags "Obstacles" ou "ObjetToPush" n'est présent à la position, le déplacement est valide
        return true;
    }


}


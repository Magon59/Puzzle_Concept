using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    private bool canBePushed = true;

    public bool CanBePushed
    {
        get { return canBePushed; }
        set { canBePushed = value; }
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        StartCoroutine(MoveSmoothly(newPosition));
    }

    private System.Collections.IEnumerator MoveSmoothly(Vector3 targetPosition)
    {
        float moveSpeed = 5f; // Vitesse de d�placement de l'objet pouss�
        float distanceThreshold = 0.001f; // Seuil de distance pour terminer le d�placement

        while (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}



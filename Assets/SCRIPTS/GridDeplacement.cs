using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDeplacement : MonoBehaviour
{
    private Vector3 startPos, endPos;
    private bool isMoving = false;
    public float MoveTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!isMoving) StartCoroutine(MovePlayer(new Vector3(x, y, 0f)));
    }

    IEnumerator MovePlayer (Vector3 dir)
    {
        isMoving = true;
        float nextMove = 0f;
        startPos = transform.position;
        endPos = startPos + dir;

        while(nextMove < MoveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, nextMove / MoveTime);
            nextMove += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        isMoving = false;
    }
}

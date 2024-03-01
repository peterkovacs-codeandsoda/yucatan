using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField]
    private float movementSpeed = 3f;

    private void Start()
    {
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
    }

    private void HandleStageCleared()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = new();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movementDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementDirection.x += 1;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movementDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movementDirection.y -= 1;
        }

        transform.Translate(movementSpeed * Time.deltaTime * movementDirection.normalized);

        Vector2 currentPositionInBoundary = GameAreaBoundController.Instance.KeepInBound(transform.position);
        if (!currentPositionInBoundary.Equals(transform.position))
        {
            transform.position = currentPositionInBoundary;
        }
    }
}

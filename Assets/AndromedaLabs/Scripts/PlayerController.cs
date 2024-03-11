using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField]
    private float movementSpeed = 3f;
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private Transform projectileParent;

    private bool throwInProgress = false;

    private void Start()
    {
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
        GameEvents.Instance.throwStone.AddListener(HandleThrowStone);
    }

    private void HandleThrowStone()
    {
        bool flipped = GetComponent<SpriteRenderer>().flipX;
        Vector3 projectileOffset = new(0.2714f * (flipped ? -1f : 1f), 0.1938f);
        GameObject projectile = Instantiate(stonePrefab, transform.position + projectileOffset, Quaternion.identity, projectileParent);
        projectile.GetComponent<ProjectileController>().flipped = flipped;
        projectile.GetComponent<ProjectileController>().movementSpeed = movementSpeed * 1.5f;
    }

    private void HandleStageCleared()
    {
        GetComponent<Animator>().enabled = false;
        this.enabled = false;
    }

    private void HandleThrowDone()
    {
        throwInProgress = false;
        print("called throwdone");
    }

    // Update is called once per frame
    void Update()
    {
        if (!throwInProgress && Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetTrigger("throw");
            throwInProgress = true;
        }

        if (!throwInProgress)
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
            if (!movementDirection.Equals(Vector2.zero))
            {
                GetComponent<Animator>().SetBool("walk", true);
                bool directionSign = movementDirection.x < 0;
                GetComponent<SpriteRenderer>().flipX = directionSign;
            }
            else
            {
                GetComponent<Animator>().SetBool("walk", false);
            }

            Vector2 currentPositionInBoundary = GameAreaBoundController.Instance.KeepInBound(transform.position);
            if (!currentPositionInBoundary.Equals(transform.position))
            {
                transform.position = currentPositionInBoundary;
            }
        }

    }
}

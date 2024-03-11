using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool flipped;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        Vector2 movementDirection = transform.right * (flipped ? -1f : 1f);
        transform.Translate(movementSpeed * Time.deltaTime * movementDirection.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}

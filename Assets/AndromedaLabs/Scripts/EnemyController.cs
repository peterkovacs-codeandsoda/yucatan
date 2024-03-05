using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2f;

    void Update()
    {
        Vector3 target = PlayerController.Instance.transform.position;
        Vector2 movementDirection = target - transform.position;
        bool directionSign = movementDirection.x < 0;
        GetComponent<SpriteRenderer>().flipX = directionSign;
        transform.Translate(movementSpeed * Time.deltaTime * movementDirection.normalized);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.spawnCollectible.Invoke(transform.position);
        GameEvents.Instance.triggerEnemyEliminated.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

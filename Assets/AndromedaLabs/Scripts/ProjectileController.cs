using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool flipped;

    private static bool acerolaNoMore = false;

    [SerializeField]
    private GameObject acerolaPrefab;

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
        print(collision.gameObject.layer);
        if (collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
        else if (!acerolaNoMore && collision.gameObject.layer == 12)
        {
            acerolaNoMore = true;
            Instantiate(acerolaPrefab, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
        }
    }

}

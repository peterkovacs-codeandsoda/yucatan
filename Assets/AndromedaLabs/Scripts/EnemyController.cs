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
        transform.Translate(movementSpeed * Time.deltaTime * movementDirection.normalized);
    }
}

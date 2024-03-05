using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField]
    private bool goodCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameEvents.Instance.collectDrop.Invoke(goodCollectible);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void HandleThunderIsOver()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DecreaseMightiness();
        }
    }
}

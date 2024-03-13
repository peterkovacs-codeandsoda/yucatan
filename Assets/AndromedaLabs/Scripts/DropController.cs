using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField]
    private bool goodCollectible;

    [SerializeField]
    private bool acerola;

    private float speed = 3f;

    private void Update()
    {
        if (!acerola)
        {
            Vector3 direction = PlayerController.Instance.transform.position - transform.position;
            transform.Translate(Time.deltaTime * speed * direction.normalized);
            speed += speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (acerola)
            {
                GameEvents.Instance.openAcerolaPanel.Invoke();
            }
            GameEvents.Instance.collectDrop.Invoke(goodCollectible);
            Destroy(gameObject);
        }
    }
}

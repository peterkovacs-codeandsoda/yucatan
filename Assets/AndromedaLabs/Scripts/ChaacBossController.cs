using System.Collections;
using UnityEngine;

public class ChaacBossController : MonoBehaviour
{
    [SerializeField]
    private GameObject thunderPrefab;

    [SerializeField]
    private Transform thunderParent;

    [SerializeField]
    private Transform traversalRoot;

    [SerializeField]
    private float attackDelay = 1f;

    private int selectedIndex = -1;

    private int hp = 1;

    private float thunderRange = 3f;

    private bool invulnerable = false;


    void Start()
    {
        StartCoroutine(Attack());
    }

    private void Teleport()
    {
        Vector2 targetPosition = CalculateNextTarget();
        transform.position = targetPosition;
        GetComponent<Animator>().SetTrigger("spawn");
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("attack");
    }

    private void DoThunders()
    {
        invulnerable = true;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AsyncThunders());
    }

    private void Materialized()
    {
        invulnerable = false;
        GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator AsyncThunders()
    {
        int thunderCount = Random.Range(0, 10);
        for (int i = 0; i < thunderCount; i++)
        {
            /*Vector2 position = new(Random.Range(0f, Camera.main.pixelWidth), Random.Range(0f, Camera.main.pixelHeight));
            position = Camera.main.ScreenToWorldPoint(position);
            */
            Vector2 position = PlayerController.Instance.transform.position + new Vector3(Random.Range(-thunderRange, thunderRange), Random.Range(-thunderRange, thunderRange));
            Instantiate(thunderPrefab, position, Quaternion.identity, thunderParent);
            float spawnDelay = Random.Range(0f, 1f);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector2 CalculateNextTarget()
    {
        int targetMaxCount = traversalRoot.childCount;
        int index = Random.Range(0, targetMaxCount);
        while (selectedIndex == index)
        {
            index = Random.Range(0, targetMaxCount);
        }
        selectedIndex = index;
        return traversalRoot.GetChild(index).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invulnerable)
        {
            hp--;
            GameEvents.Instance.bossHpChanged.Invoke(hp);
            if (hp <= 0)
            {
                GameEvents.Instance.triggerStageCleared.Invoke();
                Destroy(gameObject);
            }
        }
    }
}

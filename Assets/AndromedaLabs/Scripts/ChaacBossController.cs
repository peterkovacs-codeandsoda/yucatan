using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ChaacBossController : MonoBehaviour
{
    [SerializeField]
    private GameObject thunderPrefab;

    [SerializeField]
    private Transform thunderParent;

    [SerializeField]
    private Transform traversalRoot;

    private int selectedIndex = -1;

    [SerializeField]
    private int hp = 20;

    private readonly float thunderRange = 2.5f;

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
        float distance = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        print(distance);
        if (distance < 6f)
        {
            StartCoroutine(HorizontalAttack());
        }

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
            Vector2 position = PlayerController.Instance.transform.position + new Vector3(Random.Range(-thunderRange, thunderRange), Random.Range(-thunderRange, thunderRange));
            Instantiate(thunderPrefab, position, Quaternion.identity, thunderParent);
            float spawnDelay = Random.Range(0f, thunderCount / 10f);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator HorizontalAttack()
    {
        Vector3 triggerPosition = transform.position;
        Vector3 direction = PlayerController.Instance.transform.position - triggerPosition;
        for (int i = 0;i < 5;i++)
        {
            direction = direction.normalized;
            direction = direction * 2 * (i + 1);
            GameObject thunder = Instantiate(thunderPrefab, direction + triggerPosition, Quaternion.identity, thunderParent);
            //thunder.transform.localScale = new(2 / (i + 1f), 2 / (i + 1f));
            yield return new WaitForSeconds(1/ (10f *(i+1f)));
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

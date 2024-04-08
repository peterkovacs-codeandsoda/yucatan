using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private const string acerolaLocalStorageKey = "acerola";
    [SerializeField]
    private float movementSpeed = 3f;
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private Transform projectileParent;

    private bool throwInProgress = false;

    private int mightiness = 10;
    private bool acerolaInTheInventory;
    private bool acerolaActivated = false;
    private bool invincible = false;

    private void Start()
    {
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
        GameEvents.Instance.throwStone.AddListener(HandleThrowStone);
        GameEvents.Instance.openAcerolaPanel.AddListener(HandleAcerolaGathered);
        GameEvents.Instance.activateAcerola.AddListener(HandleAcerolaActivation);
        acerolaInTheInventory = PlayerPrefs.GetInt(acerolaLocalStorageKey, 0) == 1;
    }

    private void HandleAcerolaActivation()
    {
        PlayerPrefs.DeleteKey(acerolaLocalStorageKey);
        PlayerPrefs.Save();
        StartCoroutine(DoInvincibility());
        SwitchShield(true);
    }

    private void SwitchShield(bool state)
    {
        Transform shield = transform.GetChild(0);
        if (shield != null)
        {
            shield.gameObject.SetActive(state);
        }
    }

    private IEnumerator DoInvincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(20f);
        invincible = false;
        SwitchShield(false);
    }

    private void HandleAcerolaGathered()
    {
        acerolaInTheInventory = true;
        PlayerPrefs.SetInt(acerolaLocalStorageKey, 1);
        PlayerPrefs.Save();
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
    }

    public void DecreaseMightiness(int decrement)
    {
        if (!invincible)
        {
            mightiness -= decrement;
            GameEvents.Instance.triggerMightChanged.Invoke(mightiness);
            if (mightiness <= 0)
            {
                GameEvents.Instance.triggerRestartGame.Invoke();
            }
        }
    }

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

        if (acerolaInTheInventory && !acerolaActivated && Input.GetKeyDown(KeyCode.Space))
        {
            acerolaActivated = true;
            GameEvents.Instance.activateAcerola.Invoke();
        }

    }
}

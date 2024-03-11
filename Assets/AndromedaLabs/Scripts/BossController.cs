using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum MovementType
    {
        MOVE, DASH, TELEPORT
    }

    [SerializeField]
    private float movementSpeed = 2f;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private Transform traversalRoot;

    private MovementType selectedMovementType;
    private Vector3 target;
    private int selectedIndex = -1;

    void Start()
    {
        target = CalculateNextTarget();
        selectedMovementType = ChooseNextMovementType();
        //print(target + " - " + selectedMovementType);
    }

    void Update()
    {
        switch (selectedMovementType)
        {
            case MovementType.MOVE:
                Move();
                break;
            case MovementType.DASH:
                Dash();
                break;
            case MovementType.TELEPORT:
                Teleport();
                break;
        }
        if (Vector2.Distance(target, transform.position) < 0.1f)
        {
            target = CalculateNextTarget();
            selectedMovementType = ChooseNextMovementType();
            //print(target + " - " + selectedMovementType);
        }
    }

    private void Teleport()
    {
        //print("teleport");
        transform.position = target;
        selectedMovementType = ChooseNextMovementType();
    }

    private void Dash()
    {
        Vector2 direction = dashSpeed * Time.deltaTime * (target - transform.position).normalized;
        //print("dash: " + direction);
        transform.Translate(direction);
        selectedMovementType = MovementType.MOVE;
    }

    private void Move()
    {
        Vector2 direction = movementSpeed * Time.deltaTime * (target - transform.position).normalized;
        //print("move: " + direction);
        transform.Translate(direction);
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

    private MovementType ChooseNextMovementType()
    {
        int typeIndex = Random.Range(0, 6);
        switch (typeIndex)
        {
            case 0:
            case 1:
            case 2:
                return MovementType.MOVE;
            case 3:
            case 4:
                return MovementType.DASH;
            case 5:
                return MovementType.TELEPORT;
        }
        return MovementType.MOVE;
    }
}

using UnityEngine.Events;

public class GameEvents : Singleton<GameEvents>
{
    public UnityEvent introTextIsOver;

    public UnityEvent triggerEnemySpawning;
}

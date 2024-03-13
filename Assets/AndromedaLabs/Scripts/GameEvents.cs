using UnityEngine;
using UnityEngine.Events;

public class GameEvents : Singleton<GameEvents>
{
    public UnityEvent introTextIsOver;

    public UnityEvent triggerEnemySpawning;

    public UnityEvent triggerEnemyEliminated;

    public UnityEvent triggerStageCleared;

    public UnityEvent<Vector2> spawnCollectible;

    public UnityEvent<bool> collectDrop;

    public UnityEvent triggerNextSpeechEntry;

    public UnityEvent hideNextButton;

    public UnityEvent loadNextScene;

    public UnityEvent throwStone;

    public UnityEvent triggerRestartGame;

    public UnityEvent<int> triggerMightChanged;

    public UnityEvent chaacGodAttack;

    public UnityEvent<int> bossHpChanged;

}

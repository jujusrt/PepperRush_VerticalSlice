using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private LevelTimer levelTimer;

    private void Awake()
    {
        // Lo busca automáticamente en la escena, así no dependes del inspector
        levelTimer = Object.FindFirstObjectByType<LevelTimer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (levelTimer != null)
        {
            levelTimer.PlayerReachedGoal();
        }
    }
}

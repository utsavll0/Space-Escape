using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        //Todo Add sfx and particle effects
        DisableMovement();
        Invoke(nameof(ReloadScene),_delay);
    }

    private void StartNextLevelSequence()
    {
        DisableMovement();
        Invoke(nameof(LoadNextLevel),_delay);
    }

    private void DisableMovement()
    {
        _movement.enabled = false;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void LoadNextLevel()
    {
        var nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _sucessSound;

    private Movement _movement;
    private AudioSource _audioSource;
    private bool _isTransitioning = false;


    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(_isTransitioning) return;
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
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSound);
        _isTransitioning = true;
        DisableMovement();
        Invoke(nameof(ReloadScene),_delay);
    }

    private void StartNextLevelSequence()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_sucessSound);
        _isTransitioning = true;
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

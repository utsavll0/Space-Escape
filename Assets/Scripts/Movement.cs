using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _mainThrust = 100f;
    [SerializeField] private float _rotationThrust = 1f;
    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        }
    }
    
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRocket(_rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRocket(-_rotationThrust);
        }
    }

    private void RotateRocket(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // Freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false; // UnFreeze rotation so physics system can take over
    }
}

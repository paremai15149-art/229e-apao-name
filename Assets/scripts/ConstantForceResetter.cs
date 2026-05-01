using UnityEngine;

public class ConstantForceResetter : MonoBehaviour
{
    [Header("Reset Settings")]
    public float resetAfterSeconds = 3f;

    private Vector2 _startPosition;
    private ConstantForce2D _constantForce;
    private Rigidbody2D _rb;

    private Vector2 _savedForce;       // saves original force
    private Vector2 _savedRelativeForce; // saves original relative force
    private float _timer = 0f;
    private bool _isCounting = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _constantForce = GetComponent<ConstantForce2D>();

        // Save starting values
        _startPosition = transform.position;
        _savedForce = _constantForce.force;
        _savedRelativeForce = _constantForce.relativeForce;
    }

    void Update()
    {
        // Detect if object has moved from start
        if (!_isCounting && (Vector2)transform.position != _startPosition)
        {
            _isCounting = true;
            _timer = 0f;
        }

        if (_isCounting)
        {
            _timer += Time.deltaTime;

            if (_timer >= resetAfterSeconds)
            {
                ResetToStart();
            }
        }
    }

    void ResetToStart()
    {
        // Teleport back to start
        transform.position = _startPosition;

        // Reset velocity so it doesn't keep moving
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        // Restore original constant force
        _constantForce.force = _savedForce;
        _constantForce.relativeForce = _savedRelativeForce;

        // Stop counting
        _isCounting = false;
        _timer = 0f;
    }
}
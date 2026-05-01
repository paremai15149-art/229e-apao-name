using UnityEngine;

public class SlingshotResetter : MonoBehaviour
{
    [Header("Launch Settings")]
    public float forceX = 5f;
    public float forceY = 8f;

    [Header("Reset Settings")]
    public float resetAfterSeconds = 3f;

    private Rigidbody2D _rb;
    private ConstantForce2D _constantForce;

    private Vector2 _startPosition;
    private Vector2 _savedForce;
    private Vector2 _savedRelativeForce;

    private float _timer = 0f;
    private bool _isCounting = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _constantForce = GetComponent<ConstantForce2D>();

        // Save starting values
        _startPosition = transform.position;

        if (_constantForce != null)
        {
            _savedForce = _constantForce.force;
            _savedRelativeForce = _constantForce.relativeForce;
        }

        // Launch on start
        Launch();
    }

    void Update()
    {
        // Start counting once object moves
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
                ResetAndRelaunch();
            }
        }
    }

    void Launch()
    {
        _rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

    void ResetAndRelaunch()
    {
        // Teleport back to start
        transform.position = _startPosition;

        // Reset velocity
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;

        // Restore constant force
        if (_constantForce != null)
        {
            _constantForce.force = _savedForce;
            _constantForce.relativeForce = _savedRelativeForce;
        }

        // Reset timer
        _isCounting = false;
        _timer = 0f;

        // Shoot again
        Launch();
    }
}
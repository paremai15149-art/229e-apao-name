using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform destination;        // Drag the destination object here
    public float cooldown = 1f;          // Prevents instant re-teleport

    private float _cooldownTimer = 0f;
    private bool _onCooldown = false;

    void Update()
    {
        if (_onCooldown)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= cooldown)
            {
                _onCooldown = false;
                _cooldownTimer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_onCooldown) return;
        if (!other.CompareTag("Player")) return;
        if (destination == null)
        {
            Debug.LogWarning("Teleporter has no destination set!");
            return;
        }

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        // Teleport player
        other.transform.position = destination.position;

        // Reset velocity so player doesn't carry momentum
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        _onCooldown = true;
    }
}
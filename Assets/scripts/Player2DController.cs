using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float speed = 5.0f; // ��������㹡���Թ
    public float jumpForce = 450f; // �ç���ⴴ

    private Rigidbody2D _rb; // ���ԡ��
    private float _moveInput; // ��˹���ȷҧ����Թ A = (-1), D = 1 / ����, ���
    private bool _isGrounded; // _isGrounded �����������������??
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = _rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            _moveInput = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0); // �� A, D ��ҧ
        }
        _rb.linearVelocity = new Vector2(_moveInput * speed, _rb.linearVelocity.y); // ��ȷҧ X * �������� // Y ����������������

        if (_moveInput < 0) { _spriteRenderer.flipX = true; }
        else if (_moveInput > 0) { _spriteRenderer.flipX = false; }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded) // �� Space ������ ��� ��ͧ��������
        {
            _rb.AddForce(new Vector2(_rb.linearVelocity.x, jumpForce)); // ���ç᡹ X ��� �����çⴴ᡹ Y
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}

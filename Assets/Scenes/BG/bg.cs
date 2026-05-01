using UnityEngine;

public class BGLoop : MonoBehaviour
{
    public float speed = 200f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.localPosition.x <= -1920) // ขนาดจอ
        {
            transform.localPosition += new Vector3(1920 * 2, 0, 0);
        }
    }
}
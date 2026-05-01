using UnityEngine;

public class win : MonoBehaviour
{
    public GameObject winUI; // ลาก UI ชนะมาใส่ใน Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winUI.SetActive(true); // แสดงหน้าชนะ
            Time.timeScale = 0f;   // หยุดเกม
        }
    }
}
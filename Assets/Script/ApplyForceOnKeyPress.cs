using UnityEngine;

public class ApplyForceAndMoveOnKeyPress : MonoBehaviour
{
    public float upwardForce = 10f; // 向上的力大小
    public float horizontalSpeed = 5f; // 水平移动速度

    private Rigidbody rb;

    void Start()
    {
        // 获取物体上的Rigidbody组件
        rb = GetComponent<Rigidbody>();

        // 检查是否存在Rigidbody组件
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the object.");
        }
    }

    void Update()
    {
        // 检测按键"W"是否被按下
        if (Input.GetKeyDown(KeyCode.W))
        {
            // 施加向上的力
            ApplyUpwardForce();
        }

        // 检测按键"A"是否被按下
        if (Input.GetKey(KeyCode.A))
        {
            // 左移
            MoveLeft();
        }

        // 检测按键"D"是否被按下
        if (Input.GetKey(KeyCode.D))
        {
            // 右移
            MoveRight();
        }
    }

    void ApplyUpwardForce()
    {
        // 检查Rigidbody组件是否存在
        if (rb != null)
        {
            // 施加向上的力
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }

    void MoveLeft()
    {
        // 左移
        transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        // 右移
        transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
    }
}
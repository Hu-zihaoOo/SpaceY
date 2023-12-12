using UnityEngine;

public class ApplyForceAndMoveOnKeyPress : MonoBehaviour
{
    public float upwardForce = 10f; // ���ϵ�����С
    public float horizontalSpeed = 5f; // ˮƽ�ƶ��ٶ�

    private Rigidbody rb;

    void Start()
    {
        // ��ȡ�����ϵ�Rigidbody���
        rb = GetComponent<Rigidbody>();

        // ����Ƿ����Rigidbody���
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the object.");
        }
    }

    void Update()
    {
        // ��ⰴ��"W"�Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.W))
        {
            // ʩ�����ϵ���
            ApplyUpwardForce();
        }

        // ��ⰴ��"A"�Ƿ񱻰���
        if (Input.GetKey(KeyCode.A))
        {
            // ����
            MoveLeft();
        }

        // ��ⰴ��"D"�Ƿ񱻰���
        if (Input.GetKey(KeyCode.D))
        {
            // ����
            MoveRight();
        }
    }

    void ApplyUpwardForce()
    {
        // ���Rigidbody����Ƿ����
        if (rb != null)
        {
            // ʩ�����ϵ���
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }

    void MoveLeft()
    {
        // ����
        transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        // ����
        transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
    }
}
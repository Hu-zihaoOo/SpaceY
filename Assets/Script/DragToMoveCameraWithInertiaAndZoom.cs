using UnityEngine;

public class DragToMoveCameraWithInertiaAndZoom : MonoBehaviour
{
    // ����������ƶ��ٶ�
    public float moveSpeed = 5f;

    // ��������������ٶ�
    public float zoomSpeed = 5f;

    // ����ϵ����ֵԽ������Խǿ
    public float damping = 5f;

    // ���Լ��ٶȣ�ֵԽ�����Խ��
    public float inertiaDeceleration = 10f;

    // ���������ŵľ���
    public float zoomDistance = 5f;

    // ���ڼ�¼��갴��ʱ��λ��
    private Vector3 dragStartPosition;

    // ���ڼ�¼����ͷ�ʱ���ٶ�
    private Vector3 dragVelocity;

    // ��־����Ƿ��ͷ�
    private bool isMouseReleased = true;

    void Update()
    {
        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            // ��¼��갴��ʱ��λ��
            dragStartPosition = Input.mousePosition;
            isMouseReleased = false;
        }

        // ��������ס���϶�
        if (Input.GetMouseButton(0))
        {
            // �����϶��ľ���
            Vector3 dragDelta = Input.mousePosition - dragStartPosition;

            // ������ת��Ϊ�������꣬��������ĳ���
            dragDelta = Camera.main.transform.TransformDirection(dragDelta);
            dragDelta.z = 0f;

            // ��������ƶ���Ŀ��λ��
            Vector3 targetPosition = transform.position - dragDelta * moveSpeed * Time.deltaTime;

            // ʹ�� Mathf.MoveTowards �����𽥼�С�ٶȣ�ʵ������Ч��
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, damping * Time.deltaTime);

            // ������갴�µ�λ�ã��Ա���һ֡�����϶�����
            dragStartPosition = Input.mousePosition;

            // ����ͷű�־��Ϊfalse
            isMouseReleased = false;
        }

        // ����ͷ�
        if (Input.GetMouseButtonUp(0))
        {
            // ��������ͷ�ʱ���ٶ�
            dragVelocity = (Input.mousePosition - dragStartPosition) / (Time.deltaTime* 100);
            dragVelocity.x *= -1f;
            dragVelocity.y *= -1f;
            // ��־������ͷ�
            isMouseReleased = true;
        }

        // ������϶�ʱҲ���ӹ���Ч��
        if (!isMouseReleased && dragVelocity.magnitude > 0.1f)
        {
            dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, inertiaDeceleration * Time.deltaTime);
            transform.position += dragVelocity * Time.deltaTime;
        }

        // ����ͷź��𽥼�С�ٶȣ�ʵ�ֹ���Ч��
        if (isMouseReleased && dragVelocity.magnitude > 0.1f)
        {
            dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, inertiaDeceleration * Time.deltaTime);
            transform.position += dragVelocity * Time.deltaTime;
        }

        // ����������
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scrollWheel * zoomDistance * zoomSpeed);
    }
}
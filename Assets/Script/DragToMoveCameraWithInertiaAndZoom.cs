using UnityEngine;

public class DragToMoveCameraWithInertiaAndZoom : MonoBehaviour
{
    // 控制相机的移动速度
    public float moveSpeed = 5f;

    // 控制相机的缩放速度
    public float zoomSpeed = 5f;

    // 阻尼系数，值越大阻尼越强
    public float damping = 5f;

    // 惯性减速度，值越大减速越快
    public float inertiaDeceleration = 10f;

    // 鼠标滚轮缩放的距离
    public float zoomDistance = 5f;

    // 用于记录鼠标按下时的位置
    private Vector3 dragStartPosition;

    // 用于记录鼠标释放时的速度
    private Vector3 dragVelocity;

    // 标志鼠标是否释放
    private bool isMouseReleased = true;

    void Update()
    {
        // 鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 记录鼠标按下时的位置
            dragStartPosition = Input.mousePosition;
            isMouseReleased = false;
        }

        // 鼠标左键按住并拖动
        if (Input.GetMouseButton(0))
        {
            // 计算拖动的距离
            Vector3 dragDelta = Input.mousePosition - dragStartPosition;

            // 将距离转换为世界坐标，考虑相机的朝向
            dragDelta = Camera.main.transform.TransformDirection(dragDelta);
            dragDelta.z = 0f;

            // 计算相机移动的目标位置
            Vector3 targetPosition = transform.position - dragDelta * moveSpeed * Time.deltaTime;

            // 使用 Mathf.MoveTowards 函数逐渐减小速度，实现阻尼效果
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, damping * Time.deltaTime);

            // 更新鼠标按下的位置，以便下一帧计算拖动距离
            dragStartPosition = Input.mousePosition;

            // 鼠标释放标志设为false
            isMouseReleased = false;
        }

        // 鼠标释放
        if (Input.GetMouseButtonUp(0))
        {
            // 计算鼠标释放时的速度
            dragVelocity = (Input.mousePosition - dragStartPosition) / (Time.deltaTime* 100);
            dragVelocity.x *= -1f;
            dragVelocity.y *= -1f;
            // 标志鼠标已释放
            isMouseReleased = true;
        }

        // 在鼠标拖动时也增加惯性效果
        if (!isMouseReleased && dragVelocity.magnitude > 0.1f)
        {
            dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, inertiaDeceleration * Time.deltaTime);
            transform.position += dragVelocity * Time.deltaTime;
        }

        // 鼠标释放后，逐渐减小速度，实现惯性效果
        if (isMouseReleased && dragVelocity.magnitude > 0.1f)
        {
            dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, inertiaDeceleration * Time.deltaTime);
            transform.position += dragVelocity * Time.deltaTime;
        }

        // 鼠标滚轮缩放
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scrollWheel * zoomDistance * zoomSpeed);
    }
}
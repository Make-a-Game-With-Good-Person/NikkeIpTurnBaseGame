using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewState : ICameraState
{
    Vector2 lastTouchPos;
    public void UpdateState(CameraStateController camera)
    {
#if UNITY_EDITOR
        // �����Ϳ��� ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPos = Input.mousePosition;
            camera.isDragging = true;
        }
        else if (Input.GetMouseButton(0) && camera.isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - lastTouchPos;

            // ī�޶� �̵� ��� (ȸ�� ���)
            Vector3 right = camera.transform.right; // ī�޶��� ������ ����
            Vector3 forward = Vector3.Cross(right, Vector3.up); // ī�޶��� ������ ���� ���� (���� ��� ����), ����

            // �Է� ���� ī�޶� �࿡ �°� ��ȯ
            Vector3 move = (-right * delta.x + -forward * delta.y) * camera.dragSpeed * Time.deltaTime;

            camera.transform.Translate(move, Space.World);

            lastTouchPos = currentMousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            camera.isDragging = false;
        }
#else
        // ����Ͽ��� ��ġ �Է� ó��
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPos = touch.position;
                camera.isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && camera.isDragging)
            {
                Vector2 delta = touch.deltaPosition;

                 // ī�޶� �̵� ��� (ȸ�� ���)
                Vector3 right = controller.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                Vector3 move = (-right * delta.x + forward * delta.y) * controller.dragSpeed * Time.deltaTime;

                camera.transform.Translate(move, Space.World);

                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                camera.isDragging = false;
            }
        }
#endif
    }

    /*
     ���� �� ũ�Ⱑ �����Ǹ� �� �̵� ���� �ڵ�
    // ī�޶� �̵� ���� ����
    Vector3 newPosition = controller.transform.position + move;

    // �̵� ������ �� ��� ���� (��: x: -10 ~ 10, z: -10 ~ 10)
    newPosition.x = Mathf.Clamp(newPosition.x, -10f, 10f);
    newPosition.z = Mathf.Clamp(newPosition.z, -10f, 10f);

    // ���ѵ� ��ġ�� ī�޶� �̵�
    controller.transform.position = newPosition;
     */
}

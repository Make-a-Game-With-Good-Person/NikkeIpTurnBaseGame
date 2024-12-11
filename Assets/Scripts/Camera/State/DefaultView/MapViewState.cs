using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewState : ICameraState
{
    Vector2 lastTouchPos;
    public void UpdateState(CameraStateController camera)
    {
#if UNITY_EDITOR
        // 에디터에서 마우스 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPos = Input.mousePosition;
            camera.isDragging = true;
        }
        else if (Input.GetMouseButton(0) && camera.isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 delta = currentMousePosition - lastTouchPos;

            // 카메라 이동 계산 (회전 고려)
            Vector3 right = camera.transform.right; // 카메라의 오른쪽 방향
            Vector3 forward = Vector3.Cross(right, Vector3.up); // 카메라의 앞으로 가는 방향 (수평 평면 투영), 외적

            // 입력 값을 카메라 축에 맞게 변환
            Vector3 move = (-right * delta.x + -forward * delta.y) * camera.dragSpeed * Time.deltaTime;

            camera.transform.Translate(move, Space.World);

            lastTouchPos = currentMousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            camera.isDragging = false;
        }
#else
        // 모바일에서 터치 입력 처리
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

                 // 카메라 이동 계산 (회전 고려)
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
     추후 맵 크기가 결정되면 쓸 이동 제한 코드
    // 카메라 이동 제한 설정
    Vector3 newPosition = controller.transform.position + move;

    // 이동 가능한 맵 경계 제한 (예: x: -10 ~ 10, z: -10 ~ 10)
    newPosition.x = Mathf.Clamp(newPosition.x, -10f, 10f);
    newPosition.z = Mathf.Clamp(newPosition.z, -10f, 10f);

    // 제한된 위치로 카메라 이동
    controller.transform.position = newPosition;
     */
}

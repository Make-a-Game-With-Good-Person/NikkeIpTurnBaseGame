using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 quarterViewoffset = new Vector3(3, 5, -5);
    public Vector3 quarterViewrotationOffset = new Vector3(45, -30, 0);  // 각도 offset (Pitch, Yaw, Roll)

    public Vector3 shoulderViewOffset = new Vector3(1, 1, 5);
    public Vector3 shoulderViewrotationOffset = new Vector3(0, 0, 0);  // 각도 offset (Pitch, Yaw, Roll)

    public Vector3 enemyShoulderViewOffset = new Vector3(-1, 1, 5);
    public Vector3 enemyShoulderViewrotationOffset = new Vector3(0, 180, 0);  // 각도 offset (Pitch, Yaw, Roll)

    public bool playerPhase = true;
    public float transitionSpeed = 2f;
    public float rotationSpeed = 4f;

    // 상태 객체 캐싱
    ICameraState quarterViewState;
    ICameraState shoulderViewState;
    ICameraState currentState;

    // Start is called before the first frame update
    void Start()
    {
        // 상태 객체를 한번만 생성해두고 재사용
        quarterViewState = new QuarterViewState();
        shoulderViewState = new ShoulderViewState();
        currentState = quarterViewState; // 초기 상태 설정
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SwitchToShoulderView();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ReturnToQuarterView();
        }
    }

    public void SetPlayerPhase(bool _phase)
    {
        this.playerPhase = _phase;
    }

    public void SetCamTarget(Transform _target)
    {
        this.target = _target;
    }

    public void SwitchToShoulderView()
    {
        currentState = shoulderViewState;
    }

    public void ReturnToQuarterView()
    {
        currentState = quarterViewState;
    }

}

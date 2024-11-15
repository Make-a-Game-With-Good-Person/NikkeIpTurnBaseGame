using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 quarterViewoffset = new Vector3(3, 5, -5);
    public Vector3 quarterViewrotationOffset = new Vector3(45, -30, 0);  // ���� offset (Pitch, Yaw, Roll)

    public Vector3 shoulderViewOffset = new Vector3(1, 1, 5);
    public Vector3 shoulderViewrotationOffset = new Vector3(0, 0, 0);  // ���� offset (Pitch, Yaw, Roll)

    public Vector3 enemyShoulderViewOffset = new Vector3(-1, 1, 5);
    public Vector3 enemyShoulderViewrotationOffset = new Vector3(0, 180, 0);  // ���� offset (Pitch, Yaw, Roll)

    public bool playerPhase = true;
    public float transitionSpeed = 2f;
    public float rotationSpeed = 4f;

    // ���� ��ü ĳ��
    ICameraState quarterViewState;
    ICameraState shoulderViewState;
    ICameraState currentState;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ��ü�� �ѹ��� �����صΰ� ����
        quarterViewState = new QuarterViewState();
        shoulderViewState = new ShoulderViewState();
        currentState = quarterViewState; // �ʱ� ���� ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 quarterViewoffset = new Vector3(0, 5, -10);
    public Vector3 quarterViewrotationOffset = new Vector3(10, 0, 0);  // ���� offset (Pitch, Yaw, Roll)

    public Vector3 shoulderViewOffset = new Vector3(0.5f, 2, -2);
    public Vector3 shoulderViewrotationOffset = new Vector3(10, 0, 0);  // ���� offset (Pitch, Yaw, Roll)

    public float transitionSpeed = 2f;

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

        /*if (Input.GetKeyDown(KeyCode.F1))
        {
            SwitchToShoulderView();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ReturnToQuarterView();
        }*/
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

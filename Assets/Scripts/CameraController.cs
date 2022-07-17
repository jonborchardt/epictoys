using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MinFollowYOffset = 2f;

    private const float MaxFollowYOffset = 12f;

    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualCamera;

    private Vector3 targetFollowOffset;

    private CinemachineTransposer cinemachineTransposer;

    void Start()
    {
        cinemachineTransposer =
            cinemachineVirtualCamera
                .GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        float moveSpeed = 10f;
        Vector3 moveVector =
            transform.forward * inputMoveDir.z +
            transform.right * inputMoveDir.x;
        transform.position += moveVector * Time.deltaTime * moveSpeed;
    }

    private void HandleRotation()
    {
        float rotationSpeed = 100f;
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }
        transform.eulerAngles +=
            rotationVector * Time.deltaTime * rotationSpeed;
    }

    private void HandleZoom()
    {
        float zoomAmount = 1f;
        float zoomSpeed = 5f;
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        targetFollowOffset.y =
            Mathf
                .Clamp(targetFollowOffset.y,
                MinFollowYOffset,
                MaxFollowYOffset);
        cinemachineTransposer.m_FollowOffset =
            Vector3
                .Lerp(cinemachineTransposer.m_FollowOffset,
                targetFollowOffset,
                zoomSpeed * Time.deltaTime);
    }
}

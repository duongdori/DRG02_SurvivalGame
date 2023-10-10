using Cinemachine;
using UnityEngine;

namespace CameraSystem
{
    public class CameraSystem : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private CinemachineTransposer cinemachineTransposer;

        [Header("Camera Movement")] [SerializeField]
        private float moveSpeed = 50f;

        [Header("Camera Move Option")] [SerializeField]
        private bool useEdgeScrolling = false;

        [SerializeField] private bool useDragPan = false;

        [Header("Camera Rotation")] [SerializeField]
        private float rotateSpeed = 100f;

        [Header("Camera Zoom")] [SerializeField]
        private float zoomSpeed = 10f;

        [Header("Camera Zoom FieldOfView")] [SerializeField]
        private float fieldOfViewMax = 50f;

        [SerializeField] private float fieldOfViewMin = 10f;

        [Header("Camera Zoom MoveForward")] [SerializeField]
        private float followOffsetMax = 50f;

        [SerializeField] private float followOffsetMin = 5f;

        [Header("Camera Zoom LowerY")] [SerializeField]
        private float followOffsetMaxY = 50f;

        [SerializeField] private float followOffsetMinY = 10f;

        private bool dragPanMoveActive = false;
        private Vector2 lastMousePosition;
        private Vector3 followOffset;
        private float targetFieldOfView = 50f;

        private Transform player;

        private void Awake()
        {
            //virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            player = GameObject.Find("Player").transform;

            followOffset = cinemachineTransposer.m_FollowOffset;
        }

        private void Update()
        {
            //HandleCameraMovement();

            if (useEdgeScrolling)
            {
                HandleCameraMovementEdgeScrolling();
            }

            if (useDragPan)
            {
                HandleCameraMovementDragPan();
            }

            HandleCameraRotation();

            //HandleCameraZoom_FieldOfView();
            HandleCameraZoom_MoveForward();
            //HandleCameraZoom_LowerY();
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(player.position.x, 0f, player.transform.position.z);
        }

        private void HandleCameraMovement()
        {
            Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        private void HandleCameraMovementEdgeScrolling()
        {
            Vector3 inputDir = new Vector3(0f, 0f, 0f);

            int edgeScrollSize = 20;

            if (Input.mousePosition.x < edgeScrollSize)
            {
                inputDir.x = -1f;
            }

            if (Input.mousePosition.x > Screen.width - edgeScrollSize)
            {
                inputDir.x = +1f;
            }

            if (Input.mousePosition.y < edgeScrollSize)
            {
                inputDir.z = -1f;
            }

            if (Input.mousePosition.y > Screen.height - edgeScrollSize)
            {
                inputDir.z = +1f;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        private void HandleCameraMovementDragPan()
        {
            Vector3 inputDir = new Vector3(0f, 0f, 0f);

            if (Input.GetMouseButtonDown(1))
            {
                dragPanMoveActive = true;
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(1))
            {
                dragPanMoveActive = false;
            }

            if (dragPanMoveActive)
            {
                Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

                float dragPanSpeed = 0.5f;

                inputDir.x = mouseMovementDelta.x * dragPanSpeed;
                inputDir.z = mouseMovementDelta.y * dragPanSpeed;

                lastMousePosition = Input.mousePosition;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        private void HandleCameraRotation()
        {
            float rotateDir = 0f;
            if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
            if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

            transform.eulerAngles += new Vector3(0f, rotateDir * rotateSpeed * Time.deltaTime, 0f);
        }

        private void HandleCameraZoom_FieldOfView()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFieldOfView -= 5;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                targetFieldOfView += 5;
            }

            targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFieldOfView,
                zoomSpeed * Time.deltaTime);
        }

        private void HandleCameraZoom_MoveForward()
        {
            Vector3 zoomDir = followOffset.normalized;

            float zoomAmount = 3f;

            if (Input.mouseScrollDelta.y > 0)
            {
                followOffset -= zoomDir * zoomAmount;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                followOffset += zoomDir * zoomAmount;
            }

            if (followOffset.magnitude < followOffsetMin)
            {
                followOffset = zoomDir * followOffsetMin;
            }

            if (followOffset.magnitude > followOffsetMax)
            {
                followOffset = zoomDir * followOffsetMax;
            }

            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, followOffset,
                zoomSpeed * Time.deltaTime);
        }

        private void HandleCameraZoom_LowerY()
        {
            float zoomAmount = 3f;

            if (Input.mouseScrollDelta.y > 0)
            {
                followOffset.y -= zoomAmount;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                followOffset.y += zoomAmount;
            }

            followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);

            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, followOffset,
                zoomSpeed * Time.deltaTime);
        }
    }
}


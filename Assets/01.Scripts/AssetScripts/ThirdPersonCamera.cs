
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

namespace UnityChan
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public float smooth = 3f;       // 카메라 움직임 부드럽게 하는 변수
        public int CameraPositionIndex = 1; // 카메라 포지션변경 값
        Transform standardPos;          // 기본 카메라 위치
        Transform frontPos;         // 전방카메라 위치
        Transform lookAtPos;            // 일인칭 카메라 위치

        private float camCurXRot;
        private float camCurYRot;
        public float minLook;
        public float maxLook;
        public float lookSensitivity;

        private Vector2 mouseDelta;


        private void Start()
        {
            standardPos = GameObject.Find("CamPos").transform;
            frontPos = GameObject.Find("FrontPos").transform;
            lookAtPos = GameObject.Find("LookAtPos").transform;

            //카메라 위치 및 방향 초기화
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
        }
        private void Update()
        {
            if (CameraPositionIndex == 2) Look();
        }

        private void SwitchCamera()
        {
            switch (CameraPositionIndex)
            {
                case 1:
                    SetCameraPositionFrontView(frontPos);
                    break;
                case 2:
                    SetCameraPositionFrontView(lookAtPos);
                    break;
                default:
                    CameraPositionIndex = 0;
                    SetCameraPositionFrontView(standardPos);
                    break;
            }

        }

        private void SetCameraPositionFrontView(Transform target)
        {
            transform.position = target.position;
            transform.forward = target.forward;
        }

        private void Look()
        {
            camCurXRot += mouseDelta.y * lookSensitivity;
            camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
            camCurYRot += mouseDelta.x * lookSensitivity;
            camCurYRot = Mathf.Clamp(camCurYRot, minLook, maxLook);
            transform.localEulerAngles = new Vector3(-camCurXRot, camCurYRot, 0);
        }

        public void OnCameraTransition(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                CameraPositionIndex++;
                SwitchCamera();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (CameraPositionIndex == 2)
                mouseDelta = context.ReadValue<Vector2>();
        }

    }
}
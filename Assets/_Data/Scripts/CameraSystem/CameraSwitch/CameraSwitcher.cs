using System;
using UnityEngine;
using Cinemachine;

namespace CameraSystem.CameraSwitch
{
    public class CameraSwitcher : MonoBehaviour
    {
        public static CameraSwitcher Instance { get; private set; }
        
        [SerializeField] private CinemachineFreeLook thirdPersonCamera;
        [SerializeField] private CinemachineVirtualCamera topdownCamera;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            SwitchToTopdownCamera();
        }

        private void SwitchToTopdownCamera()
        {
            topdownCamera.Priority = 10;
            thirdPersonCamera.Priority = 0;
        }
        
        public void SwitchToThirdPersonCamera()
        {
            thirdPersonCamera.Priority = 10;
            topdownCamera.Priority = 0;
        }
    }
}

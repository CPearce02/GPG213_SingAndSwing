using Cinemachine;
using UnityEngine;

namespace Cameras
{
    public class SegmentHandler : MonoBehaviour
    {
        //Deprecated for now
        [field:SerializeField] public CinemachineVirtualCamera VCam { get; private set; }

        private void Awake() => VCam = GetComponentInChildren<CinemachineVirtualCamera>();
        public void IncreaseVCamPriority() => VCam.Priority++;
        public void DecreaseVCamPriority() => VCam.Priority--;
    }
}

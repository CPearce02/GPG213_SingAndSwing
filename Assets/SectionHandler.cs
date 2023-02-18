using Cinemachine;
using UnityEngine;

public class SegmentHandler : MonoBehaviour
{
    [field:SerializeField] public CinemachineVirtualCamera VCam { get; private set; }

    private void Awake() => VCam = GetComponentInChildren<CinemachineVirtualCamera>();
    public void IncreaseVCamPriority() => VCam.Priority++;
    public void DecreaseVCamPriority() => VCam.Priority--;
}

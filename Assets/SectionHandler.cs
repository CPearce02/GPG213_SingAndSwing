using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SegmentHandler : MonoBehaviour
{
    [field:SerializeField] public CinemachineVirtualCamera VCam { get; private set; }

    private void Awake()
    {
        VCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

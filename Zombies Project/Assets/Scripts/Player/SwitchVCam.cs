
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class SwitchVCam : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField] private Canvas ThirdPersonCanvas;
    [SerializeField] private Canvas AimCanvas;

    [Header("Player References")]
    [SerializeField] private PlayerInput playerInput;


    [Header("Cinemachine")]
    [SerializeField] private int priorityBoostAmount = 10;
    private CinemachineVirtualCamera virtualCamera;


    private InputAction aimAction;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }
    
    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
      
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
      
    }

    private void StartAim()
    {
        virtualCamera.Priority += priorityBoostAmount;
        AimCanvas.enabled = true;
        ThirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= priorityBoostAmount;
        AimCanvas.enabled = false;
        ThirdPersonCanvas.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMovable
{
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float gravity = 40f;
    [SerializeField] private float jumpForce = 10f;

    float maxDistance = 0.1f;

    public Transform cameraAngle;
    private Vector2 mouseDelta;
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private PlayerHealth playerHealth;
    //private Transform spine_01;

    [SerializeField]
    private Transform rightArm;

    public float mouseSensitivity = 10f; // 마우스 민감도
    private float xRotation = 0f; // 수직 회전을 위한 변수

    //[SerializeField]
    //private int playerMaxHP = 100;
    //[SerializeField]
    //private int playerCurrentHP = 100;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //spine_01 = animator.GetBoneTransform(HumanBodyBones.Spine);
        Cursor.lockState = CursorLockMode.Locked;
        //UIManager.Instance.UpdateHpBar(playerCurrentHP, playerMaxHP);
    }

    private void Update()
    {
        if(!IsGrounded())
            moveVector.y -= gravity * Time.deltaTime;
        //controller.Move는 월드좌표 벡터로 움직이기 때문에 변환해주어야 한다.
        controller.Move(transform.TransformDirection(moveVector) * moveSpeed * runSpeed * Time.deltaTime);
        SetAnimator();
    
        Debug.DrawRay(transform.position, -transform.up * maxDistance, Color.red);
    }

    private void LateUpdate()
    {
        //spine_01.RotateAround(spine_01.transform.position, transform.right, xRotation);
        //rightArm.RotateAround(rightArm.transform.position, transform.right, xRotation);
    }

    #region 인풋 시스템 함수
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector3>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) runSpeed = 2f;
        else if (context.performed) { }
        else if (context.canceled) runSpeed = 1f;
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        //수평 회전
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        //수직 회전
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        cameraAngle.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded()) moveVector.y = jumpForce;
    }
    #endregion

    private void SetAnimator()
    {
        animator.SetFloat("XSpeed", moveVector.x * runSpeed);
        animator.SetFloat("ZSpeed", moveVector.z * runSpeed);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, maxDistance);
        if  (hit.collider != null) { /*Debug.Log(hit.collider.name);*/ return true; }
        else { /*Debug.Log("안부딫힘");*/ return false; }
    }
}

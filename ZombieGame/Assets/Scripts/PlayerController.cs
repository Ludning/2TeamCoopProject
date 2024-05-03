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
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isGrounded = true;

    float maxDistance = 0.1f;

    public Transform cameraAngle;
    private Vector2 mouseDelta;
    private CharacterController controller;
    private Animator animator;
    private Transform spine_01;

    public float mouseSensitivity = 10f; // ���콺 �ΰ���
    private float xRotation = 0f; // ���� ȸ���� ���� ����


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spine_01 = animator.GetBoneTransform(HumanBodyBones.Spine);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        moveVector.y -= gravity * Time.deltaTime;
        //controller.Move�� ������ǥ ���ͷ� �����̱� ������ ��ȯ���־�� �Ѵ�.
        controller.Move(transform.TransformDirection(moveVector) * moveSpeed * runSpeed * Time.deltaTime);
        SetAnimator();
    
        Debug.DrawRay(transform.position, -transform.up * maxDistance, Color.red);
    }

    private void LateUpdate()
    {
        //���� �̻��ϱ� �ѵ� ��ü�� ȸ���Ѵ�.
        spine_01.localRotation = Quaternion.Euler(180 , 0, xRotation);
    }

    public void OnMove(InputValue input)
    {
        moveVector = input.Get<Vector3>();
    }

    public void OnRun(InputValue input)
    {
        if (input.isPressed) runSpeed = 2f;
        else runSpeed = 1f;
    }

    public void OnLook(InputValue input)
    {
        mouseDelta = input.Get<Vector2>();
        //���� ȸ��
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        //���� ȸ��
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 50f);
        cameraAngle.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void OnJump(InputValue input)
    {
        if(IsGrounded()) moveVector.y = jumpForce;
    }

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
        else { /*Debug.Log("�Ⱥ΋H��");*/ return false; }
    }

}

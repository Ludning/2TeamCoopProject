using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Field
    [SerializeField]
    Camera camera;
    [SerializeField]
    GameObject idleCamera;
    [SerializeField]
    GameObject leftCamera;
    [SerializeField]
    GameObject rightCamera;

    GameObject currentCamera;

    [SerializeField]
    Transform firePosition;

    Quaternion lookDirection;

    CharacterController characterController;
    Animator animator;
    Vector3 direction = Vector3.zero;
    float force = 0;

    public float gravity = 9.8f;
    private float verticalVelocity = 0f;

    Vector2 mouseDelta = Vector2.zero;

    [SerializeField]
    private float jumpSpeed = 8.0f; // 점프 속도

    [Range(1f, 10f)]
    [SerializeField]
    float walkSpeed = 3f;

    [Range(1f, 10f)]
    [SerializeField]
    float runSpeed = 6f;

    [SerializeField]
    float rotationSpeed = 5f;

    [SerializeField]
    float fireSpeed = 0.9f;
    int currentAmmo = 30;
    int maxAmmo = 30;

    float previousTime = 0f;

    float CurrentSpeed;

    bool isMove = false;
    bool isAim = false;
    bool isRun = false;
    bool isFire = false;
    bool isJump = false;
    bool isReload = false;

    [SerializeField]
    GameObject HitPrefab;

    [SerializeField]
    LineRenderer lineRenderer;

    Coroutine fireCoroutine;
    #endregion


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        lookDirection = Quaternion.identity;

        CurrentSpeed = walkSpeed;

        // 마우스 커서를 숨김
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lineRenderer.enabled = false;
    }


    private void FixedUpdate()
    {
        if(characterController == null)
            return;

        if (characterController.isGrounded)
        {
            if(isJump)
            {
                isJump = false;
            }
            else
            {
                // 캐릭터가 땅에 닿아 있는 경우 중력 효과 초기화
                verticalVelocity = 0f;
            }
        }
        else
        {
            // 캐릭터가 땅에 닿아 있지 않은 경우 중력 효과 적용
            verticalVelocity -= gravity;
        }

        //Debug.Log($"direction : {direction.x}, {direction.z}");

        animator.SetFloat("XSpeed", direction.x * CurrentSpeed);
        animator.SetFloat("YSpeed", direction.z * CurrentSpeed);

        if (isMove || isRun || isFire || isAim)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0), rotationSpeed * Time.deltaTime);
        }

        // 중력에 따라 캐릭터를 아래로 이동
        Vector3 rotatedVector = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * direction;

        characterController.Move(new Vector3(rotatedVector.x * force * CurrentSpeed, verticalVelocity, rotatedVector.z * force * CurrentSpeed) * Time.fixedDeltaTime);
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            animator.SetTrigger("Fire");

            RaycastHit cameraHit;

            // 카메라의 중앙에서 레이캐스트 발사
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);

            if (Physics.Raycast(ray, out cameraHit, 200f))
            {
                // 레이저가 어떤 것에 맞았을 때 처리할 내용
                Debug.Log("레이저가 " + cameraHit.collider.gameObject.name + "에 맞았습니다.");
            }
            Vector3 fireDir = cameraHit.point - firePosition.position;

            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;
            if (Physics.Raycast(firePosition.position, fireDir, out hit, 200f))
            {
                Debug.Log("충돌 발생: " + hit.collider.name);
                if (hit.transform.CompareTag("Target"))
                {
                    Target target = hit.transform.gameObject.GetComponent<Target>();
                    target.OnDamage();
                    OnHit(hit.point);
                }
                hitPosition = hit.point;
            }

            lineRenderer.SetPosition(0, firePosition.position);
            lineRenderer.SetPosition(1, hitPosition);
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
            lineRenderer.enabled = false;

            yield return new WaitForSeconds(0.2f);
        }
    }
    private void OnHit(Vector3 point)
    {
        GameObject instance = Instantiate(HitPrefab);
        instance.transform.position = point;
        instance.transform.Rotate(Vector3.forward, 180f);
        Destroy(instance, 5f);
    }
    #region Input System Event
    public void OnMove(InputAction.CallbackContext ctx)
    {
        //Debug.Log("OnMove");
        //움직임
        if (ctx.started)
        {
            isMove = true;
        }
        else if (ctx.performed)
        {
            Debug.Log("OnMove");
            Vector2 input = ctx.ReadValue<Vector2>();
            direction = new Vector3(input.x, 0, input.y).normalized;
            force = new Vector3(input.x, 0, input.y).magnitude;
        }
        else if (ctx.canceled)
        {
            isMove = false;
            direction = Vector3.zero;
            force = 0f;
        }
    }
    public void OnRun(InputAction.CallbackContext ctx)
    {
        //달리기
        Debug.Log("OnRun");

        if (ctx.started)
        {
            isRun = true;
            CurrentSpeed = runSpeed;
        }
        else if (ctx.performed)
        {

        }
        else if (ctx.canceled)
        {
            isRun = false;
            CurrentSpeed = walkSpeed;
        }
    }
    public void OnFire(InputAction.CallbackContext ctx)
    {
        //발사
        if (ctx.started)
        {
            isFire = true;
            Debug.Log("OnFireStart");
            fireCoroutine = StartCoroutine(FireCoroutine());
        }
        else if (ctx.performed)
        {
            
        }
        else if (ctx.canceled)
        {
            isFire = false;
            Debug.Log("OnFireEnd");
            StopCoroutine(fireCoroutine);
            lineRenderer.enabled = false;
        }
    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        //조준
        Debug.Log("OnAim");

        if (ctx.started)
        {
            mouseDelta = Vector2.zero;
            isAim = true;
            rightCamera.SetActive(true);
            leftCamera.SetActive(false);
            idleCamera.SetActive(false);
        }
        else if (ctx.performed)
        {
            Debug.Log("OnFire");
        }
        else if (ctx.canceled)
        {
            isAim = false;
            idleCamera.SetActive(true);
            leftCamera.SetActive(false);
            rightCamera.SetActive(false);
        }
    }
    public void OnShoulderSwitch(InputAction.CallbackContext ctx)
    {
        //숄더뷰 변경
        string controlPath = ctx.control.path;
        Debug.Log(controlPath);

        if (controlPath.Contains("q"))
        {
            Debug.Log("Q key pressed");
            leftCamera.SetActive(true);
            rightCamera.SetActive(false);
            idleCamera.SetActive(false);
        }
        else if (controlPath.Contains("e"))
        {
            Debug.Log("E key pressed");
            rightCamera.SetActive(true);
            leftCamera.SetActive(false);
            idleCamera.SetActive(false);
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        //점프

        if (ctx.started)
        {
            isJump = true;
            Debug.Log("OnJump");
            if (characterController.isGrounded)
            {
                // 캐릭터가 바닥에 닿아있을 때만 점프 가능
                verticalVelocity = jumpSpeed; // 점프 힘 적용
            }
        }
        else if (ctx.performed)
        {

        }
        else if (ctx.canceled)
        {

        }
    }
    public void OnSetCursor(InputAction.CallbackContext ctx)
    {
        if (Cursor.visible == true)
        {
            // 마우스 커서를 숨김
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void OnReload(InputAction.CallbackContext ctx)
    {
        //재장전

        if (ctx.started)
        {
            Debug.Log("OnReload");
            isReload = true;
            animator.SetTrigger("Reload");
        }
        else if (ctx.performed)
        {

        }
        else if (ctx.canceled)
        {

        }
    }
    public void OnMouse(InputAction.CallbackContext ctx)
    {
        // 마우스 이동값 가져오기
        mouseDelta = ctx.ReadValue<Vector2>();
    }
    #endregion
}
public enum PlayerState
{
    Idle,
    Walk,
    Run,
}


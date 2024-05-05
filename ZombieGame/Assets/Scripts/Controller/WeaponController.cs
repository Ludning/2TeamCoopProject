using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private Animator animator;

    //무기 거치대
    [SerializeField]
    Transform weaponHanger;

    List<GameObject> weaponList = new List<GameObject>();
    //현재 무기
    IWeapon currentWeapon;

    Coroutine weaponFireCoroutine;

    [SerializeField]
    Rig aimRig;

    [SerializeField]
    Transform RightHand;
    List<GameObject> reloadList = new List<GameObject>();

    //사격 조준 IK
    [SerializeField]
    TwoBoneIKConstraint LeftIKConstraint;
    [SerializeField]
    TwoBoneIKConstraint RightIKConstraint;
    [SerializeField]
    MultiAimConstraint HeadAimConstraint;
    [SerializeField]
    MultiAimConstraint HangerAimConstraint;

    //재장전용 IK
    [SerializeField]
    MultiParentConstraint reloadHandConstrain;
    MultiParentConstraint magazineConstrain;


    [SerializeField]
    Transform leftHandTarget;
    [SerializeField]
    Transform rightHandTarget;

    

    [SerializeField]
    Transform aimPosition;

    [SerializeField]
    private RectTransform aimScreenPosition;

    Vector2 AimScreenCenterPosition
    {
        get
        {
            return new Vector3(Screen.width / 2, Screen.height / 2);
        }
    }
    Vector2 AimScreenPosition{ get; set; }

    private void Awake()
    {

        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("AssaultRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("CrossBow").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("FlameThrower").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("HuntingRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("MachineGun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Minigun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("RocketLauncher").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Shotgun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SniperRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SubMGun").WaitForCompletion()));

        weaponList.ForEach(weapon =>
        {
            weapon.transform.SetParent(weaponHanger);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.GetComponent<IWeapon>().OnEquip();
        });

        ActiveWeapon(0);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        leftHandTarget.position = currentWeapon.GetLeftHandGrip().position;
        leftHandTarget.rotation = currentWeapon.GetLeftHandGrip().rotation;
        rightHandTarget.position = currentWeapon.GetRightHandGrip().position;
        rightHandTarget.rotation = currentWeapon.GetRightHandGrip().rotation;

        //반동 회복 코드
        if(Vector2.Distance(AimScreenPosition, AimScreenCenterPosition) > 0.05f)
            AimScreenPosition = Vector2.Lerp(AimScreenPosition, AimScreenCenterPosition, currentWeapon.GetRecoverySpeed() * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        RaycastHit hit; // 충돌 정보를 저장할 변수
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(AimScreenPosition.x, AimScreenPosition.y));

        if (Physics.Raycast(ray, out hit))
        {
            aimPosition.position = hit.point;
        }
        else
        {
            aimPosition.position = ray.origin + ray.direction * 100;
        }
        aimScreenPosition.position = AimScreenPosition;
    }

    public void AimReaction(float recoilAmount)
    {
        // 카메라의 중심에서 레이캐스트를 발사

        AimScreenPosition = new Vector2(AimScreenPosition.x + UnityEngine.Random.Range(-recoilAmount, recoilAmount), AimScreenPosition.y + UnityEngine.Random.Range(0, recoilAmount));
    }

    #region 입력시스템에서 사용하는 함수
    //시야
    public void OnLook(InputAction.CallbackContext context)
    {

    }
    //발사
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger("IsFire");
            currentWeapon.OnFireStart(AimReaction);
        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {
            currentWeapon.OnFireEnd();
        }
    }
    //조준
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }
    }
    //재장전
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentWeapon.OnReload(StartReloadAnimation, EndReloadAnimation);
        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }
    }
    //무기 변경
    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        animator.SetTrigger("IsChangeWeapon");

        string controlPath = context.control.path;
        Debug.Log(controlPath);
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (controlPath.Contains(i.ToString()))
            {
                ActiveWeapon(i);
                break;
            }
        }
    }
    #endregion
    //무기 활성화
    public void ActiveWeapon(int index)
    {
        weaponList.ForEach(weapon => weapon.SetActive(false));
        weaponList[index].SetActive(true);
        currentWeapon = weaponList[index].GetComponent<IWeapon>();

        //GunAimConstraint.data.constrainedObject = currentWeapon.GetWeaponTransform();

        //reloadHandConstrain.data.sourceObjects = weaponList[index].transform;

        //LeftIKConstraint.data.target = currentWeapon.GetLeftHandGrip();
        //RightIKConstraint.data.target = currentWeapon.GetRightHandGrip();
    }
    public void StartReloadAnimation()
    {
        animator.SetTrigger("IsReload");

        aimRig.weight = 0;
        //reloadRig.weight = 1;
        currentWeapon.GetGameObject().transform.SetParent(RightHand);// + currentWeapon.GetRightHandGrip().localPosition;
        currentWeapon.GetGameObject().transform.localPosition -= currentWeapon.GetRightHandGrip().localPosition;
    }
    public void EndReloadAnimation()
    {
        aimRig.weight = 1;
        //reloadRig.weight = 0;
        currentWeapon.GetGameObject().transform.SetParent(weaponHanger);
        currentWeapon.GetGameObject().transform.localPosition = Vector3.zero;
        currentWeapon.GetGameObject().transform.localRotation = Quaternion.identity;
    }
    public void InitWeapon(GameObject prefab)
    {
        weaponList.Add(Instantiate(prefab));
        reloadList.Add(Instantiate(prefab));
    }
}

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
    private LivingEntity playerHealth;

    //���� ��ġ��
    [SerializeField]
    Transform weaponHanger;

    List<GameObject> weaponList = new List<GameObject>();
    //���� ����
    IWeapon currentWeapon;

    Coroutine weaponFireCoroutine;

    [SerializeField]
    Rig aimRig;

    [SerializeField]
    Transform LeftHand;
    [SerializeField]
    Transform RightHand;

    List<GameObject> reloadList = new List<GameObject>();

    //��� ���� IK
    [SerializeField]
    TwoBoneIKConstraint LeftIKConstraint;
    [SerializeField]
    TwoBoneIKConstraint RightIKConstraint;
    [SerializeField]
    MultiAimConstraint HeadAimConstraint;
    [SerializeField]
    MultiAimConstraint HangerAimConstraint;

    //�������� IK
    [SerializeField]
    MultiParentConstraint reloadHandConstrain;
    MultiParentConstraint magazineConstrain;


    [SerializeField]
    Transform leftHandTarget;
    [SerializeField]
    Transform rightHandTarget;

    float prevAnimationSpeed;

    [SerializeField]
    Transform aimPosition;

    [SerializeField]
    private RectTransform aimScreenPosition;

    [SerializeField]
    AnimationClip reloadClip;

    [SerializeField]
    Transform leftHandReloadTransform;

    bool isReload = false;
    bool isFire = false;

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
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Minigun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("RocketLauncher").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SubMGun").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("CrossBow").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("FlameThrower").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("HuntingRifle").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("MachineGun").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Shotgun").WaitForCompletion()));
        //weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SniperRifle").WaitForCompletion()));

        weaponList.ForEach(weapon =>
        {
            weapon.transform.SetParent(weaponHanger);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            IWeapon wp = weapon.GetComponent<IWeapon>();
            wp.OnEquip(weaponList.IndexOf(weapon));
        });

        ActiveWeapon(0);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<LivingEntity>();
        AimScreenPosition = AimScreenCenterPosition;
    }
    private void Update()
    {
        if(playerHealth.IsDead) aimRig.weight = 0;

        leftHandTarget.position = currentWeapon.GetLeftHandGrip().position;
        leftHandTarget.rotation = currentWeapon.GetLeftHandGrip().rotation;
        rightHandTarget.position = currentWeapon.GetRightHandGrip().position;
        rightHandTarget.rotation = currentWeapon.GetRightHandGrip().rotation;

        //�ݵ� ȸ�� �ڵ�
        if (Vector2.Distance(AimScreenPosition, AimScreenCenterPosition) > 0.05f)
            AimScreenPosition = Vector2.Lerp(AimScreenPosition, AimScreenCenterPosition, currentWeapon.GetRecoverySpeed() * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        RaycastHit hit; // �浹 ������ ������ ����
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
        // ī�޶��� �߽ɿ��� ����ĳ��Ʈ�� �߻�

        AimScreenPosition = new Vector2(AimScreenPosition.x + UnityEngine.Random.Range(-recoilAmount, recoilAmount), AimScreenPosition.y + UnityEngine.Random.Range(0, recoilAmount));
    }

    #region �Է½ý��ۿ��� ����ϴ� �Լ�
    //�þ�
    public void OnLook(InputAction.CallbackContext context)
    {

    }
    //�߻�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (isReload == true)
            return;
        if(context.started)
        {
            isFire = true;
            animator.SetTrigger("IsFire");
            currentWeapon.OnFireStart(AimReaction);
        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {
            currentWeapon.OnFireEnd();
            isFire = false;
        }
    }
    //����
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
    //������
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
    //���� ����
    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if(isReload == true)
            return;
        animator.SetTrigger("IsChangeWeapon");

        string controlPath = context.control.path;
        Debug.Log(controlPath);
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (controlPath.Contains((i + 1).ToString()))
            {
                ActiveWeapon(i);
                break;
            }
        }
    }
    #endregion
    //���� Ȱ��ȭ
    public void ActiveWeapon(int index)
    {
        weaponList.ForEach(weapon => { weaponList[index].GetComponent<IWeapon>().StopMuzzleFlash(); weapon.SetActive(false); });
        weaponList[index].SetActive(true);
        currentWeapon = weaponList[index].GetComponent<IWeapon>();
        currentWeapon.StopMuzzleFlash();
        currentWeapon.GetReloadMagazineTransform().SetParent(leftHandReloadTransform);
        currentWeapon.GetReloadMagazineTransform().localPosition = Vector3.zero;
        currentWeapon.GetReloadMagazineTransform().gameObject.SetActive(false);
    }
    public void StartReloadAnimation(float reloadTime)
    {
        if(isFire == true)
            return;
        isReload = true;
        animator.SetTrigger("IsReload");

        aimRig.weight = 0;

        prevAnimationSpeed = animator.speed;
        Debug.Log($"reloadClip.length : {reloadClip.length}, {reloadClip.length / reloadTime}");
        //������ �ִϸ��̼� ��� �ӵ� ���
        float animationSpeed = reloadClip.length / reloadTime;
        // �ִϸ��̼��� ��� �ӵ� ����
        animator.speed = animationSpeed;

        currentWeapon.GetReloadMagazineTransform().gameObject.SetActive(true);

        currentWeapon.GetGameObject().transform.SetParent(RightHand);// + currentWeapon.GetRightHandGrip().localPosition;
        currentWeapon.GetGameObject().transform.localPosition -= currentWeapon.GetRightHandGrip().localPosition;
    }
    public void EndReloadAnimation()
    {
        animator.speed = prevAnimationSpeed;

        aimRig.weight = 1;

        currentWeapon.GetReloadMagazineTransform().gameObject.SetActive(false);

        currentWeapon.GetGameObject().transform.SetParent(weaponHanger);
        currentWeapon.GetGameObject().transform.localPosition = Vector3.zero;
        currentWeapon.GetGameObject().transform.localRotation = Quaternion.identity;
        isReload = false;
    }
    public void InitWeapon(GameObject prefab)
    {
        weaponList.Add(Instantiate(prefab));
        reloadList.Add(Instantiate(prefab));
    }
    public void AddAmmo()
    {
        weaponList.ForEach(weapon => { weapon.GetComponent<Weapon>().AddAmmo(); });
    }
}

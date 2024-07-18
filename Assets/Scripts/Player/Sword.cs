using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] protected Transform weaponCollider;

    private PlayerController playerController;
    private Animator animator;
    private Player player;
    private ActiveWeapon activeWeapon;

    private GameObject slashAni;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void Start()
    {
        playerController.Combat.Attack.started += _ => Acttack();
    }

    private void Acttack()
    {
        animator.SetTrigger("Attack");

        weaponCollider.gameObject.SetActive(true);
        slashAni = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAni.transform.parent = this.transform.parent;
    }

    public void DoneAttackAnimation()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpAnimation()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);

        if (player.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (player.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

}

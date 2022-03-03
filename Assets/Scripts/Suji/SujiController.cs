using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpPower;
    public Transform suji;
    public BGMManager _BGMManager;
    public InteractiveObject InteractiveObject { set { _interactiveObject = value; } }
    public bool IsHiding { get { return isHiding; } private set {} }
    public GameObject[] myBodies;


    private InteractiveObject _interactiveObject;
    private Animator animator;
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private WaitForSeconds landingDelay = new WaitForSeconds(0.5f);
    private float walkDirection;
    private float initScaleX;
    private const float JUMP_CHARGING_DELAY = 0.55f;
    private bool hasControl;
    private bool isRunning;
    private bool isJumping;
    private bool isHiding;
    private bool isDie;
    private SoundEvent _soundEvent;
    private const string BGM_DIE = "Sound_Bgm_Die";



    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        _soundEvent = GetComponentInChildren<SoundEvent>();
        initScaleX = suji.localScale.x;
    }

    void Update()
    {
        if (!isDie || FadeInOutController.IsFinished)
        {
            walkDirection = Input.GetAxisRaw("Horizontal");
            hasControl = !Mathf.Approximately(walkDirection, 0f);
            isRunning = Input.GetButton("Run");
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                Jump();
            }

            if (Input.GetButtonDown("Interaction"))
            {
                Interaction();
            }
        }
    }

    void FixedUpdate()
    {
        TurnOtherSide();
        Move();
    }


    public void Hide(SpriteRenderer hidingSprite, bool isDynamicHiddenSpace)
    {
        if (isJumping)
            return;

        if (!isHiding)
        {
            isHiding = true;
            ControlEnableMyBody(false);
        }
        else
        {
            isHiding = false;
            ControlEnableMyBody(true);
        }

        OnOffHiddenSpritePlayer(hidingSprite, isDynamicHiddenSpace);
    }

    public void OnDie()
    {
        isDie = true;
        walkDirection = 0f;
        hasControl = false;
        isRunning = false;
        isJumping = false;

        FadeInOutController.Instance.ImmediateFadeOut();
        _BGMManager.OnPlay();
        FadeInOutController.Instance.TextFadeInOut();
    }



    private void Jump()
    {
        if (isHiding)
            return;

        isJumping = true;

        if (!hasControl)
        {
            animator.SetBool("SargentJump", true);
            Invoke("_Jump", JUMP_CHARGING_DELAY);
        }
        else
        {
            animator.SetBool("MoveJump", true);
            _Jump();
        }
    }

    public void FootSettings(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Jumpable_Floor") && isJumping)
        {
            if (animator.GetBool("SargentJump"))
            {
                animator.SetTrigger("Landing");
                animator.SetBool("SargentJump", false);
                StartCoroutine(LandingDelay());
                return;
            }

            animator.SetBool("MoveJump", false);
            isJumping = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            collision.gameObject.SetActive(false);
        }

        _soundEvent.isSheInside = collision.gameObject.CompareTag("InsideFloor") ? true : false;
    }


    private void Move()
    {
        /* parameterValue => {0f : Idle}, {0.5f : Walk}, {1f : Run}  */

        if (isJumping)
            return;

        Vector2 velocity = new Vector2(0f, _rigidBody.velocity.y);
        float parameterValue = 0f;

        if (!isHiding)
        {
            if (hasControl || (hasControl && isRunning))
            {
                float charMoveSpeed = isRunning ? walkSpeed * 2f : walkSpeed;
                parameterValue = isRunning ? 1f : 0.5f;
                velocity.Set(walkDirection * charMoveSpeed, _rigidBody.velocity.y);
            }
        }

        animator.SetFloat("Move", parameterValue);
        _rigidBody.velocity = velocity;
    }


    private void Interaction()
    {
        if (_interactiveObject == null)
            return;

        _interactiveObject.Interaction();
    }


    private void ControlEnableMyBody(bool enable)
    {
        float gravityScale = enable ? 3f : 0f;

        if (!enable)
        {
            _collider.isTrigger = true;               // enabled = false�� �ϸ�, ��ȣ�ۿ� ������Ʈ�� Ž���� ���Ѵ�.
            _rigidBody.gravityScale = gravityScale;   // �ݶ��̴� �浹�� �� �ǰ� �����߱� ������, �ٴ����� ��������.
        }

        foreach (var myBody in myBodies)
        {
            myBody.SetActive(enable);
        }

        if (enable)
        {
            _collider.isTrigger = false;
            _rigidBody.gravityScale = gravityScale;
        }
    }


    private void OnOffHiddenSpritePlayer(SpriteRenderer hidingSprite, bool isDynamicHiddenSpace)
    {
        if (isDynamicHiddenSpace)
        {
            hidingSprite.transform.position = new Vector3(this.transform.position.x,
                                                          hidingSprite.transform.position.y,
                                                          this.transform.position.z);
        }

        hidingSprite.gameObject.SetActive(IsHiding);
    }


    private void _Jump()
    {
        _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }


    IEnumerator LandingDelay()
    {
        yield return landingDelay;
        isJumping = false;
    }


    private void TurnOtherSide()
    {
        if (!hasControl || isJumping)
            return;

        var scaleX = suji.localScale.x;
        if (Mathf.Approximately(walkDirection * scaleX, initScaleX))
            return;

        var scaleY = suji.localScale.y;
        var scaleZ = suji.localScale.z;

        suji.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }

    public void MoveAnotherRoom(Vector2 roomVector)
    {
        transform.position = roomVector;
    }
}

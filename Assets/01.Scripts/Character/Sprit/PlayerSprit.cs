using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprit : BaseSprit<PlayerPersona,DUnityChan>
{
    //애니메이션 관련
    private Animator anim;
    private float animSpeedRatio = 10.0f;
    private Vector2 moveInput;
    private float animWalkRatio = 1.0f;

    // 애니메이션 상태 정의
    private AnimatorStateInfo currentBaseState;
    private static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    private static int jumpState = Animator.StringToHash("Base Layer.Jump");
    private static int idleState = Animator.StringToHash("Base Layer.Idle");


    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        CharacterManager.Instance.Player = this;
    }
    void Update()
    {
        anim.SetTrigger("Hit");
        // 이동 처리
        Entity.Move(moveInput.x, moveInput.y);
        animWalkRatio = Entity.walkMode ? 0.2f : 1.0f;
        anim.SetFloat("Speed", moveInput.y* animWalkRatio);
        anim.SetFloat("Direction", moveInput.x);
        anim.speed =Entity.forwardSpeed / animSpeedRatio ;

        // 애니메이션 상태 확인
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 처리
        if (currentBaseState.fullPathHash == jumpState)
        {
            if (Entity.IsGrounded())
            {
                anim.SetBool("Jump", false);
            }
        }
    }

    //인풋시스템용 이벤트 메서드
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump()
    {
        if (currentBaseState.fullPathHash == locoState || currentBaseState.fullPathHash == idleState)
        {
            if (!anim.IsInTransition(0))
            {
                if (Entity.TryJump())
                {
                    anim.SetBool("Jump", true);
                }
            }
        }
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Entity.manualWalkMode = true;
        }
            
        else if (context.phase == InputActionPhase.Canceled)
        {
            Entity.manualWalkMode = false;
        }    
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            Persona.TryInteractaction(Persona.curInteractObject);
    }
}

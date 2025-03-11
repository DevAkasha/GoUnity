using System.Collections;
using UnityEngine;

public class DUnityChan : BaseEntity
{
    // 캐릭터 기본 스텟
    public float currentSpeed;
    public float walkSpeed = 3.0f;
    public float forwardSpeed = 10.0f;
    public float backwardSpeed = 3.0f;
    public float rotateSpeed = 4.0f;
    public float jumpPower = 150.0f;
    public bool walkMode = false;
    public bool manualWalkMode = false;

    private float curHP;
    public float CurHP
    {
        get { return curHP; }
        private set
        {
            curHP = Mathf.Min(value, MaxHP);
            if (curHP < 0f) Debug.Log("플레이어가 죽었어요");
        }
    }
    public float MaxHP { get; private set; } = 100f;
    private float curST;
    public float CurST
    {
        get { return curST; }
        private set
        {
            curST = Mathf.Min(value, MaxST);
        }
    }
    public float MaxST { get; private set; } = 200f;

    public float runCostST = 100.0f;
    public float jumpCostST = 20.0f;
    public float stRecoveryRate = 100.0f;
    public float stThreshold;
    // 충돌 관련
    public Rigidbody rb;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
        CurHP = MaxHP*0.5f;
        CurST = MaxST * 0.5f;
        stThreshold = MaxST * 0.3f;
    }

    // 이동 처리
    public void Move(float h, float v)
    {
        Vector3 velocity = new Vector3(0, 0, v);
        velocity = transform.TransformDirection(velocity);

        if (CurST <= 0)
            walkMode = true;
        else if (CurST > stThreshold)
            walkMode = false;

        if (manualWalkMode) walkMode = manualWalkMode;

        currentSpeed = walkMode ? walkSpeed : forwardSpeed;

        if (v > 0.1)
        {
            velocity *= currentSpeed;
            if (!walkMode)
                CurST -= runCostST * Time.fixedDeltaTime;
            else RecoverStamina();
        }
        
        else if (v < -0.1)
        {
            velocity *= backwardSpeed;
            RecoverStamina();
        }

        else RecoverStamina();


        transform.localPosition += velocity * Time.fixedDeltaTime;
        transform.Rotate(0, h * rotateSpeed, 0);
        
    }

    // 점프 처리
    public bool TryJump()
    {
        bool isGround = IsGrounded();
        if (isGround && CurST > jumpCostST)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            CurST -= jumpCostST;
        }

        return isGround;
    }

    // 지면 확인
    public bool IsGrounded()
    {
        float rayLength = 0.3f;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }
    
    // 아이템효과 코루틴
    public IEnumerator IncreaseSpeed(float addSpeed, float duration)
    {
        forwardSpeed += addSpeed;
        yield return new WaitForSeconds(duration);
        forwardSpeed -= addSpeed;
    }

    // 아이템효과 힐
    public void Heal(float addHP)
    {
        CurHP += addHP;
    }

    //스테미나 자동회복
    private void RecoverStamina()
    {
        if (CurST < MaxST)
        {
            CurST += stRecoveryRate * Time.deltaTime;
        }
    }
}

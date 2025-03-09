using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DUnityChan : BaseEntity
{
    // 캐릭터 기본 속성
    public float currentSpeed;
    public float walkSpeed = 3.0f;
    public float forwardSpeed = 10.0f;
    public float backwardSpeed = 3.0f;
    public float rotateSpeed = 4.0f;
    public float jumpPower = 150.0f;
    public bool walkMode = false;
    public bool manualWalkMode = false;


    public float CurHP { get; private set; }
    public float MaxHP { get; private set; } = 100f;
    public float CurST { get; private set; }
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
        CurHP = MaxHP;
        CurST = MaxST;
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

    public bool IsGrounded()
    {
        float rayLength = 0.3f;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, rayLength);
    }

    private void RecoverStamina()
    {
        if (CurST < MaxST)
        {
            CurST += stRecoveryRate * Time.deltaTime;
            CurST = Mathf.Min(CurST, MaxST);
        }
    }
}

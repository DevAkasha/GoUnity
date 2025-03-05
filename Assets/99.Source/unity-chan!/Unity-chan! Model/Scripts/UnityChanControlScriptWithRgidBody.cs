
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	//필요 컴포넌트
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]

	public class UnityChanControlScriptWithRgidBody : MonoBehaviour
	{

		public float animSpeed = 1.5f;				// 애니메이션 속도 조절
		public float lookSmoother = 3.0f;			// 카메라 움직임 부드럽게
		public bool useCurves = true;				// 애니메이션 커브를 사용할 지 여부
		
		public float useCurvesHeight = 0.5f;        // 커브 적용 높이 (지면 충돌 문제 방지)

        //캐릭 이동 관련 
        public float forwardSpeed = 7.0f; //전진속도                                   
        public float backwardSpeed = 3.0f; //후진속도		
		public float rotateSpeed = 2.0f; // 회전속도                                       
        public float jumpPower = 3.0f;  // 점프 힘
                                        
        private CapsuleCollider col;
		private Rigidbody rb;
		private Vector3 velocity;

		// 캡슐 콜라이더의 원래 높이 및 중심값
		private float orgColHight;
		private Vector3 orgVectColCenter;

		private Animator anim;							// 애니메이터
		private AnimatorStateInfo currentBaseState;			// 현재 애니메이션 상태

		private GameObject cameraObject;	// 메인 카메라 참조
		
		// 애니메이션 상태 정의
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		// 초기 설정
		void Start ()
		{
			anim = GetComponent<Animator> ();
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
			cameraObject = GameObject.FindWithTag ("MainCamera");
		
			// 캡슐 콜라이더의 원래 높이 및 중심 저장
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}


        // 물리 연산이 포함된 업데이트 (FixedUpdate 사용)
        void FixedUpdate ()
		{
			float h = Input.GetAxis ("Horizontal");				// 수평입력
			float v = Input.GetAxis ("Vertical");				// 수직입력
			anim.SetFloat ("Speed", v);							// 애니메이터의 speed 파라미터 설정
			anim.SetFloat ("Direction", h);                         // 애니메이터의 Direction 파라미터 설정
            anim.speed = animSpeed;                             //애니메이션 속도 설정
            currentBaseState = anim.GetCurrentAnimatorStateInfo (0);    //현재 애니메이션 상태 가져오기
            rb.useGravity = true; //중력 활성화 (점프 시 일시적으로 비활성화됨)



            // 이동 벡터 계산 (Z축 기준)
            velocity = new Vector3 (0, 0, v);		
			velocity = transform.TransformDirection (velocity); // 로컬 공간에서 월드 공간으로 변환
            
			//전진 및 후진 속도 조정
            if (v > 0.1) {
				velocity *= forwardSpeed;		
			} else if (v < -0.1) {
				velocity *= backwardSpeed;
			}

			//점프 입력 처리
			if (Input.GetButtonDown ("Jump")) {	
				
				//점프 입력 처리
				if (currentBaseState.nameHash == locoState) {
					if (!anim.IsInTransition (0)) {
						rb.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
						anim.SetBool ("Jump", true);	
					}
				}
			}
		

			// 이동 적용
			transform.localPosition += velocity * Time.fixedDeltaTime;

			// 회전 적용
			transform.Rotate (0, h * rotateSpeed, 0);	
	

			// 애니메이션 
			if (currentBaseState.nameHash == locoState) {
				if (useCurves) {
					resetCollider (); // 충돌 크기 초기화
				}
			}
		else if (currentBaseState.nameHash == jumpState) {
				cameraObject.SendMessage ("setCameraPositionJumpView");	// 점프 시 카메라 변경
		
				if (!anim.IsInTransition (0)) {
				
					// 점프 중일 때, 애니메이션 커브를 이용해 충돌 크기 조정
					if (useCurves) {

						float jumpHeight = anim.GetFloat ("JumpHeight");
						float gravityControl = anim.GetFloat ("GravityControl"); 
						if (gravityControl > 0)
							rb.useGravity = false;  //점프 중일 때 중력  제거 

                       
                        Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
						RaycastHit hitInfo = new RaycastHit ();

                        // 충돌 감지 후, 일정 높이 이상일 경우 충돌 크기 조정
                        if (Physics.Raycast (ray, out hitInfo)) {
							if (hitInfo.distance > useCurvesHeight) {
								col.height = orgColHight - jumpHeight;			
								float adjCenterY = orgVectColCenter.y + jumpHeight;
								col.center = new Vector3 (0, adjCenterY, 0);	
							} else {					
								resetCollider ();
							}
						}
					}
					// 점프 애니메이션 종료 후 , jump 플래그 초기화			
					anim.SetBool ("Jump", false);
				}
			}

		else if (currentBaseState.nameHash == idleState) {

				if (useCurves) {
					resetCollider ();
				}
                // 점프 키를 누르면 휴식 애니메이션 실행
                if (Input.GetButtonDown ("Jump")) {
					anim.SetBool ("Rest", true);
				}
			}

		else if (currentBaseState.nameHash == restState) {

				if (!anim.IsInTransition (0)) {
					anim.SetBool ("Rest", false);
				}
			}
		}

		// GUI 표시
		void OnGUI ()
		{
			GUI.Box (new Rect (Screen.width - 260, 10, 250, 150), "Interaction");
			GUI.Label (new Rect (Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forwald/Go Back");
			GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
			GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
			GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
			GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
			GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");
		}


        // 충돌 크기 초기화
        void resetCollider ()
		{
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}
	}
}
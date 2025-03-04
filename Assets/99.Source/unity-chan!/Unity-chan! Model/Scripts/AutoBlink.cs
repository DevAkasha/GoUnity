using UnityEngine;
using System.Collections;
using System.Security;

namespace UnityChan
{
	public class AutoBlink : MonoBehaviour
	{

		public bool isActive = true;                //자동 눈 깜빡임 활성화 여부
        //눈 깜빡임 랜더러
        public SkinnedMeshRenderer ref_SMR_EYE_DEF;	
		public SkinnedMeshRenderer ref_SMR_EL_DEF;
		
		// 눈 상태에 따른 BlendShape 가중치
		public float ratio_Close = 85.0f;			//눈 완전 닫힘
		public float ratio_HalfClose = 20.0f;		//눈 반 닫힘
		[HideInInspector]
		public float ratio_Open = 0.0f;						//눈 완전 열림
		
		//타이머 및 눈 깜빡임 상태변수
        private bool timerStarted = false;			// 타이머 시작 여부
		private bool isBlink = false;				// 현재 눈을 깜빡이는 상태인지 여부

		// 눈을 깜빡이는 시간
		public float timeBlink = 0.4f;				
		private float timeRemining = 0.0f;			// 타이머 남은 시간

		public float threshold = 0.3f;				// 깜빡임 발생 확률
		public float interval = 3.0f;				// 랜덤 깜빡임 발생 간격


		// 눈 상태 정의
		enum Status
		{
			Close,
			HalfClose,
			Open
		}


		private Status eyeStatus;	//현재 눈 상태

		void Awake ()
		{
			//ref_SMR_EYE_DEF = GameObject.Find("EYE_DEF").GetComponent<SkinnedMeshRenderer>();
			//ref_SMR_EL_DEF = GameObject.Find("EL_DEF").GetComponent<SkinnedMeshRenderer>();
		}



		// Use this for initialization
		void Start ()
		{
			ResetTimer ();
			// 랜덤하게 깜빡이기 코루틴
			StartCoroutine ("RandomChange");
		}

		//타이머 초기화
		void ResetTimer ()
		{
			timeRemining = timeBlink;
			timerStarted = false;
		}

		// Update is called once per frame
		void Update ()
		{
			//깜빡임
			if (!timerStarted) {
				eyeStatus = Status.Close;
				timerStarted = true;
			}
			
			// 타이머 진행
			if (timerStarted) {
				timeRemining -= Time.deltaTime;
				if (timeRemining <= 0.0f) {
					eyeStatus = Status.Open; //눈 뜨기
					ResetTimer ();
				} else if (timeRemining <= timeBlink * 0.3f) {
					eyeStatus = Status.HalfClose; // 반쯤 감기
				}
			}
		}

		//레이트 업데이트에서 눈 상태 적용
		void LateUpdate ()
		{
			if (isActive) {
				if (isBlink) {
					switch (eyeStatus) {
					case Status.Close:
						SetCloseEyes ();
						break;
					case Status.HalfClose:
						SetHalfCloseEyes ();
						break;
					case Status.Open:
						SetOpenEyes ();
						isBlink = false; // 깜빡임 종료
						break;
					}
				
				}
			}
		}

		// 눈 완전히 감기
		void SetCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Close);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Close);
		}

        // 눈 반쯤 감기
        void SetHalfCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
		}

        // 눈 완전히 열기
        void SetOpenEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Open);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Open);
		}

        // 랜덤하게 눈을 깜빡이도록 하는 코루틴
        IEnumerator RandomChange ()
		{
			
			while (true) {
                // 랜덤 값 생성
                float _seed = Random.Range (0.0f, 1.0f);
				// 일정 확률 이상이면 눈을 깜빡이도록 설정
				if (!isBlink) {
					if (_seed > threshold) {
						isBlink = true;
					}
				}

				// 다음 깜빡임 체크까지 대기
				yield return new WaitForSeconds (interval);
			}
		}
	}
}
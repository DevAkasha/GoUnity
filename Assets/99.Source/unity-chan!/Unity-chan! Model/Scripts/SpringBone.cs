
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class SpringBone : MonoBehaviour
	{
		//연결된 자식 본
		public Transform child;

		//본의 기본방향
		public Vector3 boneAxis = new Vector3 (-1.0f, 0.0f, 0.0f);
		public float radius = 0.05f;

		//고유한 복원력과 감쇠 값을 사용할지 여부
		public bool isUseEachBoneForceSettings = false; 

		//본이 원래 위치로 돌아가려는 복원력
		public float stiffnessForce = 0.01f;

		//감쇠력 (움직임이 얼마나 빠르게 멈추는지)
		public float dragForce = 0.4f;
		public Vector3 springForce = new Vector3 (0.0f, -0.0001f, 0.0f);
		public SpringCollider[] colliders;
		public bool debug = true;
		//움직임 역치값
		public float threshold = 0.01f;
		private float springLength; 
		private Quaternion localRotation; 
		private Transform trs;
		private Vector3 currTipPos;
		private Vector3 prevTipPos;
		//부모 본 
		private Transform org;
		
		private SpringManager managerRef;

		private void Awake ()
		{
			trs = transform;
			localRotation = transform.localRotation;
			managerRef = GetParentSpringManager (transform);
		}

		private SpringManager GetParentSpringManager (Transform t)
		{
			var springManager = t.GetComponent<SpringManager> ();

			if (springManager != null)
				return springManager;

			if (t.parent != null) {
				return GetParentSpringManager (t.parent);
			}

			return null;
		}

		private void Start ()
		{
			springLength = Vector3.Distance (trs.position, child.position);
			currTipPos = child.position;
			prevTipPos = child.position;
		}

		public void UpdateSpring ()
		{
			org = trs;
			//회전값 초기화
			trs.localRotation = Quaternion.identity * localRotation;

			float sqrDt = Time.deltaTime * Time.deltaTime;

			//stiffness
			Vector3 force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

			//drag
			force += (prevTipPos - currTipPos) * dragForce / sqrDt;

			force += springForce / sqrDt;

			
			Vector3 temp = currTipPos;

            //Verlet Integration을 이용한 물리 계산
            currTipPos = (currTipPos - prevTipPos) + currTipPos + (force * sqrDt);

            // 본의 길이를 유지하면서 위치를 업데이트
            currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;

            //충돌 감지 및 위치 조정
            for (int i = 0; i < colliders.Length; i++) {
				if (Vector3.Distance (currTipPos, colliders [i].transform.position) <= (radius + colliders [i].radius)) {
					Vector3 normal = (currTipPos - colliders [i].transform.position).normalized;
					currTipPos = colliders [i].transform.position + (normal * (radius + colliders [i].radius));
					currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;
				}


			}
            //이전 위치 업데이트
			prevTipPos = temp;

            // 본 회전 적용
            Vector3 aimVector = trs.TransformDirection (boneAxis);
			Quaternion aimRotation = Quaternion.FromToRotation (aimVector, currTipPos - trs.position);

            //회전값을 보간하여 자연스러운 움직임 구현
            Quaternion secondaryRotation = aimRotation * trs.rotation;
			trs.rotation = Quaternion.Lerp (org.rotation, secondaryRotation, managerRef.dynamicRatio);
		}

		private void OnDrawGizmos ()
		{
			if (debug) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere (currTipPos, radius);
			}
		}
	}
}
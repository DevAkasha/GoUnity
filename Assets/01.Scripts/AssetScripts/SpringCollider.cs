﻿
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class SpringCollider : MonoBehaviour
	{
		//반지름
		public float radius = 0.5f;

		private void OnDrawGizmosSelected ()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, radius);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace sumstudio
{

	public class MoveDemo : MonoBehaviour
	{
		
		public float _scrollSpeed;


		void Update()
		{
			float _scroll = Input.GetAxis("Mouse ScrollWheel");

			if ((transform.position.y >= 0 && Mathf.Sign(_scroll) == 1) || (transform.position.y <= 297.5f && Mathf.Sign(_scroll) == -1))
			{
				transform.Translate(0, -_scroll * _scrollSpeed, 0, Space.World);
			}
		}

	}

}
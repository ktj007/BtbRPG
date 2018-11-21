using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace btbrpg.ui
{
	public class MouseFollower : MonoBehaviour
	{
        
		void Update()
		{
			transform.position = Input.mousePosition;
		}

	}
}

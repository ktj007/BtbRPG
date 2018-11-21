using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace btbrpg.characters
{
	[CreateAssetMenu(menuName = "BtbRPG/Characters/Character")]
	public class Character : ScriptableObject
	{
		public int agility = 10;
		public int StartingAP {
			get {
				return agility;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEmotionState { Joy, Surprised, Sad };
public class EmotionGem : MonoBehaviour
{
	public EEmotionState state;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log(this.state.ToString() + " 구슬이 " + coll.gameObject.name + "에 충돌");
		Destroy(this.gameObject);
	}

}

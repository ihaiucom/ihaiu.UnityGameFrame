using UnityEngine;
using System.Collections;

public class LoopRotation : MonoBehaviour 
{
	public Vector3 velocity;
	private Vector3 _value;
	void Start () 
	{
		_value = transform.localRotation.eulerAngles;
	}

	void Update () 
	{
		_value = _value + velocity * Time.deltaTime;

		transform.localRotation = Quaternion.Euler(_value);
	}
}
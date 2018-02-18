using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour
{
	public float Speed = 3f;
	public Transform Follow;

	Transform thisTransform;

	void Awake()
	{
		thisTransform = transform;
	}

	void Update()
	{
		if (Follow)
			thisTransform.position = Vector3.Lerp(thisTransform.position, Follow.position, Speed * Time.deltaTime);
	}
}

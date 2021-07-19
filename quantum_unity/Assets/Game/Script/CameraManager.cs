using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static private CameraManager instance;

	public Camera mainCamera;


	private void Awake()
	{
		instance = this;

	}

	public static CameraManager GetInstance()
	{
		return instance;
	}


}

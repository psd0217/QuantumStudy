using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour
{

	public LocalInput QuantumInput;


    public void OnClickWeaponSlot00()
	{
		QuantumInput.SelectedWeaponSlot = 0;
	}
	public void OnClickWeaponSlot01()
	{
		QuantumInput.SelectedWeaponSlot = 1;
	}
}

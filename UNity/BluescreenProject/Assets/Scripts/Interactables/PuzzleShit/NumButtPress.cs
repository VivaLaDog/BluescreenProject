using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumButtPress : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI field;
    Action ButtonPressed => OnButtonPressed;
    int val;
    int numAmount = 0;
    void OnButtonPressed()
    {
        field.text += $"{val} ";
        numAmount++;
        if(numAmount == 7)
        {
            //ask the numpad for code check, if return false then make error, if return true then write good or something
        }
    }
    public void Pressed(int value)
    {
        val = value;
        ButtonPressed.Invoke();
    }
}

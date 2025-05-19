using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumButtPress : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI field;
    [SerializeField] NumericCode numPad;

    string codeString = string.Empty;
    protected Action ButtonPressed => OnButtonPressed;
    public Action OpenPad => OnOpenPad;

    private void OnOpenPad()
    {
        gameObject.SetActive(true);
    }
    bool pressed = false;
    float timer = 1.5f;
    Button[] buttons;
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>(true);
    }
    private void Update()
    {
        if (pressed)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                foreach (Button button in buttons)
                {
                    button.enabled = true;
                }
                pressed = false;
                gameObject.SetActive(false);
                field.text = string.Empty;
                codeString = string.Empty;
                numPad.CauseMayhemInCode();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                timer = 1.5f;
            }
        }
    }

    int val;
    int numAmount = 0;
    void OnButtonPressed()
    {
        if (numPad.GetCode().ToString().Length <= numAmount)
            return;

        field.text += $"{val}";
        codeString += $"{val}";
        numAmount++;
        if(numAmount == 6)
        {
            foreach (Button button in buttons)
            {
                button.enabled = false;
            }
            numAmount = 0;
            int a = int.Parse(codeString);
            if (numPad.CheckCode(a))
            { 
                field.text = "* GOOD *";
                pressed = true;
                numPad.PassDoor();
            }
            else
            {
                field.text = "* ERROR *";
                pressed = true;
            }
            
            //ask the numpad for code check, if return false then make error, if return true then write good or something
        }
    }
    public void Pressed(int value)
    {
        val = value;
        ButtonPressed.Invoke();
    }
}

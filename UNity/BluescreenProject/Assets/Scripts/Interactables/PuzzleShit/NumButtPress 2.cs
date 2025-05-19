using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumButtPress2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI field;
    [SerializeField] NumericCode2 numPad;
    [SerializeField] float timer = 1.5f;
    public bool confirm;

    string codeString = string.Empty;
    protected Action ButtonPressed => OnButtonPressed;
    public Action OpenPad => OnOpenPad;

    private void OnOpenPad()
    {
        gameObject.SetActive(true);
    }
    bool pressed = false;
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
            if (timer <= 0)
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
        if (numAmount == numPad.GetCode().ToString().Length && !confirm) 
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
        if(field.text == "Funny. . ." || field.text == "Calling. . ." || field.text == "Unknown")
        {
            field.text = "";
            codeString = "";
            numAmount = 0;
        }
        val = value;
        ButtonPressed.Invoke();
    }
    public void CallOrDeny(bool ye)
    {
        if (ye)
            CheckNumber();
        else
            BuggerOff();
    }

    private void BuggerOff()
    {
        numAmount = 0;
        pressed = false;
        gameObject.SetActive(false);
        field.text = string.Empty;
        codeString = string.Empty;
        numPad.CauseMayhemInCode();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CheckNumber()
    {
        if (numPad.CheckCode(int.Parse(codeString)) && confirm)
        {
            field.text = "Calling . . .";
            //call sound
            timer = 5f; //sound length
            pressed = true;
            numPad.PassDoor();
        }
        else if(codeString == "911" || codeString == "112")
        {
            field.text = "Funny. . .";
        }
        else
        {
            field.text = "Unknown";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class RotaryPadLocks : MonoBehaviour
{
    [SerializeField] private short currentNumber = 0;
    [SerializeField] private short currentPlace = 0;
    [SerializeField] private short currentFullRotations = 0;
    [SerializeField] private short currentNumbersPassed = 0;

    [SerializeField] private float currentRotation = 0;
    [SerializeField] private float rotationSpeed = 9;

    [SerializeField] private short[] combination = new short[3];
    [SerializeField] private short[] currentInputs = new short[3];
    //[SerializeField] private bool[] direction = new short[3];

    [SerializeField] private RectTransform dial;
    [SerializeField] private Animator animator;

    private PlayerControls playerControls;
    private bool holding = false;
    private bool speedUp = false;
    private bool direction = false; //false = left
    public bool locked = true;
    private Transform[] pieces;

    private void Awake()
    {
        playerControls ??= new();
        dial ??= new();
        animator.speed = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControls.RotaryLock.Rotation.performed += LockController;
        playerControls.RotaryLock.Rotation.started += ToggleLock;
        playerControls.RotaryLock.Rotation.canceled += ToggleLock;
        playerControls.RotaryLock.SpeedUp.started += SpeedUp;
        playerControls.RotaryLock.SpeedUp.canceled += SpeedUp;
        playerControls.RotaryLock.Exit.performed += Exit;

        //playerControls.RotaryLock.Rotation. += LockController;
        pieces = GetComponentsInChildren<Transform>();
        Deactivate();
    }

    // Update is called once per frame
    [System.Obsolete]
    void LateUpdate()
    {
        var tempDir = direction;
        //Only allow player to interact with the lock if the lock is actually locked
        if (holding && (dial.rotation.z != currentRotation) && locked) 
        {
            var tempNum = currentNumber;
            Vector3 rotation = new Vector3(0, 0, currentRotation * Time.deltaTime * (speedUp ? 2 : 1));
            
            dial.Rotate(rotation);
            currentNumber = (short)Mathf.RoundToInt((dial.localEulerAngles.z) / 9);

            
            
            direction = (currentRotation >= 0);

            if (direction != tempDir) 
            { 
                currentPlace++;
                currentFullRotations = 0;
                currentNumbersPassed = 0;
            }

            currentPlace %= 3;
            if (currentNumber != tempNum) currentNumbersPassed++;
            currentFullRotations = (short)(currentNumbersPassed / 40);

        }
        else if (!holding)
        {
            var desiredRotation = Quaternion.Euler(0, 0, currentNumber * 9);
            dial.localRotation = Quaternion.Slerp(desiredRotation, dial.localRotation, (rotationSpeed * 0.00000000001f * Time.deltaTime));
        }

        switch (currentPlace)
        {
            case 0:
                if ((!direction) && currentFullRotations >= 3) currentInputs[0] = currentNumber;
                else if ((direction)) ResetLock();
                
                break;
             case 1:
                if (direction && currentPlace == 1 && currentFullRotations == 1) currentInputs[1] = currentNumber;
                else if (!direction || currentFullRotations > 1) ResetLock();
                break;
            case 2:
                if (!direction && currentFullRotations < 1)
                {
                    currentInputs[2] = currentNumber;

                    if (CheckCombo())
                    {
                        locked = false;
                        animator.speed = 1;
                        //testing
                        Invoke("Deactivate", 2);
                    }
                }
                else if (direction || currentFullRotations >= 1)
                {
                    ResetLock();
                }
                break;
        }
    }
    public void Activate()
    {
        playerControls.RotaryLock.Enable();
        FindAnyObjectByType<PlayerController>().DeactivateControls();

        //im tired don't kill me plz
        foreach (var piece in pieces)
        {
            piece.gameObject.SetActive(true);
        }
    }
    public void Deactivate()
    {
        playerControls.RotaryLock.Disable();
        FindAnyObjectByType<PlayerController>().ActivateControls();

        foreach (var piece in pieces)
        {
            piece.gameObject.SetActive(false);
        }
    }
    public bool CheckCombo()
    {
        bool output = true;
        for (int i = 0; i < combination.Length; i++) 
        {
            output = (combination[i] == currentInputs[i]);
        }
        return output;
    }
    public void LockController(InputAction.CallbackContext context)
    {
        float axisValue = context.ReadValue<float>();
        axisValue = Mathf.Sign(axisValue);
        currentRotation = axisValue * rotationSpeed;


        //Vector3 rotation = new Vector3(0, 0, currentRotation);
        //dial.Rotate(rotation);
    }
    private void ToggleLock(InputAction.CallbackContext context)
    {
        holding = !holding;
    }
    public void SpeedUp(InputAction.CallbackContext context)
    {
        if (context.started) speedUp = true;
        if (context.canceled) speedUp = false;
    }
    public void Exit(InputAction.CallbackContext context)
    {
        Deactivate();
    }
    private void ResetLock()
    {
        currentInputs = new short[3];
        currentPlace = 0;
    }
    public short[] GetCombo()
    {
        return combination;
    }
    public int[] GetComboInt()
    {
        int[] combo = new int[3];
        combo[0] = combination[0];
        combo[1] = combination[1];
        combo[2] = combination[2];
        return combo;
    }
    public Vector3 GetComboVec3()
    {
        Vector3 combo = new();
        combo.x = combination[0];
        combo.y = combination[1];
        combo.z = combination[2];
        return combo;
    }
    public void GenerateRandomCombo()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            combination[i] = GenerateUniqueNumber(); 
        }
    }

    private short GenerateUniqueNumber()
    {
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        bool unique = false;
        short output = 0;
        while (!unique)
        {
            output = (short)rand.Next(40);
            if (!combination.Contains(output)) unique = true;
        }
        return output;
    }
    private void OnDestroy()
    {
        playerControls.RotaryLock.Disable();
    }
}

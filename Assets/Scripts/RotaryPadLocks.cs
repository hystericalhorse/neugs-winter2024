using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private PlayerControls playerControls;
    private bool holding = false;
    private bool direction = false; //false = left

    private void Awake()
    {
        playerControls ??= new();
        dial ??= new();
    }
    // Start is called before the first frame update
    void Start()
    {
        ////For DEBUGGING don't kill me!
        combination[0] = 5;
        combination[1] = 35;
        combination[2] = 2;

        currentInputs[0] = 5;
        currentInputs[1] = 35;
        currentInputs[2] = 2;

        playerControls.RotaryLock.Rotation.performed += LockController;
        playerControls.RotaryLock.Rotation.started += ToggleLock;
        playerControls.RotaryLock.Rotation.canceled += ToggleLock;
        //playerControls.RotaryLock.Rotation. += LockController;

        playerControls.RotaryLock.Enable();
    }

    // Update is called once per frame
    [System.Obsolete]
    void LateUpdate()
    {
        var tempDir = direction;
        if (holding && (dial.rotation.z != currentRotation)) 
        {
            var tempNum = currentNumber;
            Vector3 rotation = new Vector3(0, 0, currentRotation * Time.deltaTime);
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
            //Debug.Log(Mathf.Abs(currentNumber - tempNum));
            if (currentNumber != tempNum) currentNumbersPassed++;//= (short)Mathf.Abs(currentNumber - tempNum);
            currentFullRotations = (short)(currentNumbersPassed / 40);

        }
        else if (!holding)
        {
            var desiredRotation = Quaternion.Euler(0, 0, currentNumber * 9);
            dial.localRotation = Quaternion.Slerp(desiredRotation, dial.localRotation, (rotationSpeed * 0.00000000001f * Time.deltaTime));
        }

        if ((!direction && currentPlace == 0) && currentFullRotations >= 3) currentInputs[0] = currentNumber;
        else if ((direction && currentPlace == 0))
        {
            currentInputs = new short[3];
            currentPlace = 0;
        }

        if (direction && currentPlace == 1 && currentFullRotations == 1) currentInputs[1] = currentNumber;
        else if ((!direction && currentPlace == 1) || (currentFullRotations > 1 && currentPlace == 1))
        {
            currentInputs = new short[3];
            currentPlace = 0;
        }

        if ((!direction && currentPlace == 2) && (currentFullRotations < 1 && currentPlace == 2))
        {
            currentInputs[2] = currentNumber;

            if (CheckCombo()) Debug.Log("YIPPEEEEEEEEEEE");
        }
        else if ((direction && currentPlace == 2) || (currentFullRotations >= 1 && currentPlace == 2))
        {
            currentInputs = new short[3];
            currentPlace = 0;
        }
    }

    public bool CheckCombo()
    {
        bool output = true;
        for (int i = 0; i < combination.Length; i++) 
        {
            output = (combination[0] == currentInputs[0]);
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
    public void ToggleLock(InputAction.CallbackContext context)
    {
        holding = !holding;
    }
    private void OnDestroy()
    {
        playerControls.RotaryLock.Disable();
    }
}

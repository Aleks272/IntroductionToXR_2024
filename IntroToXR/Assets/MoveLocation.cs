using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveLocation : MonoBehaviour
{
    public bool inStartingPos;
    public InputActionReference toggleReference;
    // Start is called before the first frame update
    void Start()
    {
        // player in starting position, set to true
        inStartingPos = true;
        toggleReference.action.Enable();
        toggleReference.action.performed += TogglePosition;
    }

    void TogglePosition(InputAction.CallbackContext context) {
        if (inStartingPos){
            this.transform.position = new Vector3(-16f, 16f, -20f);
            this.transform.LookAt(new Vector3(0, 7.5f, 0));
            inStartingPos = false;
        }
        else {
            this.transform.position = new Vector3(-2f, 9f, 1.3f);
            this.transform.LookAt(new Vector3(-2f, 9f, 2f));
            inStartingPos = true; 
        }
    }
}

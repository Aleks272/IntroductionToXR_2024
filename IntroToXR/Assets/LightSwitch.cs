using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light lightSwitch;
    public InputActionReference toggleReference;
    // Start is called before the first frame update
    void Start() {
        lightSwitch = GetComponent<Light>();
        toggleReference.action.Enable();
        toggleReference.action.performed += (context) => {ToggleColor();};
    }
    void ToggleColor() {
        lightSwitch = GetComponent<Light>();
        if (lightSwitch.color == Color.red) {
            lightSwitch.color = Color.blue;
        }
        else {
            lightSwitch.color = Color.red;
        }
    }
}

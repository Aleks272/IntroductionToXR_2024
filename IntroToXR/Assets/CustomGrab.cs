using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // TODO Lasin kulma oikein? Position päivitys
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    public UnityEngine.Vector3 lastFramePos;

    public UnityEngine.Quaternion lastFrameRot;

    public UnityEngine.Vector3 deltaPosition;
    public UnityEngine.Quaternion deltaRotation;

    public UnityEngine.Vector3 vectorToObject;
    public UnityEngine.Vector3 rotatedVector;
    public UnityEngine.Vector3 normalisedVector;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
        lastFramePos = transform.position;
        lastFrameRot = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to current here
                deltaPosition = transform.position - lastFramePos;
                deltaRotation = transform.rotation * UnityEngine.Quaternion.Inverse(lastFrameRot);
                vectorToObject = grabbedObject.position - transform.position;
                rotatedVector = deltaRotation * vectorToObject - vectorToObject;
                grabbedObject.position = grabbedObject.position + deltaPosition + rotatedVector;
                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
            grabbedObject = null;

        // Should save the current position and rotation here
        lastFramePos = transform.position;
        lastFrameRot = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}

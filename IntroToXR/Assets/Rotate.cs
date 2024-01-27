using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Rotate : MonoBehaviour
{
    public float yAngle;
    // Start is called before the first frame update
    void Start()
    {
        yAngle = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, yAngle * Time.deltaTime, 0, Space.Self);
    }
}

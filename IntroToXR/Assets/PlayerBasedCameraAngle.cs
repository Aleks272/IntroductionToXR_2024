using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasedCameraAngle : MonoBehaviour
{

    public Transform maincamera;
    public Transform lens;

    public Transform magnifyingglass;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = maincamera.transform;
        lens = this.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerposition = lens.InverseTransformPoint(maincamera.position);
        Vector3 angletoglass = lens.TransformPoint(new Vector3(-playerposition.x, -playerposition.y, -playerposition.z));
        transform.LookAt(angletoglass,magnifyingglass.up);

    }
}

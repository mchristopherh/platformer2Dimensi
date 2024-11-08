using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    public static ParallaxBackground instance;
    private void Awake()
    {
        instance = this;
    }

    private Transform theCam;
    public Transform sky, treeline;
    [Range(0f, 1f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*sky.position = new UnityEngine.Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        treeline.position = new UnityEngine.Vector3(
            theCam.position.x * parallaxSpeed, 
            theCam.position.y*parallaxSpeed, 
            treeline.position.z);*/
    }

    public void MoveBackground(){
        sky.position = new UnityEngine.Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        treeline.position = new UnityEngine.Vector3(
            theCam.position.x * parallaxSpeed, 
            theCam.position.y*parallaxSpeed, 
            treeline.position.z);
    }
}

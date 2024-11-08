using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, clampMin, clampMax;
    public bool freezeVertical, freezeHorizontal, clampPosition;
    private Vector3 positionStore;
    private float halfWidth, halfHeight;
    public Camera theCam;


    // Start is called before the first frame update
    void Start()
    {
        positionStore = transform.position;
        
        clampMax.SetParent(null);
        clampMin.SetParent(null);

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void Update () 
    {

    }
    
    void LateUpdate()
    {
        transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);

        if(freezeVertical)
        {
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }

        if(freezeHorizontal)
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }

        if(clampPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth), 
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight), 
                transform.position.z);
        }

        if(ParallaxBackground.instance != null)
        {
            ParallaxBackground.instance.MoveBackground();
        }  
    }

    private void OnDrawGizmos()
    {
        if(clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(new Vector3(clampMin.position.x, clampMax.position.y, 0f), clampMax.position);
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }
}

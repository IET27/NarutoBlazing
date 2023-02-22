using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour{
    
    public static Transform transformCameraFocus;
    private Vector3 normalPosition = new Vector3(-5.64f, 1.29f, -10);
    private Vector3 offset = new Vector3(-5.64f,1.29f,0);
    private void Update() {
        if(transformCameraFocus != null){
            transform.position = new Vector3(transformCameraFocus.position.x, 
                                             transformCameraFocus.position.y, 
                                             -10);
        }else{
            transform.position = normalPosition;
        }
    }
}

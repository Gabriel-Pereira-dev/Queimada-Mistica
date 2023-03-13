using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    public bool freezeXZ = true;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       if(freezeXZ){
        transform.rotation =  Quaternion.Euler( 0f,mainCamera.transform.rotation.eulerAngles.y,0f);
       }else{
        transform.rotation = mainCamera.transform.rotation;
       }
    }
}

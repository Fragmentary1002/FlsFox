using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update(){
        //小写就是camera的transform
        transform.position=new Vector3(playerTransform.position.x,playerTransform.position.y,transform.position.z);
    }
}

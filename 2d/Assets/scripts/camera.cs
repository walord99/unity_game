using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform followTransform;
    private void Update()
    {
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
    }
}

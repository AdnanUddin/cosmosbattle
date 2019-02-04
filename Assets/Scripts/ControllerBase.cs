using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() => this.Create();
    
    public abstract void Create();

    public virtual void  Destory()
    {

    }

}
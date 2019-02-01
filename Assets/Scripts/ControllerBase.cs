using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public event EventHandler Created;

    // Start is called before the first frame update
    void Start()
    {
        this.Create();
        this.Created?.Invoke(this, EventArgs.Empty);
    }
    public abstract void Create();

}
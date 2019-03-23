using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Projectile : MonoBehaviour
{
    protected Vector3 source;
    protected Vector3 target;

    public virtual void SetSource(Vector3 source)
    {
        this.source = source;
    }

    public virtual void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Update()
    {
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ThrownEffect : MonoBehaviour
{
    public abstract void Throw();

    public int bonus = 0;
}

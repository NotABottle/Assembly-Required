using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Brick : MonoBehaviour
{
    public BoxCollider brickCollider { get; private set;}

    private void Awake(){
        brickCollider = GetComponent<BoxCollider>();
    }
}

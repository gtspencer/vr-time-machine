using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSaver : MonoBehaviour
{
    public SerializedTrasnform GetTransform()
    {
        return new SerializedTrasnform(this.transform, this.gameObject.GetComponentInChildren<PictureFrame>().url);
    }
}
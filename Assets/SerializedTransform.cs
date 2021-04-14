﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedTrasnform
{
    public float[] _position = new float[3];
    public float[] _rotation = new float[4];
    public float[] _scale = new float[3];
    public string url;

    public SerializedTrasnform()
    {

    }
    public SerializedTrasnform(Transform transform, string url)
    {
        _position[0] = transform.localPosition.x;
        _position[1] = transform.localPosition.y;
        _position[2] = transform.localPosition.z;

        _rotation[0] = transform.localRotation.w;
        _rotation[1] = transform.localRotation.x;
        _rotation[2] = transform.localRotation.y;
        _rotation[3] = transform.localRotation.z;

        _scale[0] = transform.localScale.x;
        _scale[1] = transform.localScale.y;
        _scale[2] = transform.localScale.z;

        this.url = url;
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    public GameObject _dot;
    public float _degree, _c;
    public int _n;
    public float _dotScale;

    private Vector2 CalculatePhyllotaxis(float dgree, float scale, int count)
    {
        double angle = count * (_degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);

        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);

        Vector2 vec2 = new Vector2(x, y);
        return vec2;

        // x = r * cos(angle)
        // y = r * sin(angle)
    }

    private Vector2 _phyllotaxisPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _c, _n);
            GameObject dotInstance = (GameObject)Instantiate(_dot);
            dotInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            dotInstance.transform.localScale = new Vector3(_dotScale, _dotScale, _dotScale);
            _n++;
        }
    }
}

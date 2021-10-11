using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    [Header("Audio")]
    public AudioPeer _audioPeer;
    public int _band;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    Material _material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (_audioPeer._audioBandBuffer[_band]
            * _maxScale) + _startScale, transform.localScale.z);
            // r g b
            Color _color = new Color(_audioPeer._audioBandBuffer[_band], _audioPeer._audioBandBuffer[_band], _audioPeer._audioBandBuffer[_band]);
            _material.SetColor("_EmissionColor", _color);
        }

        if (!_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (_audioPeer._audioBand[_band]
            * _maxScale) + _startScale, transform.localScale.z);
            Color _color = new Color(_audioPeer._audioBandBuffer[_band], _audioPeer._audioBandBuffer[_band], _audioPeer._audioBandBuffer[_band]);
            _material.SetColor("_EmissionColor", _color);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    [Header("Audio")]
    public AudioPeer _audioPeer;
    public float _startScale, _maxScale;
    public bool _useBuffer;
    Material _material;
    public float _red, _green, _blue;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3((_audioPeer._Amplitude * _maxScale) + _startScale,
                                               (_audioPeer._Amplitude * _maxScale) + _startScale,
                                               (_audioPeer._Amplitude * _maxScale) + _startScale);
            // r g b
            Color _color = new Color(_red * _audioPeer._Amplitude, _green * _audioPeer._Amplitude, _blue * _audioPeer._Amplitude);
            _material.SetColor("_EmissionColor", _color);
        }

        if (!_useBuffer)
        {
            transform.localScale = new Vector3((_audioPeer._Amplitude * _maxScale) + _startScale,
                                               (_audioPeer._Amplitude * _maxScale) + _startScale,
                                               (_audioPeer._Amplitude * _maxScale) + _startScale);
            // r g b
            Color _color = new Color(_red * _audioPeer._Amplitude, _green * _audioPeer._Amplitude, _blue * _audioPeer._Amplitude);
            _material.SetColor("_EmissionColor", _color);
        }
    }
}

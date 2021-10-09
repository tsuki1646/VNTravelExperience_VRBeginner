using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis2 : MonoBehaviour
{
    public float _degree, _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;

    //Lerping
    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    private Vector3 _startPosition, _endPosition;
    private float _timeStartedLerping;

    private int _number;
    private int _currentIteration;
    private TrailRenderer _trailRenderer;

    private Vector2 CalculatePhyllotaxis(float dgree, float scale, int number)
    {
        double angle = number * (_degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(number);

        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);

        Vector2 vec2 = new Vector2(x, y);
        return vec2;

        // x = r * cos(angle)
        // y = r * sin(angle)
    }

    private Vector2 _phyllotaxisPosition;

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;
        _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, _number);
        _startPosition = this.transform.localPosition;
        _endPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
    }

    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _number = _numberStart;
        transform.localPosition = CalculatePhyllotaxis(_degree, _scale, _number);
        if (_useLerping)
        {
            StartLerping();
        }
    }

    private void FixedUpdate()
    {
        if (_useLerping)
        {
            if (_isLerping)
            {
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / _intervalLerp;
                transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                if (percentageComplete >= 0.97f)
                {
                    transform.localPosition = _endPosition;
                    _number += _stepSize;
                    _currentIteration++;
                    if (_currentIteration <= _maxIteration)
                    {
                        StartLerping();
                    }
                    else
                    {
                        _isLerping = false;
                    }
                }
            }
        }
        else
        {
            _phyllotaxisPosition = CalculatePhyllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            _number+= _stepSize;
            _currentIteration++;
        }
        
    }
}

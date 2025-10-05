using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskObject : MonoBehaviour
{
    [SerializeField] private Transform _diskObject;
    [SerializeField] private DisksManager _disksManager;
    [SerializeField] private float _stepAngle = 90f; // шаг поворота

    private int _angleState = 1;   // текущее состояние
    private float _baseAngle;      // стартовый угол по X
    private bool _isRotating = false;
    private bool _firstTime = true;
    public int AngleState => _angleState;
    public Transform Disk => _diskObject;

    void Start()
    {
        // фиксируем стартовый угол по оси X
        _baseAngle = _diskObject.localEulerAngles.x;
    }

    public void RotateDisk()
    {
        if (_isRotating) return;

        _isRotating = true;

        // вычисляем целевой угол
        float targetX =_baseAngle+ _angleState * _stepAngle;
        Vector3 targetRotation = new Vector3(targetX, 0, 0);

        _diskObject
            .DOLocalRotate(targetRotation, 0.3f, RotateMode.Fast)
            .OnComplete(() =>
            {
                _isRotating = false;

                _angleState++;
                if (_angleState * _stepAngle >= 360f)
                    _angleState = 0; // цикл по кругу
                Debug.Log(AngleState);
                _disksManager.CheckDisks();

            });
    }
}

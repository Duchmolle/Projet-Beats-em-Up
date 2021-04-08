using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] TransformData _player;
    [SerializeField] float _followDamp;
    [SerializeField] float _camSpeed;
    [SerializeField] GameObjectVariable _cameraData;


    private Vector3 velocity = Vector3.zero;
    private bool _isFollow = true;

    private void Awake()
    {
        _cameraData.gameObjectData = gameObject;
    }

    public void UnlockCam(bool set)
    {
        _isFollow = set;
    }

    private void Update()
    {      
        if (_isFollow)
        {
            float xtarget = _player.value.position.x;
            Vector3 playertarget = new Vector3(xtarget, transform.position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, playertarget, ref velocity, _followDamp);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScore : MonoBehaviour
{
    [SerializeField] IntVariable _player;
    [SerializeField] int _scoreAmount;

    public void DoCollect()
    {
        _player.value += _scoreAmount;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject _ennemyPrefab;
    [SerializeField] Transform[] _spawners;
    [SerializeField] Transform[] _roundSpot;
    [SerializeField] GameObjectVariable _camData;
    [SerializeField] GameObject _spawnerHolder;
    [SerializeField] GameObject _goArrow;
    [SerializeField] PlayerHP _playerLives;
    [SerializeField] PlayerHP _playerMaxLives;
    [SerializeField] GameObject _playerPrefab;
    
    private PlayerAttack _player;
    private CameraFollow _cam;
    private Transform _nextRound;
    private int _currentEnnemies;
    private int _numberOfEnnemies;
    private int nextIndex;
    private bool _waveStarted;


    private void Awake()
    {
        PlayerAttack[] tab = Resources.FindObjectsOfTypeAll<PlayerAttack>();
            _player = tab[0];
        _playerLives.lives = _playerMaxLives.lives;
    }

    private void Start()
    {
        nextIndex = 0;
        _cam = _camData.gameObjectData.transform.GetComponent<CameraFollow>();
        _cam.UnlockCam(false);
        _cam.transform.position = _roundSpot[0].position;
        _nextRound = _roundSpot[0];

    }

    private void Update()
    {

        if (Mathf.Approximately(_cam.transform.position.x, _nextRound.position.x))
        {
            _goArrow.SetActive(false);
            StartWave();
            _cam.UnlockCam(false);
            SetNextRound();
        }
        EndWave();
    }

    public void ResetPlayer()
    {
            Debug.Log("PlayerReset");
            _player.transform.position = new Vector3(_cam.transform.position.x, _cam.transform.position.y, 0f);
            _player.isDead = false;
            _playerLives.lives--;
            _player.gameObject.SetActive(true);
    }

    private void StartWave()
    {
        Debug.Log("Starting Wave n° " + nextIndex);
        _waveStarted = true;
        _spawnerHolder.transform.position = (Vector2)_camData.gameObjectData.transform.position;
        foreach (Transform spawner in _spawners)
        {
            Instantiate(_ennemyPrefab, spawner.position, Quaternion.identity, _spawnerHolder.transform);
        }
    }

    private void EndWave()
    {
        if (FindObjectsOfType<EnnemyMovement>().Length <= 0)
        {
            _goArrow.SetActive(true);
            _waveStarted = false;
            _cam.UnlockCam(true);
        }
    }

    private void SetNextRound()
    {
        nextIndex++;
        _nextRound = _roundSpot[nextIndex];
    }



}

using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _sideBlock;
    [SerializeField] private GameObject _breakableBlock;
    [SerializeField] private GameObject _PbLine;
    [SerializeField] private float _wallsNumber = 10;
    [SerializeField] private float _levelSize = 1000;
    [SerializeField] private int _firstSpawnHeightOffset = -10;
    private Transform _bigWallLeft;
    private Transform _bigWallRight;
    private float _currentWallHeight = 5;

    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    private float _seed;

    [SerializeField] private Transform _sideParent;
    [SerializeField] private Transform _platformParent;

    private Vector3Int _lastGeneratedPos;

    private void Start()
    {
        _seed = Random.Range(-100000, 1000000);
        _currentWallHeight = _wallsNumber / 2;
        GenerateSideWalls();
        GeneratePlatforms(true);
        CheckPlayerPosForGen();
        GameManager.Instance.GameStartedEvent?.AddListener(SpawnPBLine);
    }

    private void SpawnPBLine()
    {
        Instantiate(_PbLine, new Vector3(0, -PlayerPrefs.GetInt("MaxDistance", 100), _PbLine.transform.position.z), Quaternion.identity);
    }

    private void GenerateSideWalls()
    {
        _bigWallLeft = new GameObject().transform; _bigWallLeft.name = "BigWallLeft"; _bigWallLeft.parent = _sideParent;
        _bigWallRight = new GameObject().transform; _bigWallRight.name = "BigWallRight"; _bigWallRight.parent = _sideParent;
        for (int i = 0; i < _wallsNumber; i++)
        {
            float xOffset = 0.45f;
            Instantiate(_sideBlock, new Vector2(Camera.main.orthographicSize/2 - xOffset, _currentWallHeight), Quaternion.identity, _bigWallRight);
            Instantiate(_sideBlock, new Vector2(-Camera.main.orthographicSize/2 + xOffset, _currentWallHeight), Quaternion.identity, _bigWallLeft);
            _currentWallHeight -= 1;
        }
    }

    private void GeneratePlatforms(bool firstGen)
    {
        Vector3Int spawnPos = new Vector3Int((int)(-Camera.main.orthographicSize / 2), (int)_player.position.y + _firstSpawnHeightOffset, (int)_player.position.z);
        if (!firstGen)
        {
            spawnPos = _lastGeneratedPos;
        }
        for (int i = 0; i < _levelSize; i++)
        {
            for (int y = 0; y < (int)Camera.main.orthographicSize - 2; y++)
            {
                float spawnProbSub = 0;
                float screenPercent = 0.1f;
                float lessBlockMidPercent = 0.15f;
                if (Mathf.Abs(spawnPos.x) < Camera.main.orthographicSize * screenPercent) spawnProbSub += lessBlockMidPercent;

                if (Mathf.PerlinNoise(spawnPos.x/10f + _seed, spawnPos.y/10f + _seed) > 0.65f + spawnProbSub)
                {
                    Instantiate(_breakableBlock, spawnPos, Quaternion.identity, _platformParent);
                }
                else
                {
                    GenerateEnemy(spawnPos);
                }
                spawnPos.x += 1;
            }
            spawnPos.x = (int)(-Camera.main.orthographicSize / 2) + 1;
            spawnPos.y -= 1;
        }
        _lastGeneratedPos = spawnPos;
        CheckPlayerPosForGen();
    }

    private void DestroyFarObjects()
    {
        float baseCheckHeight = 2;
        float endCheckHeight = 6;
        Vector2 areaA = new Vector2((int)(-Camera.main.orthographicSize / 2), _lastGeneratedPos.y +(_levelSize * baseCheckHeight));
        Vector2 areaB = new Vector2((int)(Camera.main.orthographicSize / 2), _lastGeneratedPos.y + (_levelSize * endCheckHeight));
        foreach (Collider2D collider in Physics2D.OverlapAreaAll(areaA, areaB))
        {
            Destroy(collider.transform.parent.gameObject);
        }
    }

    private void CheckPlayerPosForGen()
    {
        if (Mathf.Abs(_lastGeneratedPos.y - _player.transform.position.y) < _levelSize / 2)
        {
            GeneratePlatforms(false);
            DestroyFarObjects();
        }
        Invoke("CheckPlayerPosForGen", 0.02f);
    }

    private void GenerateEnemy(Vector3Int spawnPos)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 0.5f)
        {
            Instantiate(_enemyList[Random.Range(0, _enemyList.Count)], spawnPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        UpdateSideWalls();
    }

    private void UpdateSideWalls()
    {
        _bigWallLeft.position = new Vector3(_bigWallLeft.position.x, _player.position.y, _bigWallLeft.position.z);
        _bigWallRight.position = new Vector3(_bigWallRight.position.x, _player.position.y, _bigWallRight.position.z);
    }
}

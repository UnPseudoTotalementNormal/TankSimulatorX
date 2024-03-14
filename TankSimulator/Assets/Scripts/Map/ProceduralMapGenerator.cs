using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class ProceduralMapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _block;
    [SerializeField] private float _wallsNumber = 10;
    [SerializeField] private float _levelSize = 1000;
    private Transform _bigWallLeft;
    private Transform _bigWallRight;
    private float _currentWallHeight = 5;

    private float _seed;

    [SerializeField] private Transform _sideParent;
    [SerializeField] private Transform _platformParent;

    private void Start()
    {
        _seed = Random.Range(-100000, 1000000);
        _currentWallHeight = _wallsNumber / 2;
        GenerateSideWalls();
        GeneratePlatforms();
    }

    private void GenerateSideWalls()
    {
        _bigWallLeft = new GameObject().transform; _bigWallLeft.name = "BigWallLeft"; _bigWallLeft.parent = _sideParent;
        _bigWallRight = new GameObject().transform; _bigWallRight.name = "BigWallRight"; _bigWallRight.parent = _sideParent;
        for (int i = 0; i < _wallsNumber; i++)
        {
            float xOffset = 0.45f;
            Instantiate(_block, new Vector2(Camera.main.orthographicSize/2 - xOffset, _currentWallHeight), Quaternion.identity, _bigWallRight);
            Instantiate(_block, new Vector2(-Camera.main.orthographicSize/2 + xOffset, _currentWallHeight), Quaternion.identity, _bigWallLeft);
            _currentWallHeight -= 1;
        }
    }

    private void GeneratePlatforms()
    {
        Vector3Int spawnPos = new Vector3Int((int)(-Camera.main.orthographicSize / 2), (int)_player.position.y, (int)_player.position.z);
        for (int i = 0; i < _levelSize; i++)
        {
            for (int y = 0; y < (int)Camera.main.orthographicSize; y++)
            {
                float spawnProbSub = 0;
                float screenPercent = 0.1f;
                float lessBlockMidPercent = 0.15f;
                if (Mathf.Abs(spawnPos.x) < Camera.main.orthographicSize * screenPercent) spawnProbSub += lessBlockMidPercent;

                if (Mathf.PerlinNoise(spawnPos.x/10f + _seed, spawnPos.y/10f + _seed) > 0.65f + spawnProbSub)
                {
                    Instantiate(_block, spawnPos, Quaternion.identity, _platformParent);
                }
                spawnPos.x += 1;
            }
            spawnPos.x = (int)(-Camera.main.orthographicSize / 2);
            spawnPos.y -= 1;
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

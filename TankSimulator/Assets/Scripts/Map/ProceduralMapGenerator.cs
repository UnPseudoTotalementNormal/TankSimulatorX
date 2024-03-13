using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _block;
    [SerializeField] private float _wallsNumber = 10;
    private Transform _bigWallLeft;
    private Transform _bigWallRight;
    private float _currentWallHeight = 5;

    private float _seed;

    [SerializeField] private Transform _sideParent;
    [SerializeField] private Transform _platformParent;

    private void Start()
    {
        _seed = Random.Range(-100000, 1000000);
        _currentWallHeight = _currentWallHeight / 2;
        GenerateSideWalls();
        GeneratePlatforms();
    }

    private void GenerateSideWalls()
    {
        _bigWallLeft = new GameObject().transform; _bigWallLeft.name = "BigWallLeft"; _bigWallLeft.parent = _sideParent;
        _bigWallRight = new GameObject().transform; _bigWallRight.name = "BigWallRight"; _bigWallRight.parent = _sideParent;
        for (int i = 0; i < _wallsNumber; i++)
        {
            Instantiate(_block, new Vector2(Camera.main.orthographicSize/2, _currentWallHeight), Quaternion.identity, _bigWallRight);
            Instantiate(_block, new Vector2(-Camera.main.orthographicSize/2, _currentWallHeight), Quaternion.identity, _bigWallLeft);
            _currentWallHeight -= 1;
        }
    }

    private void GeneratePlatforms()
    {
        Vector3 spawnPos = _player.position;
        spawnPos = new Vector3((int)(-Camera.main.orthographicSize / 2), (int)spawnPos.y, (int)spawnPos.z);
        for (int i = 0; i < 100; i++)
        {
            for (int y = 0; y < (int)Camera.main.orthographicSize; y++)
            {
                if (Mathf.PerlinNoise(spawnPos.x/10f + _seed, spawnPos.y/10f + _seed) > 0.6f)
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

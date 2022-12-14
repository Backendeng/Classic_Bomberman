using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerPrefabs;
    [SerializeField]
    public Vector2[] PlayersPositions;
    [SerializeField]
    private Vector2 _playersPositionOffset = new Vector2(0.6f, 1.2f);
    [SerializeField]
    private string[] _playerColors = { "blue", "green", "orange", "pink" };

    private readonly string ALIVE_GAME_OBJECT = "Alive";
    private List<int> _playersIndexes = new List<int>();
    public Player[] Players { get; private set; }
    private GameObject[] _playersAliveGO;
    private Player[] _alivePlayers;
    private EnvironmentHazardGenerator _environmentHazardGenerator;

    private void Start()
    {
        _environmentHazardGenerator = FindObjectOfType<EnvironmentHazardGenerator>();

        _environmentHazardGenerator.LevelGeneratedEvent += OnLevelGenerated;

        _playersIndexes = CharacterSelection.Instance.CharacterIndexes;

        Players = new Player[4];
        _playersAliveGO = new GameObject[4];
    }

    private void OnLevelGenerated()
    {
        _environmentHazardGenerator.LevelGeneratedEvent -= OnLevelGenerated;

        for (int i = 0; i < _playersIndexes.Count(); i++)
        {
            SpawnPlayer(_playersIndexes[i]);
        }

        FindObjectOfType<PlayerStatusUIManager>().SetUIElements(Players);
        _alivePlayers = Players.Where(player => player != null).ToArray();
    }

    private void SpawnPlayer(int id)
    {
        GameObject playerGO = Instantiate(_playerPrefabs[id], PlayersPositions[id] + _playersPositionOffset, Quaternion.identity);
        playerGO.transform.parent = gameObject.transform;
        Player player = playerGO.GetComponent<Player>();

        player.CreateStatsManager(id);
        player.PlayerStatsManager.DeathEvent += OnPlayerDeath;
        player.PlayerStatsManager.PermamentDeathEvent += OnPermamentDeath;
        player.SetColorForAnimation(_playerColors[id]);

        Players[id] = player;
        _playersAliveGO[id] = player.transform.Find(ALIVE_GAME_OBJECT).gameObject;
    }

    private void OnPlayerDeath(int id)
    {
        Players[id].RemoveAllStatuses();
        _playersAliveGO[id].transform.position = PlayersPositions[id] + _playersPositionOffset;
    }

    private void OnPermamentDeath(int id)
    {
        Players[id].PlayerStatsManager.DeathEvent -= OnPlayerDeath;
        Players[id].PlayerStatsManager.PermamentDeathEvent -= OnPlayerDeath;

        _alivePlayers = _alivePlayers.Where(player => !player.PlayerStatsManager.IsPermamentDead).ToArray();

        if (_alivePlayers.Length == 1)
        {
            FindObjectOfType<WinnerManager>().SetWinnerSprite(_alivePlayers[0]);
            FindObjectOfType<LevelManager>().LoadWinningScene();
        }

        Destroy(Players[id].gameObject);
    }
}

using System;
using JetBrains.Annotations;
using UnityEngine;

public class Game : Scene
{
    private int _score;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private AudioSource _music;

    [UsedImplicitly]
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
#if !UNITY_WEBGL
            FindObjectOfType<DiscordController>()?.UpdatePresence(new DiscordRpc.RichPresence
            {
                details = $"Score: {_score}"
            });
#endif
        }
    }

    [UsedImplicitly]
    public bool Paused { get; private set; }

    [UsedImplicitly]
    public void TogglePause()
    {
        Paused = !Paused;
        UpdatePause();
    }

    public void GameOver()
    {
        _gameoverPanel.SetActive(true);
        FindObjectOfType<EnemySpawner>().Enabled = false;
    }

    private void UpdatePause()
    {
#if !UNITY_WEBGL
        FindObjectOfType<DiscordController>()?.UpdatePresence(new DiscordRpc.RichPresence {state = Paused ? "Paused" : "Playing"});
#endif
        _pausePanel?.SetActive(Paused);
        Time.timeScale = Paused ? 0 : 1;
        if (Paused) _music.Pause(); else _music.Play();
    }

    private void Start()
    {
#if !UNITY_WEBGL
        FindObjectOfType<DiscordController>()?.UpdatePresence(new DiscordRpc.RichPresence
        {
            startTimestamp = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
            details = $"Score: {_score}"
        });
#endif
        UpdatePause();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Pause"))
            TogglePause();
    }
}
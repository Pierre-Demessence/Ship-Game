using System;
using UnityEngine;

public class MainMenu : Scene
{
    private void Start()
    {
        Time.timeScale = 1;
        var findObjectOfType = FindObjectOfType<DiscordController>();
        findObjectOfType?.onConnect.AddListener(() =>
        {
            findObjectOfType.UpdatePresence(new DiscordRpc.RichPresence
            {
                state = "Main Menu"
            });
        });
    }
}
using System;
using UnityEngine;

public class MainMenu : Scene
{
    private void Start()
    {
        Time.timeScale = 1;
        var findObjectOfType = FindObjectOfType<DiscordController>();
#if !UNITY_WEBGL
        findObjectOfType?.onConnect.AddListener(() =>
        {
            findObjectOfType.UpdatePresence(new DiscordRpc.RichPresence
            {
                state = "Main Menu"
            });
        });
#endif
    }
}
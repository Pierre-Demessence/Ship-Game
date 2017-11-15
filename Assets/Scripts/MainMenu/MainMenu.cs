using System;

public class MainMenu : Scene
{
    private void Start()
    {
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
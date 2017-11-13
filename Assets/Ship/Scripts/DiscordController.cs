using System;
using UnityEngine;
using UnityEngine.Events;

public class DiscordController : MonoBehaviour
{
    [SerializeField] private string _applicationId;
    private DiscordRpc.EventHandlers _handlers;
    [SerializeField] private string _optionalSteamId;
    private DiscordRpc.RichPresence _presence;

    public UnityEvent onConnect;

    public void UpdatePresence(DiscordRpc.RichPresence presence)
    {
        var fields = typeof(DiscordRpc.RichPresence).GetFields();

        foreach (var field in fields)
        {
            var value = field.GetValue(presence);
            var defValue = (field.FieldType.IsValueType ? Activator.CreateInstance(field.FieldType) : null);
            //Debug.Log($"Field \"{field.Name}\" of type \"{field.FieldType}\" (default value is \"{defValue})\" with value \"{value}\"");

            if (value != null && !value.Equals(defValue))
            {
                //Debug.LogWarning("Different from default -> Setting the Field");
                field.SetValueDirect(__makeref(_presence), value);
            }
        }
        DiscordRpc.UpdatePresence(ref _presence);
    }

    private void ReadyCallback()
    {
        Debug.Log("Discord: ready");
        _presence.largeImageKey = "cok";
        _presence.largeImageText = "BEST GAME EVER";
        _presence.smallImageKey = "kek";
        _presence.smallImageText = "KEKIMURUS";
        DiscordRpc.UpdatePresence(ref _presence);
        onConnect.Invoke();
    }

    private static void DisconnectedCallback(int errorCode, string message)
    {
        Debug.LogWarning($"Discord: disconnect {errorCode}: {message}");
    }

    private static void ErrorCallback(int errorCode, string message)
    {
        Debug.LogError($"Discord: error {errorCode}: {message}");
    }

    private static void JoinCallback(string secret)
    {
        Debug.Log($"Discord: join ({secret})");
    }

    private static void SpectateCallback(string secret)
    {
        Debug.Log($"Discord: spectate ({secret})");
    }

    private static void RequestCallback(DiscordRpc.JoinRequest request)
    {
        Debug.Log($"Discord: join request {request.username}: {request.userId}");
    }

    private void Update()
    {
        DiscordRpc.RunCallbacks();
    }

    private void OnEnable()
    {
        Debug.Log("Discord: init");

        _handlers = new DiscordRpc.EventHandlers();
        _handlers.readyCallback = ReadyCallback;
        _handlers.disconnectedCallback += DisconnectedCallback;
        _handlers.errorCallback += ErrorCallback;
        _handlers.joinCallback += JoinCallback;
        _handlers.spectateCallback += SpectateCallback;
        _handlers.requestCallback += RequestCallback;
        DiscordRpc.Initialize(_applicationId, ref _handlers, true, _optionalSteamId);
    }

    private void OnDisable()
    {
        Debug.Log("Discord: shutdown");
        DiscordRpc.Shutdown();
    }
}
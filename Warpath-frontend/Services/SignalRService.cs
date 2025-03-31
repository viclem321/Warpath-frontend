using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Warpath_frontend.Services;
using Warpath_frontend.Services.AppState;



namespace Warpath_frontend.Services;


public class SignalRService
{
    private HubConnection _hubConnection; private AppStateService _appStateService;

    public event Action<string> OnAttackNotificationReceived;

    public SignalRService(AppStateService appStateService)
    {
        _appStateService = appStateService;
    }

    private async Task<string> FindJwtToken()
    {
        string token = _appStateService?.user?.token ?? "";
        return token;
    }

    public async Task ConnectAsync()
    {
        _hubConnection = new HubConnectionBuilder()
           .WithUrl("http://localhost:5000/gamehub", options =>
           {
               options.AccessTokenProvider = async () => await FindJwtToken();
           })
           .WithAutomaticReconnect() // Reconnexion automatique en cas de coupure
           .Build();
        try
        {
            await _hubConnection.StartAsync();
            Debug.WriteLine("Connecté à SignalR.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur SignalR: {ex.Message}");
        }
    }

    public async Task DisconnectAsync()
    {
        await _hubConnection.StopAsync();
    }

    public async Task SendMessage(string message)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.InvokeAsync("SendMessage", message);
        }
    }

    public void ListenForMessages()
    {
        _hubConnection.On<string>("ReceiveAttackNotification", (message) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Notifie tous les abonnés que SignalR a reçu un message
                OnAttackNotificationReceived?.Invoke(message);
            });
        });
    }
}
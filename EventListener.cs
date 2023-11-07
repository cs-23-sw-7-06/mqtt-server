using System.Text;
using MQTTnet.Server;

public static class EventListener{
    public static Dictionary<string, List<Dictionary<string, string>>> receivedMessages = new();
    
    public static Task OnNewConnection(ClientConnectedEventArgs args){
        Log.Info("ESP32 with ID: " + args.ClientId + " Connected!");

        if(!receivedMessages.ContainsKey(args.ClientId)){
            receivedMessages.Add(args.ClientId, new List<Dictionary<string, string>>());
        }

        return Task.CompletedTask;
    }

    public static Task OnDisconnect(ClientDisconnectedEventArgs args){
        Log.Warning("ESP32 with ID: " + args.ClientId + " Disconnected!");

        return Task.CompletedTask;
    }

    public static Task OnNewMessage(ClientAcknowledgedPublishPacketEventArgs args){
        Log.Info("New Message!");

        return Task.CompletedTask;
    }
    public static Task OnClientSubscribed(ClientSubscribedTopicEventArgs args){
        Log.Info("Client subscribed!");

        return Task.CompletedTask;
    }

    public static Task OnClientUnsubscribed(ClientUnsubscribedTopicEventArgs args){
        Log.Info("Client unsubscribed");

        return Task.CompletedTask;
    }

    public static Task OnValidatingConnection(ValidatingConnectionEventArgs args){
        Log.Info("Validating connection");

        return Task.CompletedTask;
    }

    public static Task OnApplicationMessageNotConsumed(ApplicationMessageNotConsumedEventArgs args){
        Log.GInfo("Application message received!");
        var payload = Encoding.UTF8.GetString(args.ApplicationMessage.PayloadSegment.ToArray(), 0, args.ApplicationMessage.PayloadSegment.Count);
        var topic = args.ApplicationMessage.Topic;
        var id = args.SenderId;

        Log.Info("Node ID:  " + id);
        Log.Info("Topic:    " + topic);
        Log.Info("Payload:  " + payload);

        var messageData = new Dictionary<string, string>
        {
            { "topic", topic },
            { "payload", payload }
        };

        receivedMessages[args.SenderId].Add(messageData);

        return Task.CompletedTask;
    }
}
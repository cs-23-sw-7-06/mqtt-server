using System.Text;
using MQTTnet;
using MQTTnet.Formatter;
using MQTTnet.Packets;
using MQTTnet.Server;


public class Program {
    private MqttServer server;

    private Program(){
        // defining options:
        var options = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(4000)
            .Build();

        server = new MqttFactory().CreateMqttServer(options);
        server.ClientConnectedAsync                 += EventListener.OnNewConnection;
        server.ClientAcknowledgedPublishPacketAsync += EventListener.OnNewMessage;
        server.ClientSubscribedTopicAsync           += EventListener.OnClientSubscribed;
        server.ClientUnsubscribedTopicAsync         += EventListener.OnClientUnsubscribed;
        server.ValidatingConnectionAsync            += EventListener.OnValidatingConnection;
        server.ApplicationMessageNotConsumedAsync   += EventListener.OnApplicationMessageNotConsumed;

        server.StartAsync().GetAwaiter().GetResult();

    }

    public static void Main(String[] args){
        var program = new Program();

        Console.Write("> ");
        for(var line = Console.ReadLine(); line != "exit"; line = Console.ReadLine()){
            if(line == null) continue;
            
            switch(line.Split(" ")[0]){
                case "messages":
                    Console.WriteLine(EventListener.receivedMessages[EventListener.receivedMessages.Keys.First()][0]["topic"]);
                    break;
                case "broadcast":
                    program.Broadcast(line.Split(" ").Length > 1 ? line.Split(" ")[1] : "cmdline message", line.Split(" ").Length > 2 ? line.Split(" ")[2] : "broadcast message from command line");
                    break;
                case "list":
                    Console.WriteLine("Available devices:");
                    foreach(var device in EventListener.receivedMessages.Keys){
                        Console.WriteLine(device);
                    }
                    break;
                case "unicast":
                    if(line.Split(" ").Length == 1) break;
                    program.Unicast(line.Split(" ")[1], line.Split(" ").Length > 2 ? line.Split(" ")[2] : "cmdline message", line.Split(" ").Length > 3 ? line.Split(" ")[3] : "unicast message from command line");
                    break;
                default:
                    Console.WriteLine("No command defined by: "+line);
                    break;
            }
            Console.Write("> ");
        }
    }

    private void Broadcast(string topic, string payload){
        var message = new MqttApplicationMessage(){
            Topic = topic,
            PayloadSegment = Encoding.UTF8.GetBytes(payload)
        };

        foreach(var client in server.GetClientsAsync().GetAwaiter().GetResult()){
            client.Session.EnqueueApplicationMessageAsync(message);
        }
    }

    private void Unicast(string id, string topic, string payload){
        var client = server.GetClientsAsync().GetAwaiter().GetResult().First(client => client.Id == id);

        var message = new MqttApplicationMessage(){
            Topic = topic,
            PayloadSegment = Encoding.UTF8.GetBytes(payload)
        };

        client.Session.EnqueueApplicationMessageAsync(message);
    }

    
}
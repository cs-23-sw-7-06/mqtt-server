using MQTTnet;
using MQTTnet.Server;


public class Program {

    public static void Main(String[] args){
        // defining options:
        var options = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(4000)
            .Build();

        var server = new MqttFactory().CreateMqttServer(options);
        server.ClientConnectedAsync += OnNewConnection;

        server.StartAsync().GetAwaiter().GetResult();

        Console.ReadLine();
    }

    public static Task OnNewConnection(ClientConnectedEventArgs args){
        Console.WriteLine("Hello world from ESP over MQTT!");

        return Task.CompletedTask;
    }

    public static void OnNewMessage(){
        Console.WriteLine("New Message!");

    }
}
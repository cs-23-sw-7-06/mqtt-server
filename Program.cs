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

        server.StartAsync().GetAwaiter().GetResult();

        Console.Write("end? > ");
        Console.ReadLine();
    }

    public static void OnNewConnection(){
        
    }

    public static void OnNewMessage(){

    }
}
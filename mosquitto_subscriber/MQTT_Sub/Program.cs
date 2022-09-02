using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_Sub
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var mqttFactory = new MqttFactory().CreateMqttClient();

            var _mqttClient = mqttFactory;

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost", 1883)
                .WithCleanSession().Build();

            _mqttClient.UseConnectedHandler(e =>
            {
                var topic = new TopicFilterBuilder()
                    .WithTopic("123")
                    .Build();

                _mqttClient.SubscribeAsync(topic);


            });



            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;

                    if (string.IsNullOrWhiteSpace(topic) == false)
                    {
                        string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"Topic: {topic}. Message Received: {payload}");

                        var httpClient = new HttpClient();

                        var pay = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");


                        var response = await httpClient.PostAsync(
                            "http://localhost:5000/api/Mosquitto", 
                            pay);
                        
                        Console.WriteLine($"Response status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                }
            });

            await _mqttClient.ConnectAsync(options);

            Console.ReadLine();
        }
    }
}

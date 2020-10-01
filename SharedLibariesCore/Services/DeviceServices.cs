using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibariesCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MAD = Microsoft.Azure.Devices;

namespace SharedLibariesCore.Services
{
    public class DeviceServices
    {
        private static readonly Random rnd = new Random();
        public static async Task<string> SendMessageAsync(DeviceClient deviceClient)
        {

            var data = new TempratureModel
            {
                Temperature = rnd.Next(20, 30).ToString(),
                Humidity = rnd.Next(40, 50).ToString()
            };

            var json = JsonConvert.SerializeObject(data);

            var payload = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(json));


            await deviceClient.SendEventAsync(payload);

            Console.WriteLine($"Message sent: {json}");

            return json;

        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                Console.WriteLine($"Message Received {Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }
        }

        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}

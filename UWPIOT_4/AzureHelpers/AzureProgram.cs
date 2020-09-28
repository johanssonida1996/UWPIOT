using Microsoft.Azure.Devices.Client;
using SharedLibaries.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPIOT_4.AzureHelpers
{
    public class AzureProgram
    {
        private static readonly string _conn = "HostName=ec-win20-iothubida.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=ejgLOFauDEcazZlOsZ4DJQ/QJsSIZ0O1j6EmKwcpKdI=";

        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);

        public static void SendReceiveMessageAsync()
        {

            DeviceServices.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceServices.ReceiveMessageAsync(deviceClient).GetAwaiter();

        }
    }
}

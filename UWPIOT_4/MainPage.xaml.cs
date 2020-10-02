using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Azure.Devices.Client;
using SharedLibaries.Models;
using Newtonsoft.Json.Linq;
using SharedLibaries.Services;





// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPIOT_4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly string _conn = "HostName=ec-win20-iothubida.azure-devices.net;DeviceId=UWPApp;SharedAccessKey=HW14jmei3IwjlNvaQuwGL4Zw2mtumX6E2z8xYrXgFg4=";

        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
        public MainPage()
        {
            this.InitializeComponent();
        }
        public WeatherList weatherlist = new WeatherList();
        public BodyMessageModel humtemp = new BodyMessageModel();
        

        private async void btnSendMessageAsync_Click(object sender, RoutedEventArgs e)
        {
            var result = await DeviceServices.SendMessageAsync(deviceClient);

            dynamic data = JObject.Parse(result);

            var temp = Convert.ToString(data.Temperature);
            var hum = Convert.ToString(data.Humidity);

            DeviceServices.ReceiveMessageAsync(deviceClient).GetAwaiter();

            try
            {
                weatherlist.Add(new TempratureModel($"Temeprature: {temp}", $"Humidity: {hum}"));
            }
            catch
            {

            }

        }
    }
}

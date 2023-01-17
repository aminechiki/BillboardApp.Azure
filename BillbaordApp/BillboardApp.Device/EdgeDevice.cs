using BillboardApp.Domain.Entities;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Text;
using System.Text.Json;

namespace BillboardApp.Device
{
    public class EdgeDevice : BackgroundService
    {
        private readonly ILogger<EdgeDevice> _logger;
        private readonly IConfiguration _configuration;
        public DeviceClient deviceClient { get; set; }
        public string csDevice { get; set; }

        public EdgeDevice(ILogger<EdgeDevice> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
            this.csDevice = this._configuration.GetConnectionString("Device");
            //create device client
            this.deviceClient = DeviceClient.CreateFromConnectionString(this.csDevice, TransportType.Mqtt);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)          //cancellationToken si blocca ed esce quando arraesti l'app
        {
            while (true)
            {
                //recived messagge to cloud
                Message receivedMessage = await deviceClient.ReceiveAsync();

                if (receivedMessage == null) continue;
                string billboardSerialize = Encoding.ASCII.GetString(receivedMessage.GetBytes());

                //Deserialize object messaggeBillboard
                Billboard billboardDeserialize = JsonSerializer.Deserialize<Billboard>(billboardSerialize);
                TwinCollection reportedProperties = new TwinCollection();

                //Set color for billboard
                await SetColor(billboardDeserialize, reportedProperties);

                //Print Billboard
                PrintBillboard(billboardDeserialize, reportedProperties);

                //Delete message from the device queue
                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
        public async Task SetColor(Billboard billboard, TwinCollection reportedProperties)
        {
            //Get device twin of Device
            var twin = await deviceClient.GetTwinAsync();

            //Take color to desired from devce twin
            string color = twin.Properties.Desired["color"];

            //set color to Reported from Device Twin
            reportedProperties["color"] = color;
            await this.deviceClient.UpdateReportedPropertiesAsync(reportedProperties);

            //Set color for billboard
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
        }
        public async Task PrintBillboard(Billboard billboard, TwinCollection reportedProperties)
        {
            //Get device twin of Device
            var twin = await deviceClient.GetTwinAsync();

            //Take the rows and columns to desired from devce twin
            int rows = twin.Properties.Desired["rows"];
            int columns = twin.Properties.Desired["columns"];

            //set rows and columns to Reported from Device Twin
            reportedProperties["rows"] = rows;
            reportedProperties["columns"] = columns;

            //print billboard : print rows and columns with between confront rows and collumns in Device twin
            int countRows = 0;
            int countCharacters = 0;
            foreach (string billboardRows in billboard.rows)
            {
                countRows++;
                int CharactersBillboard = billboardRows.Length;
                foreach (char Characters in billboardRows)
                {
                    countCharacters++;
                    Console.Write(Characters);
                    if (countCharacters >= columns)
                    {
                        countCharacters = 0;
                        break;
                    }
                }
                Console.WriteLine();
                if (countRows >= rows) break;
            }
        }
    }
}
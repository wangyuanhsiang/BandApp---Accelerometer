using Microsoft.Band;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Band.Sensors;

using System.Text;
using Windows.Storage.Pickers;
using Windows.Storage;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BandApp___Accelerometer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {



        IBandInfo[] pairedBands;
        IBandClient bandClient;
        string baroData;
        string gyroData;
        string accelData;
        bool accelDataSaved = false;
        bool gyroDataSaved = false;
        bool baroDataSaved = false;
       
        
        public MainPage()
        {
            this.InitializeComponent();
            OnLoaded();
            
        }

        private async void OnLoaded()
        {
            pairedBands = await BandClientManager.Instance.GetBandsAsync();
            bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);

            bandClient.SensorManager.Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            await bandClient.SensorManager.Accelerometer.StartReadingsAsync();

            bandClient.SensorManager.Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            await bandClient.SensorManager.Gyroscope.StartReadingsAsync();

            bandClient.SensorManager.Barometer.ReadingChanged += Barometer_ReadingChanged;
            await bandClient.SensorManager.Barometer.StartReadingsAsync();
        }

        private async void Barometer_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandBarometerReading> e)
        {
            IBandBarometerReading baro = e.SensorReading;
            baroData = string.Format("Temperature = {0:G4}\nAirPressure = {1:G4}", baro.Temperature, baro.AirPressure);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,  () =>
          {
              this.tbBarometer.Text = baroData;
              //lvBarometer.Items.Add(baroData);
              tbWriteBaro.Text += baroData;

              //byte b = Convert.ToByte(baroData);
              //var data = new List<byte>();
              //data.Add(b);

              if (baroDataSaved == true)
              {
                  bandClient.SensorManager.Barometer.StopReadingsAsync();
                  Recorder RE = new Recorder();
                  RE.baroRecorder(tbWriteBaro.Text);
              }

          }).AsTask();
        }

        private async void Gyroscope_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            IBandGyroscopeReading gyro = e.SensorReading;
            gyroData = string.Format("X = {0:G4}\nY = {1:G4}\nZ = {2:G4}", gyro.AngularVelocityX, gyro.AngularVelocityY, gyro.AngularVelocityZ);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,  () =>
           {
               this.tbGyroscope.Text = gyroData;
                //lvGyroDataShow.Items.Add(gyroData);
                tbWriteGyro.Text += gyroData;

               if (gyroDataSaved == true)
               {
                   bandClient.SensorManager.Gyroscope.StopReadingsAsync();
                   Recorder RE = new Recorder();
                   RE.gyroRecorder(tbWriteGyro.Text);
               }

           }).AsTask();
        }

        private async void Accelerometer_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> e)
        {
            IBandAccelerometerReading accel = e.SensorReading;
            accelData = string.Format("X = {0:G4}\nY = {1:G4}\nZ = {2:G4}", accel.AccelerationX, accel.AccelerationY, accel.AccelerationZ);
            //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { this.accelerometerTextBlock.Text = text; }).AsTask();
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
           {
               this.tbAccelerometer.Text = accelData;
                //lvdatashow.Items.Add(accelData);

                tbWriteAccel.Text += accelData;
               if (accelDataSaved == true)
               {
                   bandClient.SensorManager.Accelerometer.StopReadingsAsync();
                   Recorder RE = new Recorder();
                   RE.accelRecorder(tbAccelerometer.Text);
               }


                //var data = new List<string>();
                //data.Add(text);

            }).AsTask();

            //var data = new List<AccData>();
            //data.Add(new AccData { textdata = text});
            //DataManager DM = new DataManager();
            //DM.GetData(text);

           

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(text);


            


            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(text);
            //if(sb.Length > 5)
            //{
            //    await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
            //    Recorder RE = new Recorder();
            //    RE.RecordHelper(sb);
            //}
        }

        private  void button_Click(object sender, RoutedEventArgs e)
        {
             bandClient.SensorManager.Accelerometer.StopReadingsAsync();
             bandClient.SensorManager.Gyroscope.StopReadingsAsync();
             bandClient.SensorManager.Barometer.StopReadingsAsync();
        }

        private  void bSaveBaroData_Click(object sender, RoutedEventArgs e)
        {
            baroDataSaved = true;
            bandClient.SensorManager.Barometer.StartReadingsAsync();
        }

        private  void bSaveGyroscope_Click(object sender, RoutedEventArgs e)
        {
            gyroDataSaved = true;
            bandClient.SensorManager.Gyroscope.StartReadingsAsync();
        }

        private  void bSaveAccel_Click(object sender, RoutedEventArgs e)
        {
            accelDataSaved = true;
            bandClient.SensorManager.Accelerometer.StartReadingsAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BandApp___Accelerometer
{
    public class Recorder
    {
        public async void baroRecorder(string e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("file style", new string[] { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = "BarometerData.txt";
            StorageFile file = await picker.PickSaveFileAsync();
            //await FileIO.WriteBytesAsync(file, e);

            await FileIO.WriteTextAsync(file, e);
        }


        public async void gyroRecorder(string e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("file style", new string[] {".txt" });
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = "gyroscopeData.txt";
            StorageFile file = await picker.PickSaveFileAsync();
            await FileIO.WriteTextAsync(file, e);
        }

        public async void accelRecorder(string e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("file style", new string[] { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = "accelemeterData.txt";
         
                StorageFile file = await picker.PickSaveFileAsync();
                if (file != null)
                {
                    await FileIO.WriteTextAsync(file, e);
                }
        }
    }
}

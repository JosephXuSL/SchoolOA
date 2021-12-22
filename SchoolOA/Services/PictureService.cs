using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOA.Services
{
    public class PictureService
    {        
        public const string imageFolderPath= @"C:\SchoolOAPictures";
        public string GetPhoto(string path)
        {            
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            var dataString=JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(byData));
            return dataString;
        }

        public string SavePhoto(string dataString, string name, string IDNumber)
        {
            var streamByte = JsonConvert.DeserializeObject<byte[]>(JsonConvert.SerializeObject(dataString));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);            
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            if (!Directory.Exists(imageFolderPath)) {
                Directory.CreateDirectory(imageFolderPath);            }
            var imagePath = imageFolderPath + @"\" + name + IDNumber + @".jpg";
            img.Save(imagePath);
            return imagePath;
        }

    }
}

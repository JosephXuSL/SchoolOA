using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Services
{
    public class PictureService
    {        
        public const string imagepath= @"C:\Pictures\image.jpg";
        public byte[] GetPictureData()
        {            
            FileStream fs = new FileStream(imagepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        public void SavePhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);            
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(@"C:\Pictures\"+"testc.jpg");
        }

    }
}

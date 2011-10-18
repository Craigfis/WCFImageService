using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace WcfImageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ImageService" in code, svc and config file together.
    public class ImageService : IImageService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string GetString()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache"); 
            return "Playing with WCF";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public CompositeType GetAnImage()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            var result = new CompositeType();
            result.StringValue = "Here's a pic for ya";
            var f = File.OpenRead(@"C:\Users\Consultant4\Pictures\2011-09-18 AnnetteLake\AnnetteLake 006.jpg");
            byte[] buffer = new byte[f.Length];
            f.Read(buffer, 0, (int)f.Length);
            result.Image = buffer;

            return result;
        }

        public System.ServiceModel.Channels.Message GetImage()
        {
            var files = Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["ImagePath"], "*.jpg", SearchOption.AllDirectories);
            var rnd = new Random(DateTime.Now.Millisecond);
            var fnum = rnd.Next(files.Length);
            var filename = files[fnum];
            var f = File.OpenRead(filename);
            byte[] buffer = new byte[f.Length];
            f.Read(buffer, 0, (int)f.Length);
            var ms = new MemoryStream(buffer);
            return WebOperationContext.Current.CreateStreamResponse(ms, "image/jpeg");
        }
    }
}

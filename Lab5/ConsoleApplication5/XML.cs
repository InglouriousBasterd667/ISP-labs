using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
namespace WpfApplication1
{
    class Program
    {


        static void Main(string[] args)
        {

            Matrix mat = new Matrix(10, 10);
            mat.Initialize(20);
            int a = 33;
            Serializer x = new Serializer();
            x.WriteObject(mat,@"C:\qwer.xml");
            
        }
    }
}

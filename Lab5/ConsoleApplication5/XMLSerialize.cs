using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;



class Foo
{
    public Bar bar;
}

class Bar
{
    public Foo foo;
}



class Serializer
{



    protected int spaces = 0;
    protected int isRootTag = 0;
    protected bool isValueWrite = false;
    protected bool isEventWrite = false;

    public Serializer()
    {

      
    }





    //private string TransformToXmlString(string line)
    //{
    //    line = line.Replace("&", "&amp;");
    //    line = line.Replace("<", "&lt;");
    //    line = line.Replace(">", "&gt;");
    //    line = line.Replace("'", "&apos;");
    //    line = line.Replace("\"", "&quot;");
    //    line = line.Replace(";", "&q");
    //    return line;
    //}

    public void WriteObject(object @object, string filename)
    {
        List<object> list = new List<object>();
        using (XmlWriter writer = XmlWriter.Create(filename))
        {
            writer.WriteStartDocument();
            WriteObject(@object, list,writer);
            writer.WriteEndDocument();
        }
    }
    //public object ReadObject(string filename)
    //{
    //    List<object> list = new List<object>();
    //    using (XmlReader reader = XmlReader.Create(filename))
    //    {
    //    }

    //}
    protected void WriteObject(object @object, List<object> list,XmlWriter writer)
    {
        int refindex;

        if (@object == null)
        {
            writer.WriteStartElement(XmlConvert.EncodeName(typeof(object).Name));
        }
        else if ((refindex = list.IndexOf(@object)) >= 0)
        {
            Type objtype = @object.GetType();

            writer.WriteStartElement(XmlConvert.EncodeName(objtype.Name));
            spaces++;
            writer.WriteString("reference = " + refindex.ToString());
            spaces--;
        }
        else
        {
            list.Add(@object);

            Type objtype = @object.GetType();

            writer.WriteStartElement(XmlConvert.EncodeName(objtype.Name));
            spaces++;

            if (objtype.IsPrimitive || objtype.Equals(typeof(string)))
            {
               writer.WriteString(@object.ToString());
            }
            else if (objtype.IsArray)
            {
                


                foreach (object item in ((System.Array)@object))
                {
                    WriteObject(item, list,writer);
                }

                
            }
            else
            {


                foreach (FieldInfo finfo in objtype.GetFields(BindingFlags.GetField | BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!finfo.IsLiteral && !finfo.IsInitOnly && !finfo.IsNotSerialized && finfo.FieldType.BaseType != null && !finfo.FieldType.BaseType.Equals(typeof(System.MulticastDelegate)))
                    {
                        writer.WriteStartElement(XmlConvert.EncodeName(finfo.Name));
                        spaces++;

                        WriteObject(finfo.GetValue(@object), list,writer);

                        spaces--;
                        writer.WriteEndElement();
                    }
                }
            }

            spaces--;

        }
        writer.WriteEndElement();
    }

}



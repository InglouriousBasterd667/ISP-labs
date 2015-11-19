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

class Serializer
{
    protected int spaces = 0;
    protected int isRootTag = 0;
    protected bool isValueWrite = false;
    protected bool isEventWrite = false;

    public Serializer()
    {

      
    }

    public void WriteObject(object @object, string filename)
    {
        List<object> list = new List<object>();
        //XmlWriterSettings settings = new XmlWriterSettings();
        //settings.Indent = true;
        using (XmlWriter writer = XmlWriter.Create(filename))
        {
            writer.WriteStartDocument();
            WriteObject(@object, list,writer);
            writer.WriteEndDocument();
        }
    }

    public object ReadObject(string filename)
    {
        List<object> list = new List<object>();
        object obj;
        using (XmlReader reader = XmlReader.Create(filename))
        {

            obj = ReadObject(null, list, reader);

        }
        return obj;

    }

    private object ReadObject(string classname, List<object> list, XmlReader reader)
    {
        object res = null;
        int count = 0;
        string attribute = null;
        int fieldcount;

            if (classname == null)
            {
                reader.Read();
                if (reader.IsStartElement())
                    classname = XmlConvert.DecodeName(reader.Name);
                count = Convert.ToInt32(reader["count"]);
            }

            if (classname == null)
                return null;
        fieldcount = Convert.ToInt32(reader["fieldcount"]);
        attribute = reader["reference"];
        reader.Read();
        if (reader.IsStartElement() || reader.Value != null)
        {
            Type objtype = Type.GetType(classname);

            if (attribute != null)
            {
                //  string attribute = reader["reference"];
                if (attribute != null)
                {
                    res = list[Convert.ToInt32(attribute)];
                }
            }
            else if (objtype.IsPrimitive)
            {
                res = (object)objtype.InvokeMember("Parse",
                      BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
                      null, null, new object[] { XmlConvert.DecodeName(reader.Value.Trim()) });
                list.Add(res);
            }
            else if (objtype.Equals(typeof(string)))
            {
                res = XmlConvert.DecodeName(reader.Value.Trim());
            }
            else
            {
                if (objtype.IsArray)
                {
                    //count = Convert.ToInt32(reader["count"]);
                    int index = 0;
                    res = Activator.CreateInstance(objtype, count);
                    list.Add(res);
                    while (index < count)
                    {
                        //reader.Read();
                        while(!reader.IsStartElement())
                            reader.Read();
                        string s = reader.Name;
                        ((System.Array)res).SetValue(ReadObject(s, list, reader), index++);
                    }
                }
                else
                {
                    res = Activator.CreateInstance(objtype);
                    list.Add(res);
                    int index = 0;
                    //count = Convert.ToInt32(reader["count"]);
                    while (index < fieldcount)
                    {
                        while (!reader.IsStartElement())
                            reader.Read();
                        index++;
                        string fieldname = reader.Name;
                        object fieldobject = ReadObject(null, list, reader);
                        FieldInfo fiendinf = objtype.GetField(fieldname, BindingFlags.GetField | BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        if (fiendinf != null)
                            fiendinf.SetValue(res, fieldobject);
                    }

                }
            }
        } 
     return res;
    }



    protected void WriteObject(object @object, List<object> list,XmlWriter writer)
    {
        int refindex;

        if (@object == null)
        {
            writer.WriteStartElement(XmlConvert.EncodeName(typeof(object).FullName));
        }
        else if ((refindex = list.IndexOf(@object)) >= 0)
        {
            Type objtype = @object.GetType();

            writer.WriteStartElement(XmlConvert.EncodeName(objtype.FullName));
            spaces++;
            writer.WriteAttributeString("reference", refindex.ToString());
            //writer.WriteString("reference = " + refindex.ToString());
            spaces--;
        }
        else
        {
            list.Add(@object);

            Type objtype = @object.GetType();

            writer.WriteStartElement(XmlConvert.EncodeName(objtype.FullName));
            spaces++;

            if (objtype.IsPrimitive || objtype.Equals(typeof(string)))
            {
               writer.WriteString(@object.ToString());
            }
            else if (objtype.IsArray)
            {
                writer.WriteAttributeString("count", ((System.Array)@object).Length.ToString());
                foreach (object item in ((System.Array)@object))
                {
                    WriteObject(item, list,writer);
                }               
            }
            else
            {
                FieldInfo[] fi = objtype.GetFields();
                int pf = 0;
                if (fi.Count() != 0)
                    pf = fi.Count();
                PropertyInfo[] pi = null;
                if (fi.Count() == 0)
                {
                    pi = objtype.GetProperties();
                    pf += pi.Count();
                }
                writer.WriteAttributeString("fieldcount", pf.ToString());
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    internal struct ObjectInfo
    {
        // Members
        public string Name;
        public string Type;
        public string Assembly;
        public string Value;
        // public bool HasBinaryConstructor;
        public string ConstructorParamType;
        public string ConstructorParamAssembly;

        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string n = Name;
            if (String.IsNullOrEmpty(n))
                n = "<Name not set>";

            string t = Type;
            if (String.IsNullOrEmpty(t))
                t = "<Type not set>";

            string a = Type;
            if (String.IsNullOrEmpty(a))
                a = "<Assembly not set>";

            return n + "; " + t + "; " + a;
        }

        /// <summary>
        /// Determines whether the values are sufficient to create an instance.
        /// </summary>
        /// <returns></returns>
        public bool IsSufficient
        {
            get
            {
                // Type and Assembly should be enough
                if (String.IsNullOrEmpty(Type) || String.IsNullOrEmpty(Assembly))
                    return false;

                return true;
            }
        }
        
    }
    public class TypeInfo
    {
        #region Members & Properties

        private string typename = null;
        private string assemblyname = null;

        /// <summary>
        /// Gets or sets the Types name.
        /// </summary>
        public string TypeName
        {
            get { return typename; }
            set { typename = value; }
        }

        /// <summary>
        /// Gets or sets the Assemblys name.
        /// </summary>
        public string AssemblyName
        {
            get { return assemblyname; }
            set { assemblyname = value; }
        }

        #endregion Members & Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public TypeInfo()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="obj"></param>
        public TypeInfo(object obj)
        {
            if (obj == null)
                return;

            TypeName = obj.GetType().FullName;
            AssemblyName = obj.GetType().Assembly.FullName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        public TypeInfo(Type type)
        {
            if (type == null)
                return;

            TypeName = type.FullName;
            AssemblyName = type.Assembly.FullName;
        }

        #endregion Constructors

        #region static Helpers

       
        public static bool IsCollection(Type type)
        {
            if (typeof(ICollection<object>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }

       
       

       
        public static bool IsList(Type type)
        {
            if (typeof(IList<object>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }

      
        public static bool IsArray(String type)
        {
            // type.HasElementType

            // The simple way
            if (type != null && type.EndsWith("[]"))
                return true;

            return false;
        }

      

     
        

       

      

        #endregion static Helpers
    }


}

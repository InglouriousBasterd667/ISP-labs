﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class XmlSerializationTag
    {
        private string OBJECT = "object";
        private string NAME = "name";
        private string TYPE = "type";
        private string ASSEMBLY = "assembly";
        private string PROPERTIES = "properties";
        private string PROPERTY = "property";
        private string ITEMS = "items";
        private string ITEM = "item";
        private string INDEX = "index";
        private string NAME_ATT_KEY = "Key";
        private string NAME_ATT_VALUE = "Value";
        private string TYPE_DICTIONARY = "typedictionary";
        private string GENERIC_TYPE_ARGUMENTS = "generictypearguments";
        private string CONSTRUCTOR = "constructor";
        private string BINARY_DATA = "binarydata";

        public virtual string GENERIC_TYPE_ARGUMENTS_TAG
        {
            get { return GENERIC_TYPE_ARGUMENTS; }
        }

        public virtual string OBJECT_TAG
        {
            get { return OBJECT; }
        }

        public virtual string NAME_TAG
        {
            get { return NAME; }
        }

        public virtual string TYPE_TAG
        {
            get { return TYPE; }
        }

        public virtual string ASSEMBLY_TAG
        {
            get { return ASSEMBLY; }
        }

        public virtual string PROPERTIES_TAG
        {
            get { return PROPERTIES; }
        }

        public virtual string PROPERTY_TAG
        {
            get { return PROPERTY; }
        }

        public virtual string ITEMS_TAG
        {
            get { return ITEMS; }
        }

        public virtual string ITEM_TAG
        {
            get { return ITEM; }
        }

        public virtual string INDEX_TAG
        {
            get { return INDEX; }
        }

        public virtual string NAME_ATT_KEY_TAG
        {
            get { return NAME_ATT_KEY; }
        }

        public virtual string NAME_ATT_VALUE_TAG
        {
            get { return NAME_ATT_VALUE; }
        }

        public virtual string TYPE_DICTIONARY_TAG
        {
            get { return TYPE_DICTIONARY; }
        }

        public string CONSTRUCTOR_TAG
        {
            get { return CONSTRUCTOR; }
        }

        public string BINARY_DATA_TAG
        {
            get { return BINARY_DATA; }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.POCO
{    
    public class TB_CADASTRO : Cemapa.Models.TB_CADASTRO, IEditableObject, INotifyPropertyChanged
    {
        Hashtable props = null;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void BeginEdit()
        {
            //exit if in Edit mode
            //uncomment if  CancelEdit discards changes since the 
            //LAST BeginEdit call is desired action
            //otherwise CancelEdit discards changes since the 
            //FIRST BeginEdit call is desired action
            if (null != props) return;

            //enumerate properties
            PropertyInfo[] properties = (this.GetType()).GetProperties
                        (BindingFlags.Public | BindingFlags.Instance);

            props = new Hashtable(properties.Length - 1);

            for (int i = 0; i < properties.Length; i++)
            {
                //check if there is set accessor
                if (null != properties[i].GetSetMethod())
                {
                    object value = properties[i].GetValue(this, null);
                    props.Add(properties[i].Name, value);
                }
            }
        }

        public void CancelEdit()
        {
            //check for inappropriate call sequence
            if (null == props) return;

            //restore old values
            PropertyInfo[] properties = (this.GetType()).GetProperties
                (BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                //check if there is set accessor
                if (null != properties[i].GetSetMethod())
                {
                    object value = props[properties[i].Name];
                    properties[i].SetValue(this, value, null);
                }
            }

            //delete current values
            props = null;
        }

        public void EndEdit()
        {
            //delete current values
            props = null;
        }
    }
}

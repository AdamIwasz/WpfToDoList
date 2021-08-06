using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfToDoList.Models
{
    public class MainModel
    {
        public string Task {
           get; set; 
        }

        public string Description { get; set; }

        public override string ToString()
        {
            return Task.ToString();
        }
    }
}

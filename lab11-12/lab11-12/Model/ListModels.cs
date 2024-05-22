using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_12.Model
{
    internal class ListModels
    {
        public List<string> Models { get; set; }
        public ListModels(params string[] models)
        { 
            Models = new List<string>();

            foreach (string model in models)
            {
                Models.Add(model);
            }
        }

        public override string ToString()
        {
            string result = "";

            foreach (string model in Models)
            {
                result += model;
                result += ", ";
            }
            return result;
        }

    }
}

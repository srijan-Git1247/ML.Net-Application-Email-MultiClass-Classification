using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Email_MultiClass_Classification.ML.Objects
{
    public class Email//Container class tht contains the data to predict and train our model
    {
        #pragma warning disable 8618
        [LoadColumn(0)]
        public string Subject
        {
            get;set;
        }
        [LoadColumn(1)]
        public string Body
        { get;set; }
        [LoadColumn(2)]
        public string Sender
        { get;set; }
        [LoadColumn(3)]
        public string Category
        { get;set; }

    }
}

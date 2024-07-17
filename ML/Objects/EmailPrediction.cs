using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Email_MultiClass_Classification.ML.Objects
{
    //The EmailPrediction class contains the propery mapped to our prediction output used for model evaluation
    public class EmailPrediction
    {
        #pragma warning disable 8618
        [ColumnName("PredictedLabel")]
        public string Category;
    }
}

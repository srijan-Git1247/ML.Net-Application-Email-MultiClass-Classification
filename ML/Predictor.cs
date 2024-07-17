using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Email_MultiClass_Classification.ML.Base;
using Email_MultiClass_Classification.ML.Objects;
using Email_MultiClass_Classification.Common;
using Microsoft.ML;
using Newtonsoft.Json;

namespace Email_MultiClass_Classification.ML
{
    public class Predictor: BaseML//This provide prediction support in our project
    {
        public void Predict(string inputDataFile)
        {
            if (!File.Exists(ModelPath))
            {
                ////Verifying if the model exists prior to reading it
                Console.WriteLine($"Failed to find model at {ModelPath}");
                return;

            }
            if (!File.Exists(inputDataFile))
            {
                //Verifying if the input file exists before making predictions on it 
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }
            

            /*Loading the model  */
            //Then we define the ITransformer Object
            ITransformer mlModel;

            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                mlModel = MlContext.Model.Load(stream, out _);
                //Stream is disposed as a result of Using statement
            }
            if (mlModel == null)
            {
                Console.WriteLine("Failed to load the model");
                return;
            }

            // Create a prediction engine\
            var predictionEngine = MlContext.Model.CreatePredictionEngine<Email, EmailPrediction>(mlModel);

            //Read in the file as Text and deserialize the JSON into our Email Class Object
            var json = File.ReadAllText(inputDataFile);
            //Call predict model on prediction engine class

            #pragma warning disable 8604
            var prediction=predictionEngine.Predict(JsonConvert.DeserializeObject<Email>(json));

            //Adjust the output of our prediction to match our new EmailPrediction Properties

            Console.WriteLine(
                               $"Based on input json:{System.Environment.NewLine}" +
                               $"{json}{System.Environment.NewLine}" +
                               $"The email is predicted to be a {prediction.Category}");










        }
    }
}

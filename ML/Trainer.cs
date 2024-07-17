using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Email_MultiClass_Classification.ML.Base;
using Email_MultiClass_Classification.ML.Objects;
using Microsoft.ML;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Email_MultiClass_Classification.Common;

namespace Email_MultiClass_Classification.ML
{
    public class Trainer:BaseML
    {
        public void Train(string trainingFileName, string testFileName)
        {
            // Check if training data exists
            if (!File.Exists(trainingFileName))
            {
                Console.WriteLine($"Failed to find the training data file {trainingFileName}");
                return;
            }

            //Ensure that the test fileName exists
            if (!File.Exists(testFileName))
            {
                Console.WriteLine($"Failed to find test data file ({testFileName}");
                return;
            }

            //We read the trainingFileName string and typecast it to an Email object like this
            //Loads Text file into an IDataViewObject
            var trainingDataView = MlContext.Data.LoadFromTextFile<Email>(trainingFileName, ',', hasHeader: false); // Use of comma to separate the data



            /*Creating pipeline*/
            //We will create our pipeline mapping our input properties to FeaturizeText transformations before appending our SDCA trainer
            var dataProcessPipeline = MlContext.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(Email.Category), outputColumnName: "Label")
                .Append(MlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Subject), outputColumnName: "SubjectFeaturized"))
                .Append(MlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Body), outputColumnName: "BodyFeaturized"))
                .Append(MlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(Email.Sender), outputColumnName: "SenderFeaturized"))
                .Append(MlContext.Transforms.Concatenate("Features", "SubjectFeaturized", "BodyFeaturized", "SenderFeaturized"))
                .AppendCacheCheckpoint(MlContext);

            //Complete our pipeline by appending the SDCA trainer
            var trainingPipeline = dataProcessPipeline
               .Append(MlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
               .Append(MlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


            //Train the model with the data set created Earlier
            ITransformer trainedModel = trainingPipeline.Fit(trainingDataView);


            //Save created model to the filename specified matching training set's schema
            MlContext.Model.Save(trainedModel, trainingDataView.Schema, ModelPath);



            //Lastly we load in our test data,
            //Run the multiclassClassification evalutation and then output the four model evalutation properties, like this:

            var testDataView = MlContext.Data.LoadFromTextFile<Email>(testFileName, ',', hasHeader: false);

            // Call the Evaluate method to provide specific metrics
            var modelMetrics = MlContext.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
            Console.WriteLine($"MicroAccuracy: {modelMetrics.MicroAccuracy:0.###}");
            Console.WriteLine($"MacroAccuracy: {modelMetrics.MacroAccuracy:0.###}");
            Console.WriteLine($"LogLoss: {modelMetrics.LogLoss:#.###}");
            Console.WriteLine($"LogLossReduction: {modelMetrics.LogLossReduction:#.###}");
            Console.WriteLine($"LogLossReduction: {modelMetrics.LogLossReduction:#.###}");




        }
    }
}

using Microsoft.ML;
using Microsoft.ML.Data;
using MLNetSampleFCCML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCApp.Helpers
{
    public class ModelOutputWithContribution : ModelOutput
    {
        public float[] FeatureContributions { get; set; }

        public List<FeatureContribution> GetFeatureContributions(DataViewSchema dataview)
        {
            //base.PrintToConsole();
            VBuffer<ReadOnlyMemory<char>> slots = default;
            dataview.GetColumnOrNull("Features").Value.GetSlotNames(ref slots);
            var featureNames = slots.DenseValues().ToArray();
            List<FeatureContribution> featureList = new List<FeatureContribution>();
            for (int i = 0; i < featureNames.Count(); i++)
            {
                string featureName = featureNames[i].ToString();
                //if (featureName == "PassengerCount" || featureName == "TripTime" || featureName == "TripDistance")
                    featureList.Add(new FeatureContribution(featureName, FeatureContributions[i]));
            }

            return featureList;

        }
    }

    public class ScorePrediction : ModelOutputWithContribution
    {
        public List<FeatureContribution> Features { get; set; }

        public ScorePrediction(float PredictedScore, List<FeatureContribution> Features)
        {
            this.Score = PredictedScore;
            this.Features = Features;
        }
    }

    public class FeatureContribution
    {
        public string Name;
        public float Value;

        public FeatureContribution(string Name, float Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UtahPlanners.Domain.Entity
{
    public partial class Property
    {
        public Weight Weights { get; set; }

        [DataMember]
        public List<int> PictureIds { get; set; }

        [DataMember]
        public int SecondaryPictureId { get; set; }

        [DataMember]
        public int Score { get; set; }

        public void CalculateScore()
        {
            var score = 0;
            if (Weights != null)
            {
                var neighScore = ((int)NeighborhoodCode.weight / 6.0) * (int)Weights.neighCondition;
                var streetWalkScore = ((int)StreetwalkCode.weight / 20.0) * (int)Weights.streetWalk;
                var commonCodeScore = ((int)CommonCode.weight / 15.0) * (int)Weights.commonAreas;
                var screetConnScore = ((int)StreetconnCode.weight / 6.0) * (int)Weights.streetConn;
                var buildingScore = ((int)EnclosureCode.weight / 4.0) * (int)Weights.buildingEnclosure;
                var streetSafetyScore = ((int)StreetSafteyCode.weight / 10.0) * (int)Weights.streetSaftey;
                var walkScore = ((int)walkscore / 100.0) * (int)Weights.walkscore;
                var twoFiftySFScore = (this.GetTwoFiftySFScore() / 15.0) * (int)Weights.twoFiftySingleFam;
                var twoFiftyAptsScore = (this.GetTwoFiftyAptsScore() / 5.0) * (int)Weights.twoFiftyApts;

                var overallScore = neighScore + streetWalkScore + commonCodeScore +
                    screetConnScore + buildingScore + streetSafetyScore + walkScore +
                    twoFiftySFScore + twoFiftyAptsScore;
                score = (int)overallScore;
            }
            this.Score = score;
        }

        private int GetTwoFiftySFScore()
        {
            int twoFiftySFScore;
            if (twoFiftySingleFam > 35)
                twoFiftySFScore = 15;
            else if (twoFiftySingleFam > 25)
                twoFiftySFScore = 13;
            else if (twoFiftySingleFam > 15)
                twoFiftySFScore = 11;
            else if (twoFiftySingleFam > 10)
                twoFiftySFScore = 9;
            else if (twoFiftySingleFam > 6)
                twoFiftySFScore = 7;
            else if (twoFiftySingleFam > 3)
                twoFiftySFScore = 4;
            else if (twoFiftySingleFam > 0)
                twoFiftySFScore = 2;
            else
                twoFiftySFScore = 0;
            
            return twoFiftySFScore;
        }

        private int GetTwoFiftyAptsScore()
        {
            int twoFiftyAptsScore;
            if (twoFiftyApts > 30)
                twoFiftyAptsScore = 5;
            else if (twoFiftyApts > 10)
                twoFiftyAptsScore = 4;
            else if (twoFiftyApts > 0)
                twoFiftyAptsScore = 2;
            else
                twoFiftyAptsScore = 0;

            return twoFiftyAptsScore;
        }
    }
    
    /*
    [DataContract(IsReference = true)]
    public partial class Address { }
    
    [DataContract(IsReference = true)]
    public partial class CommonCode { }

    [DataContract(IsReference = true)]
    public partial class EnclosureCode { }

    [DataContract(IsReference = true)]
    public partial class NeighborhoodCode { }
    
    [DataContract(IsReference = true)]
    public partial class PropertyType { }

    [DataContract(IsReference = true)]
    public partial class SocioEconCode { }

    [DataContract(IsReference = true)]
    public partial class StreetconnCode { }

    [DataContract(IsReference = true)]
    public partial class StreetSafteyCode { }

    [DataContract(IsReference = true)]
    public partial class StreetType { }

    [DataContract(IsReference = true)]
    public partial class StreetwalkCode { }
    */
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Infrastructure
{
    public partial class Property
    {
        public int Score
        {
            get
            {
                Weight weights = new PropertyRepository().GetWeights();
                var neighScore = ((int)NeighborhoodCode.weight / 6.0) * (int)weights.neighCondition;
                var streetWalkScore = ((int)StreetwalkCode.weight / 20.0) * (int)weights.streetWalk;
                var commonCodeScore = ((int)CommonCode.weight / 15.0) * (int)weights.commonAreas;
                var screetConnScore = ((int)StreetconnCode.weight / 6.0) * (int)weights.streetConn;
                var buildingScore = ((int)EnclosureCode.weight / 4.0) * (int)weights.buildingEnclosure;
                var streetSafetyScore = ((int)StreetSafteyCode.weight / 10.0) * (int)weights.streetSaftey;
                var walkScore = ((int)walkscore / 100.0) * (int)weights.walkscore;
                var twoFiftySFScore = (this.GetTwoFiftySFScore() / 15.0) * (int)weights.twoFiftySingleFam;
                var twoFiftyAptsScore = (this.GetTwoFiftyAptsScore() / 5.0) * (int)weights.twoFiftyApts;

                var overallScore = neighScore + streetWalkScore + commonCodeScore +
                    screetConnScore + buildingScore + streetSafetyScore + walkScore +
                    twoFiftySFScore + twoFiftyAptsScore;
                return (int)overallScore;
            }
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
}

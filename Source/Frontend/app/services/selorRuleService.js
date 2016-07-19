(function() {

    var selorRuleService = function($http, $log) {

        var getCompetenceScoreSuggestion = function(dimensions) {

            return 1;

        };


        var getFormScoreSuggestion = function(competences) {

            return 1;
        }

        var getScoreSuggestion = function(scores) {
                 $log.info("========================");
            $log.info("scores:");

            $log.info(scores);

            var scoreNotRounded = calculateAverage([calculateMinMax(scores).min,calculateMinMax(scores).max]);
             $log.info("scoreNotRounded: "+ scoreNotRounded);

             var rankMin=  getScoreMinSuggestion(scores);
             var rankMax= getScoreMaxSuggestion(scores);
             $log.info("========================");

            return {
                score: Math.floor((rankMax+rankMin)/2),
                minScore:rankMin,
                maxScore: rankMax
            };

        };

       //Get score min suggestion
        var getScoreMinSuggestion = function(scores) {

                   $log.info("------------------");

            $log.info("ScoreMin Processing");

            var scoresNA1 = scores.slice(0);

            var NbComp = scores.length;

            var NbNA = 0;

            //Replace NAs with '1'
            for (var i = 0; i < scores.length; i++) {

                if (scores[i] == null) {

                    scoresNA1[i] = 1;
                    NbNA++;
                } else {

                    scoresNA1[i] = scores[i];
                }

            }

            var etendue = calculateEtendue(scoresNA1);

            var meanNa = calculateAverage(scoresNA1);

            var NbDiff = scoresNA1.filter(function(v, i) {
                return i == scoresNA1.lastIndexOf(v);
            }).length;


            var addOneIfNA = 0;

            if (NbNA > 0) {
                addOneIfNA = 1;
            }


            var minNotRounded = meanNa - Math.sqrt((NbDiff / NbComp * (etendue + addOneIfNA) / NbComp));

            var minRounded = Math.round(minNotRounded);

            $log.info("Etendue: " + etendue);
            $log.info("MeanNa: " + meanNa);
            $log.info("NbComp: " + NbComp);
            $log.info("NbNA: " + NbNA);
            $log.info("NbDiff: " + NbDiff);
            $log.info("minNotRounded" + minNotRounded);
            $log.info("minRounded" + minRounded);

            $log.info("ScoreMin Done");

                   $log.info("------------------");

            return minRounded;

        };

        //Get score max suggestion
        var getScoreMaxSuggestion = function(scores) {

             $log.info("------------------");

            $log.info("ScoreMax Processing");

            var scoresWithoutNA = [];

            //Number of not evaluated
            var NbNA = 0;

            var NbComp = scores.length;

            var addOneIfNA = 0;


            //Fill scores w/o NA
            for (var i = 0; i < scores.length; i++) {

                if (scores[i] == null) {
                    NbNA++;
                } else {
                    scoresWithoutNA.push(scores[i]);
                }

            }

            $log.info(scoresWithoutNA);

            var etendue = calculateEtendue(scoresWithoutNA);

            var meanNoNa = calculateAverage(scoresWithoutNA);

            var NbDiff = scoresWithoutNA.filter(function(v, i) {
                return i == scoresWithoutNA.lastIndexOf(v);
            }).length;

            var maxNotRounded = meanNoNa + Math.sqrt((NbDiff / NbComp * (etendue) / NbComp));

            var maxRounded = Math.round(maxNotRounded);

            $log.info("Etendue: " + etendue);
            $log.info("meanNoNa: " + meanNoNa);
            $log.info("NbComp: " + NbComp);
            $log.info("NbNA: " + NbNA);
            $log.info("NbDiff: " + NbDiff);
            $log.info("maxNotRounded" + maxNotRounded);
            $log.info("maxRounded" + maxRounded);

            $log.info("ScoreMax Done");
                   $log.info("------------------");

            return maxRounded;
        };


        /* HELPER FUNCTIONS */

        //Calculate average value in array
        var calculateAverage = function(array) {

            var total = 0;

            for (var i = 0; i < array.length; i++) {
                total += array[i];
            }
            avg = total / array.length;
            return avg;
        }

        //Calculate difference between min and max value in array
        var calculateEtendue = function(array) {

            var minmax=calculateMinMax(array);

            minVal=minmax.min;

            maxVal= minmax.max;

            return maxVal - minVal;
        }

        //Get min and max value from array
        var calculateMinMax=function(array){

            var minVal = array[0];
            var maxVal = array[0];

            for (var i = 0; i < array.length; i++) {
                if (array[i] > maxVal) {
                    maxVal = array[i];
                }

                if (array[i] < minVal) {
                    minVal = array[i];
                }

            }

            return {min:minVal,max:maxVal};
        }

        return {
            getCompetenceScoreSuggestion: getCompetenceScoreSuggestion,
            getFormScoreSuggestion: getFormScoreSuggestion,
            getScoreSuggestion: getScoreSuggestion
        };

    };


    var module = angular.module("RevitApp");
    module.factory("selorRuleService", selorRuleService);

}());
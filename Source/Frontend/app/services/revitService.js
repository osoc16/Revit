(function() {

    var revitService = function($http, $log) {

        /* Variables */
        var apiBaseUrl = "http://revitapiazure20160717113757.azurewebsites.net/api/";

        //API Functions
        //
        //
        //

        var getEvaluationForm = function(formId, juryId, candidateId) {

            $log.info("=========API CALL===========")
            var callUrl = apiBaseUrl + "evaluations/juries/" + juryId + "/forms/" + formId + "/candidates/" + candidateId;
            $log.info("Url: " + callUrl);

            return $http.get(callUrl)
                .then(function(response) {
                    return response.data;
                });

        }

        var saveEvaluationForm = function(formId, juryId, candidateId, data) {

            $log.info("=========API CALL===========")
            var callUrl = apiBaseUrl + "evaluations/juries/" + juryId + "/forms/" + formId + "/candidates/" + candidateId;
            $log.info("Url: " + callUrl);


            return $http.put(callUrl, data).then(function(response) {
                return response.data;
            });
        }


        var getGeneralForm = function(formId) {

            $log.info("=========API CALL===========")
            var callUrl = apiBaseUrl + "forms/" + formId;
            $log.info("Url: " + callUrl);

            return $http.get(callUrl)
                .then(function(response) {
                    return response.data;
                });
        }


        var saveGeneralForm = function(formId, data) {


            $log.info("=========API CALL===========")
            var callUrl = apiBaseUrl + "forms/" + formId;
            $log.info("Url: " + callUrl);

            return $http.put(callUrl, data)
                .then(function(response) {
                    return response.data;
                });
        }

        var getScreenings = function(searchTerm) {
            $log.info("=========API CALL===========")
            var callUrl = apiBaseUrl + "screenings/?search=" + searchTerm;
            $log.info("Url: " + callUrl);

            return $http.get(callUrl)
                .then(function(response) {
                    return response.data;
                });
        }

        //Mock API Functions
        //
        //
        //
        var getScreeningsMock = function(searchTerm) {
            return [{

                    screeningId: 1,
                    name: "Screening 1",
                }
            ];
        }
        var getGeneralFormMock = function(formId) {
            var mockObject = {
                name: "Test eval form",

                description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Commodi odio, amet doloremque animi id ex autem inventore delectus consectetur ipsam, asperiores fugiat nam magnam fugit. Commodi, pariatur odio voluptas eum.",
                scoreMax: 6,
                scoreMin: 0,

                total: 6,
                score: 0,

                finalScore: 50,
                finalScoreMax: 100,
                finalScoreMin: 0,

                competences: [{
                        name: "Competence 1",
                        competenceId: 1,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maxime, explicabo fuga quibusdam ex, blanditiis ea magni minima tempora at totam sit. Repellendus similique iusto cum officiis quibusdam facilis quaerat, enim?",
                        status: "success",
                        statusMessage: "Completed",
                        comment: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ex officiis a",
                        dimensions: [{
                            name: "Dimension 1.1 ",

                            dimensionId: 1,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",

                        }, {
                            name: "Dimension 1.2 ",
                            dimensionId: 2,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint."


                        }]

                    }, {
                        name: "Competence 2",
                        competenceId: 2,
                        score: 3,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a.",
                        status: "warning",
                        statusMessage: "No comment written",
                        dimensions: [{
                            name: "Dimension 1.1 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",


                        }, {
                            name: "Dimension 1.2 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",


                        }, {
                            name: "Dimension 1.3 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",



                        }, {
                            name: "Dimension 1.4 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",



                        }]

                    }

                    , {
                        name: "Competence 3",
                        competenceId: 3,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."

                    }, {
                        name: "Competence 4",
                        competenceId: 4,

                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }, {
                        name: "Competence 5",
                        competenceId: 5,

                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }, {
                        name: "Competence 6",
                        competenceId: 6,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }
                ],

                candidates: [

                    {
                        firstName: "Sarah",
                        lastName: "Van de Velde",
                        candidateId: 2,
                        juries: []
                    }, {
                        firstName: "Peter Janssens",
                        candidateId: 3,
                        juries: []
                    },

                    {
                        firstName: "Jan",
                        lastName: "Van de Velde",
                        candidateId: 4,
                        juries: []
                    }

                ],
                juries: [

                    {
                        firstName: "Jennifer",

                        lastName: "De Groote",
                        juryId: 2
                    }, {
                        firstName: "Peter",

                        lastName: "Janssens",
                        juryId: 3
                    },

                    {
                        firstName: "Tom",
                        lastName: "Pieters",
                        juryId: 4
                    }

                ],
            };

            return {
                then: function(callback) {
                    return callback(mockObject);
                }
            }

            return
        }

        var getEvaluationFormMock = function(formId, juryId, candidateId) {
            var mockObject = {
                name: "Test eval form",

                description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Commodi odio, amet doloremque animi id ex autem inventore delectus consectetur ipsam, asperiores fugiat nam magnam fugit. Commodi, pariatur odio voluptas eum.",
                scoreMax: 6,
                scoreMin: 0,

                total: 6,
                score: 0,

                finalScore: 50,
                finalScoreMax: 100,
                finalScoreMin: 0,

                candidate: {
                    name: "John Doe",
                    candidateId: 1
                },
                competences: [{
                        name: "Competence 1",
                        competenceId: 1,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maxime, explicabo fuga quibusdam ex, blanditiis ea magni minima tempora at totam sit. Repellendus similique iusto cum officiis quibusdam facilis quaerat, enim?",
                        score: 5,
                        status: "success",
                        statusMessage: "Completed",
                        comment: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ex officiis a",
                        dimensions: [{
                            name: "Dimension 1.1 ",

                            dimensionId: 1,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 1,
                            notObserved: false

                        }, {
                            name: "Dimension 1.2 ",
                            dimensionId: 2,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 2,
                            notObserved: true


                        }, {
                            name: "Dimension 1.3 ",
                            dimensionId: 3,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 3,
                            notObserved: false


                        }, {
                            name: "Dimension 1.4 ",
                            dimensionId: 4,
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 4,
                            notObserved: false


                        }]

                    }, {
                        name: "Competence 2",
                        competenceId: 2,
                        score: 3,
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a.",
                        status: "warning",
                        statusMessage: "No comment written",
                        dimensions: [{
                            name: "Dimension 1.1 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 3,
                            notObserved: false


                        }, {
                            name: "Dimension 1.2 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 3,
                            notObserved: false


                        }, {
                            name: "Dimension 1.3 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 3,
                            notObserved: false


                        }, {
                            name: "Dimension 1.4 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: 3,
                            notObserved: false


                        }]

                    }

                    , {
                        name: "Competence 3",
                        competenceId: 3,
                        status: "neutral",

                        dimensions: [{
                            name: "Dimension 1.1 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: null

                        }, {
                            name: "Dimension 1.2 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",
                            score: null
                        }, {
                            name: "Dimension 1.3 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",

                            score: null

                        }, {
                            name: "Dimension 1.4 ",
                            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur distinctio eum expedita facere, dolorem id impedit dolore deleniti laborum aspernatur cumque maiores voluptatibus esse ipsum vero, alias qui at sint.",

                            score: null

                        }],

                        statusMessage: "Not evaluated yet",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."

                    }, {
                        name: "Competence 4",
                        competenceId: 4,
                        status: "neutral",
                        statusMessage: "Not evaluated yet",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }, {
                        name: "Competence 5",
                        competenceId: 5,
                        status: "neutral",
                        statusMessage: "Not evaluated yet",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }, {
                        name: "Competence 6",
                        competenceId: 6,
                        status: "neutral",
                        statusMessage: "Not evaluated yet",
                        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                    }
                ],

                candidates: [

                    {
                        name: "Sarah Van de Velde",
                        candidateId: 2
                    }, {
                        name: "Peter Janssens",
                        candidateId: 3
                    },

                    {
                        name: "Jan Pieters",
                        candidateId: 4
                    }

                ]
            }

            return {
                then: function(callback) {
                    return callback(mockObject);
                }
            }
        }

        var saveEvaluationFormMock = function(formId, juryId, candidateId, data) {

            return {
                then: function(callback) {
                    return callback(null);
                }

            }
        }

        var saveGeneralFormMock = function(formId, data) {

            return {
                then: function(callback) {
                    return callback(null);
                }

            }
        }

        var mockMode = false;


        if (mockMode) {

            //MockService API return
            return {

                getGeneralForm: getGeneralFormMock,
                getEvaluationForm: getEvaluationFormMock,
                getScreenings: getScreeningsMock,
                saveEvaluationForm: saveEvaluationFormMock,
                saveGeneralForm: saveGeneralFormMock
            };


        } else {

            //Service API return
            return {

                getGeneralForm: getGeneralForm,
                getEvaluationForm: getEvaluationForm,
                getScreenings: getScreenings,
                saveEvaluationForm: saveEvaluationForm,
                saveGeneralForm: saveGeneralForm

            };


        }


    };

    var module = angular.module("RevitApp");
    module.factory("revitService", revitService);

}());
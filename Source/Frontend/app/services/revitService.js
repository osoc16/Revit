(function() {

    var revitService = function($http, $log) {

        var mockMode = false;

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

                    for (var competenceKey in response.data.competences) {

                        var competence = response.data.competences[competenceKey];

                        if (competence.score == null) {
                            competence.score = NaN;
                        }
                        if (competence.finalScore == null) {
                            competence.finalScore = NaN;
                        }

                        for (var dimensionKey in competence.dimensions) {

                            var dimension = competence.dimensions[dimensionKey];
                            if (dimension.score == null) {
                                dimension.score = NaN;
                            }
                        }
                    }
                    
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
            $log.info("body");
            $log.info(data);

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

            var mockObject = mockRepository.screenings;

            return {
                then: function(callback) {
                    return callback(mockObject);
                }

            }
        }

        var getGeneralFormMock = function(formId) {
            var mockObject = mockRepository.generalForm;

            return {
                then: function(callback) {
                    return callback(mockObject);
                }
            }

            return
        }

        var getEvaluationFormMock = function(formId, juryId, candidateId) {
            var mockObject = mockRepository.evaluationForm;

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

    var mockRepository = {

        evaluationForm: {
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
                firstName: "John",
                lastName: "Doe",
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
                    firstName: "Sarah",
                    lastName: "Van de Velde",
                    candidateId: 2
                }, {
                    firstName: "Peter",
                    lastName: "Janssens",
                    candidateId: 3
                },

                {
                    firstName: "Jan",
                    lastName: "Pieters",
                    candidateId: 4
                }

            ]
        },
        generalForm: {
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
        },

        screenings:

        [{
            "screeningId": 1,
            "code": "c1",
            "name": "Screening 1",
            "name_FR": "scr_1fr",
            "name_NL": "scr_1nl",
            "name_EN": "scr_1en",
            "name_DE": "scr_1de",
            "description": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Odit est ab facere quo nobis alias fugiat, beatae, sint quidem culpa sapiente debitis delectus aspernatur accusantium. Iusto delectus porro autem deleniti.",
            "description_FR": "desc1fr",
            "description_NL": "desc1nl",
            "description_EN": "desc1en",
            "description_DE": "desc1de",
            "form": {
                "maxCandidates": 3,
                "formId": 1,
                "name": "form 1",
                "code": "code1",
                "description": "decription en 1",
                "description_FR": "decription fr 1",
                "description_NL": "decription nl 1",
                "description_EN": "decription en 1",
                "description_DE": "decription de 1",
                "scoreMin": null,
                "scoreMax": null,
                "total": null,
                "score": null,
                "finalScore": null,
                "finalScoreMax": 6,
                "finalScoreMin": 1,
                "candidate": null,
                "competences": [{
                    "competenceId": 1,
                    "name": "Competence en 1",
                    "name_FR": "Competence fr 1",
                    "name_EN": "Competence en 1",
                    "name_NL": "Competence nl 1",
                    "name_DE": "Competence de 1",
                    "code": "comp1",
                    "description": "desciption en 1",
                    "description_FR": "desciption fr 1",
                    "description_NL": "desciption nl 1",
                    "description_EN": "desciption en 1",
                    "description_DE": "desciption de 1",
                    "score": null,
                    "status": "",
                    "statusMessage": "",
                    "statusMessage_FR": "",
                    "statusMessage_NL": "",
                    "statusMessage_EN": "",
                    "statusMessage_DE": "",
                    "dimensions": [{
                        "dimensionId": 1,
                        "code": "dim1",
                        "name": "name en 1",
                        "name_FR": "name fr 1",
                        "name_EN": "name en 1",
                        "name_NL": "name nl 1",
                        "name_DE": "name de 1",
                        "description": "desciption en 1",
                        "description_FR": "desciption fr 1",
                        "description_NL": "desciption nl 1",
                        "description_EN": "desciption en 1",
                        "description_DE": "desciption de 1",
                        "notObserved": false,
                        "score": null,
                        "scores": [{
                            "scoreId": 2,
                            "result": 5,
                            "finalResult": 5,
                            "formId": 2,
                            "dimensionId": 1,
                            "competenceId": 1,
                            "candidateId": 1,
                            "juryId": null
                        }]
                    }, {
                        "dimensionId": 2,
                        "code": "dim2",
                        "name": "name en 2",
                        "name_FR": "name fr 2",
                        "name_EN": "name en 2",
                        "name_NL": "name nl 2",
                        "name_DE": "name de 2",
                        "description": "desciption en 2",
                        "description_FR": "desciption fr 2",
                        "description_NL": "desciption nl 2",
                        "description_EN": "desciption en 2",
                        "description_DE": "desciption de 2",
                        "notObserved": false,
                        "score": null,
                        "scores": []
                    }, {
                        "dimensionId": 3,
                        "code": "dim3",
                        "name": "name en 3",
                        "name_FR": "name fr 3",
                        "name_EN": "name en 3",
                        "name_NL": "name nl 3",
                        "name_DE": "name de 3",
                        "description": "desciption en 3",
                        "description_FR": "desciption fr 3",
                        "description_NL": "desciption nl 3",
                        "description_EN": "desciption en 3",
                        "description_DE": "desciption de 3",
                        "notObserved": false,
                        "score": null,
                        "scores": []
                    }],
                    "weight": 1,
                    "comment": "comment 1"
                }, {
                    "competenceId": 3,
                    "name": "Competence en 3",
                    "name_FR": "Competence fr 3",
                    "name_EN": "Competence en 3",
                    "name_NL": "Competence nl 3",
                    "name_DE": "Competence de 3",
                    "code": "comp3",
                    "description": "desciption en 3",
                    "description_FR": "desciption fr 3",
                    "description_NL": "desciption nl 3",
                    "description_EN": "desciption en 3",
                    "description_DE": "desciption de 3",
                    "score": null,
                    "status": "",
                    "statusMessage": "",
                    "statusMessage_FR": "",
                    "statusMessage_NL": "",
                    "statusMessage_EN": "",
                    "statusMessage_DE": "",
                    "dimensions": [{
                        "dimensionId": 1,
                        "code": "dim1",
                        "name": "name en 1",
                        "name_FR": "name fr 1",
                        "name_EN": "name en 1",
                        "name_NL": "name nl 1",
                        "name_DE": "name de 1",
                        "description": "desciption en 1",
                        "description_FR": "desciption fr 1",
                        "description_NL": "desciption nl 1",
                        "description_EN": "desciption en 1",
                        "description_DE": "desciption de 1",
                        "notObserved": false,
                        "score": null,
                        "scores": [{
                            "scoreId": 2,
                            "result": 5,
                            "finalResult": 5,
                            "formId": 2,
                            "dimensionId": 1,
                            "competenceId": 1,
                            "candidateId": 1,
                            "juryId": null
                        }]
                    }, {
                        "dimensionId": 4,
                        "code": "dim4",
                        "name": "name en 4",
                        "name_FR": "name fr 4",
                        "name_EN": "name en 4",
                        "name_NL": "name nl 4",
                        "name_DE": "name de 4",
                        "description": "desciption en 4",
                        "description_FR": "desciption fr 4",
                        "description_NL": "desciption nl 4",
                        "description_EN": "desciption en 4",
                        "description_DE": "desciption de 4",
                        "notObserved": false,
                        "score": null,
                        "scores": []
                    }],
                    "weight": 1,
                    "comment": "comment 3"
                }, {
                    "competenceId": 4,
                    "name": "Competence en 4",
                    "name_FR": "Competence fr 4",
                    "name_EN": "Competence en 4",
                    "name_NL": "Competence nl 4",
                    "name_DE": "Competence de 4",
                    "code": "comp4",
                    "description": "desciption en 4",
                    "description_FR": "desciption fr 4",
                    "description_NL": "desciption nl 4",
                    "description_EN": "desciption en 4",
                    "description_DE": "desciption de 4",
                    "score": null,
                    "status": "",
                    "statusMessage": "",
                    "statusMessage_FR": "",
                    "statusMessage_NL": "",
                    "statusMessage_EN": "",
                    "statusMessage_DE": "",
                    "dimensions": [],
                    "weight": 1,
                    "comment": "comment 4"
                }],
                "candidates": [{
                    "candidateId": 1,
                    "name": "ln1 fn1",
                    "firstName": "John",
                    "lastName": "Doe",
                    "nationalNumber": "nN1",
                    "juries": [{
                        "juryId": 1,
                        "name": null,
                        "firstName": "JF1",
                        "lastName": "JL1"
                    }, {
                        "juryId": 4,
                        "name": null,
                        "firstName": "JF3",
                        "lastName": "JL3"
                    }, {
                        "juryId": 5,
                        "name": null,
                        "firstName": "JF4",
                        "lastName": "JL4"
                    }]
                }, {
                    "candidateId": 2,
                    "name": "ln2 fn2",
                    "firstName": "fn2",
                    "lastName": "ln2",
                    "nationalNumber": "nN2",
                    "juries": null
                }],
                "juries": [{
                    "juryId": 1,
                    "name": null,
                    "firstName": "JF1",
                    "lastName": "JL1"
                }, {
                    "juryId": 4,
                    "name": null,
                    "firstName": "JF3",
                    "lastName": "JL3"
                }, {
                    "juryId": 5,
                    "name": null,
                    "firstName": "JF4",
                    "lastName": "JL4"
                }],
                "name_DE": "nde1",
                "name_FR": "nf1",
                "name_NL": "nnl1",
                "name_EN": "nen1"
            },
            "juries": [{
                "juryId": 1,
                "name": null,
                "firstName": "JF1",
                "lastName": "JL1"
            }, {
                "juryId": 3,
                "name": null,
                "firstName": "JF2",
                "lastName": "JL2"
            }],
            "candidates": [{
                "candidateId": 1,
                "name": "ln1 fn1",
                "firstName": "fn1",
                "lastName": "ln1",
                "nationalNumber": "nN1",
                "juries": null
            }, {
                "candidateId": 2,
                "name": "ln2 fn2",
                "firstName": "fn2",
                "lastName": "ln2",
                "nationalNumber": "nN2",
                "juries": null
            }]
        }, {
            "screeningId": 2,
            "code": "c2",
            "name": "Screening 2",
            "name_FR": "scr_2fr",
            "name_NL": "scr_2nl",
            "name_EN": "scr_2en",
            "name_DE": "scr_2de",
            "description": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. A, dolores doloribus, laboriosam dolorem dolor excepturi vero ipsum nostrum non? Beatae vero, molestias sed. Voluptatum, quidem mollitia incidunt iste error voluptate.",
            "description_FR": "desc2fr",
            "description_NL": "desc2nl",
            "description_EN": "desc2en",
            "description_DE": "desc2de",
            "form": {
                "maxCandidates": 3,
                "formId": 2,
                "name": "etjetjetj",
                "code": "code2",
                "description": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reprehenderit ipsa, quisquam quod asperiores. At vel consectetur praesentium recusandae dolore quidem quasi sed libero, fuga ea sapiente quod, asperiores, doloribus eos!",
                "description_FR": "decription fr 2",
                "description_NL": "decription nl 2",
                "description_EN": "decription en 2",
                "description_DE": "decription de 2",
                "scoreMin": null,
                "scoreMax": null,
                "total": null,
                "score": null,
                "finalScore": null,
                "finalScoreMax": 6,
                "finalScoreMin": 1,
                "candidate": null,
                "competences": [{
                    "competenceId": 1,
                    "name": "Competence en 1",
                    "name_FR": "Competence fr 1",
                    "name_EN": "Competence en 1",
                    "name_NL": "Competence nl 1",
                    "name_DE": "Competence de 1",
                    "code": "comp1",
                    "description": "desciption en 1",
                    "description_FR": "desciption fr 1",
                    "description_NL": "desciption nl 1",
                    "description_EN": "desciption en 1",
                    "description_DE": "desciption de 1",
                    "score": null,
                    "status": "",
                    "statusMessage": "",
                    "statusMessage_FR": "",
                    "statusMessage_NL": "",
                    "statusMessage_EN": "",
                    "statusMessage_DE": "",
                    "dimensions": [{
                        "dimensionId": 1,
                        "code": "dim1",
                        "name": "name en 1",
                        "name_FR": "name fr 1",
                        "name_EN": "name en 1",
                        "name_NL": "name nl 1",
                        "name_DE": "name de 1",
                        "description": "desciption en 1",
                        "description_FR": "desciption fr 1",
                        "description_NL": "desciption nl 1",
                        "description_EN": "desciption en 1",
                        "description_DE": "desciption de 1",
                        "notObserved": false,
                        "score": null,
                        "scores": [{
                            "scoreId": 2,
                            "result": 5,
                            "finalResult": 5,
                            "formId": 2,
                            "dimensionId": 1,
                            "competenceId": 1,
                            "candidateId": 1,
                            "juryId": null
                        }]
                    }, {
                        "dimensionId": 2,
                        "code": "dim2",
                        "name": "name en 2",
                        "name_FR": "name fr 2",
                        "name_EN": "name en 2",
                        "name_NL": "name nl 2",
                        "name_DE": "name de 2",
                        "description": "desciption en 2",
                        "description_FR": "desciption fr 2",
                        "description_NL": "desciption nl 2",
                        "description_EN": "desciption en 2",
                        "description_DE": "desciption de 2",
                        "notObserved": false,
                        "score": null,
                        "scores": []
                    }, {
                        "dimensionId": 3,
                        "code": "dim3",
                        "name": "name en 3",
                        "name_FR": "name fr 3",
                        "name_EN": "name en 3",
                        "name_NL": "name nl 3",
                        "name_DE": "name de 3",
                        "description": "desciption en 3",
                        "description_FR": "desciption fr 3",
                        "description_NL": "desciption nl 3",
                        "description_EN": "desciption en 3",
                        "description_DE": "desciption de 3",
                        "notObserved": false,
                        "score": null,
                        "scores": []
                    }],
                    "weight": 1,
                    "comment": "comment 1"
                }, {
                    "competenceId": 5,
                    "name": "Competence en 5",
                    "name_FR": "Competence fr 5",
                    "name_EN": "Competence en 5",
                    "name_NL": "Competence nl 5",
                    "name_DE": "Competence de 5",
                    "code": "comp5",
                    "description": "desciption en 5",
                    "description_FR": "desciption fr 5",
                    "description_NL": "desciption nl 5",
                    "description_EN": "desciption en 5",
                    "description_DE": "desciption de 5",
                    "score": null,
                    "status": "",
                    "statusMessage": "",
                    "statusMessage_FR": "",
                    "statusMessage_NL": "",
                    "statusMessage_EN": "",
                    "statusMessage_DE": "",
                    "dimensions": [],
                    "weight": 1,
                    "comment": "comment 5"
                }],
                "candidates": [{
                    "candidateId": 1,
                    "name": "ln1 fn1",
                    "firstName": "fn1",
                    "lastName": "ln1",
                    "nationalNumber": "nN1",
                    "juries": null
                }, {
                    "candidateId": 4,
                    "name": "ln4 fn4",
                    "firstName": "fn4",
                    "lastName": "ln4",
                    "nationalNumber": "nN4",
                    "juries": null
                }],
                "juries": [{
                    "juryId": 3,
                    "name": null,
                    "firstName": "JF2",
                    "lastName": "JL2"
                }],
                "name_DE": "nde2",
                "name_FR": "nfr2",
                "name_NL": "nnl2",
                "name_EN": "nen2"
            },
            "juries": [{
                "juryId": 1,
                "name": null,
                "firstName": "JF1",
                "lastName": "JL1"
            }],
            "candidates": [{
                "candidateId": 1,
                "name": "ln1 fn1",
                "firstName": "fn1",
                "lastName": "ln1",
                "nationalNumber": "nN1",
                "juries": null
            }, {
                "candidateId": 3,
                "name": "ln3 fn3",
                "firstName": "fn3",
                "lastName": "ln3",
                "nationalNumber": "nN3",
                "juries": null
            }]
        }, {
            "screeningId": 3,
            "code": "c2",
            "name": "Screening 3 ",
            "name_FR": "scr_3fr",
            "name_NL": "scr_3nl",
            "name_EN": "scr_3en",
            "name_DE": "scr_3de",
            "description": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Provident ratione saepe rerum quisquam enim nostrum, temporibus tenetur, consequuntur soluta, mollitia nihil voluptate placeat optio voluptatibus fugiat. Libero, repudiandae vitae autem?",
            "description_FR": "desc3fr",
            "description_NL": "desc3nl",
            "description_EN": "desc3en",
            "description_DE": "desc3de",
            "form": {
                "maxCandidates": 3,
                "formId": 3,
                "name": "form 3",
                "code": "code3",
                "description": "decription en 3",
                "description_FR": "decription fr 3",
                "description_NL": "decription nl 3",
                "description_EN": "decription en 3",
                "description_DE": "decription de 3",
                "scoreMin": null,
                "scoreMax": null,
                "total": null,
                "score": null,
                "finalScore": null,
                "finalScoreMax": 6,
                "finalScoreMin": 1,
                "candidate": null,
                "competences": [],
                "candidates": [],
                "juries": [],
                "name_DE": "nde3",
                "name_FR": "nfr3",
                "name_NL": "nnl3",
                "name_EN": "nen3"
            },
            "juries": [],
            "candidates": []
        }]
    }

}());
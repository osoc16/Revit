(function() {

    var revitService = function($http) {

        /* Variables */
        var apiBaseUrl = "";

        /*
      var getUser = function(username){
            return $http.get("https://api.github.com/users/" + username)
                        .then(function(response){
                           return response.data; 
                        });
      };
      
      var getRepos = function(user){
            return $http.get(user.repos_url)  
                        .then(function(response){
                            return response.data;
                        });
      };*/





        /*Functions*/

        var getGeneralForm = function(formId) {

            return {
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
                        lastName:"Van de Velde",
                        candidateId: 2,
                        juries:[]
                    }, {
                        firstName: "Peter Janssens",
                        candidateId: 3,
                        juries:[]
                    },

                    {
                        firstName: "Jan",
                        lastName:"Van de Velde",
                        candidateId: 4,
                        juries:[]
                    }

                ],
                juries: [

                    {
                        firstName: "Jennifer",

                        lastName:"De Groote",
                        juryId: 2
                    }, {
                        firstName: "Peter",

                        lastName:"Janssens",
                        juryId: 3
                    },

                    {
                        firstName: "Tom",
                        lastName:"Pieters",
                        juryId: 4
                    }

                ],

            };

        }



        var getForm = function(formId, juryId, candidateId) {

            return {
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
            };

        }




        //Service API return
        return {

            getGeneralForm: getGeneralForm,
            getForm: getForm
        };

    };

    var module = angular.module("RevitApp");
    module.factory("revitService", revitService);

}());
(function() {

    var revitService = function($http) {

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

        var getForm = function(formId, juryId, candidateId) {


            return {
                name: "Test eval form",

                description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Commodi odio, amet doloremque animi id ex autem inventore delectus consectetur ipsam, asperiores fugiat nam magnam fugit. Commodi, pariatur odio voluptas eum.",
                scoreMax: 6,
                scoreMin: 0,

                total: 6,


                candidate: {
                    name: "John Doe",
                    candidateId: 1
                },
                competences: [{
                    name: "Competence 1",
                    competenceId:1,
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Maxime, explicabo fuga quibusdam ex, blanditiis ea magni minima tempora at totam sit. Repellendus similique iusto cum officiis quibusdam facilis quaerat, enim?",
                    score: 5,
                    status: "success",
                    statusMessage: "Completed",
                    dimensions: [{
                        name: "Dimension 1 "


                    }]

                }, {
                    name: "Competence 2",
                       competenceId:2,
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a.",
                    status: "warning",
                    statusMessage: "No comment written"

                }, {
                    name: "Competence 3",
                            competenceId:3,
                    status: "neutral",
                    statusMessage: "Not evaluated yet",
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                }, {
                    name: "Competence 4",
                            competenceId:4,
                     status: "neutral",
                    statusMessage: "Not evaluated yet",
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                },
                {
                    name: "Competence 5",
                            competenceId:5,
                     status: "neutral",
                    statusMessage: "Not evaluated yet",
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                },
                {
                    name: "Competence 6",
                            competenceId:6,
                     status: "neutral",
                    statusMessage: "Not evaluated yet",
                    description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates accusantium deserunt veniam. Repudiandae expedita error facilis tempora maiores voluptate accusamus incidunt nemo necessitatibus. Nihil modi nulla officia corporis perferendis a."
                }],

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


        return {
            getForm: getForm,
        };

    };

    var module = angular.module("RevitApp");
    module.factory("revitService", revitService);

}());
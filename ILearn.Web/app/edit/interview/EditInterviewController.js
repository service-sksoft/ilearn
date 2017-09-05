(function () {
    'use strict';
    angular
        .module('app')
        .controller('EditInterviewController', EditInterviewController)

    EditInterviewController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', '$sce', 'Url', 'fileUpload'];
    function EditInterviewController($scope, $state, Util_Common, commonService, $sce, Url, fileUpload) {
        var vm = this;

        Util_Common.bindCommonFunctions(vm);

        vm.quesTypes = [
            { ID: 1, Name: 'Question' },
            { ID: 2, Name: 'External Reference' }
        ];

        vm.quesType = 1;

        vm.QuestionType = [
            { ID: 1, Name: 'Subjective' },
            { ID: 2, Name: 'One-Liner' }
        ];

        vm.erTypes = [
            { ID: 1, Name: 'PDF' },
            { ID: 2, Name: 'Video' },
            { ID: 3, Name: 'Word Document' }
        ];

        vm.loadQuestion = function () {
            switch (vm.quesType) {
                case 1: getQuestions(); break;
                case 2: getExternalResource(); break;
            }
        };

        vm.topic = {};

        initRequest();

        function initRequest() {
            vm.topicId = $state.params.topicId || -1;
            getQuestions();
        }

        // Question Section Starts
        function getQuestions() {
            commonService.getAllQuestions(vm.topicId).then(function (data) {
                vm.questionList = data.result;
            }, function () {
                Util_Common.toasterError("Error Loading Question", "Some error occured while loading questions.");
            });
        }

        vm.editQuestion = function (id) {
            if (id === 0) {
                $("#txtAnswer").val('');
                vm.question = { Language: vm.topicId, ID: 0 };
                window.setTimeout(function () { Util_Common.initCleditor('txtAnswer'); }, 200)
                return;
            }

            commonService.getQuestionDetail(id).then(function (data) {
                vm.question = data.ques;
            }).finally(function () {
                window.setTimeout(function () { Util_Common.initCleditor('txtAnswer'); }, 200)
            });
        }

        vm.saveQuestion = function () {

            var inputDTO = {
                ID: vm.question.ID,
                Question: vm.question.Question,
                Answer: $("#txtAnswer").val(),
                Language: vm.topicId,
                Type: vm.question.Type
            };

            commonService.saveQuestion(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("Question Saved", "Question added/updated successfully.");
                $('#myModal').modal('toggle');
                getQuestions();
            }, function (err) {
                Util_Common.toasterError("Question Not Saved", "Question not added/updated.");
            });
        }

        vm.deleteQuestion = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete question?");
            if (resp) {
                commonService.deleteQuestion(ID).then(function (data) {
                    if (data.result) {
                        Util_Common.toasterSuccess("Question Deleted", "Question deleted successfully.");
                        getQuestions();
                    }
                    else
                        Util_Common.toasterError("Question Not Deleted", "Question not deleted.");
                }, function () {
                    Util_Common.toasterError("Question Not Deleted", "Question not deleted.");
                });
            }
        }

        // Question Section Ends

        // External Resource Section Starts
        function getExternalResource() {
            commonService.getExternalResources(vm.topicId).then(function (data) {
                vm.externalResourceList = data.results;
            }, function () {
                Util_Common.toasterError("Error Loading External Resource", "Some error occured while loading external resources.");
            });
        }

        vm.editResource = function (index) {
            if (index == -1) {
                vm.er = { TopicID: vm.topicId, ID: 0 };
                return;
            }
            vm.er = angular.copy(vm.externalResourceList[index]);
        }

        vm.saveExternalResource = function () {
            var inputDTO = {
                ID: vm.er.ID,
                Title: vm.er.Title,
                URL: vm.er.URL,
                Type: vm.er.Type,
                TopicID: vm.er.TopicID,
                Description: vm.er.Description
            };

            commonService.saveExternalResource(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("External Resource Saved", "External resource added/updated successfully.");
                $('#myModal').modal('toggle');
                getExternalResource();
            }, function (err) {
                Util_Common.toasterError("External Resource Not Saved", "External resource not added/updated.");
            });
        }

        vm.deleteExternalResource = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete external reference?");
            if (resp) {
                commonService.deleteExternalResource(ID).then(function (data) {
                    if (data.results) {
                        Util_Common.toasterSuccess("External Resource Deleted", "Externa resource deleted successfully.");
                        getExternalResource();
                    }
                    else
                        Util_Common.toasterError("External Resource Not Deleted", "External Resource not deleted.");
                }, function () {
                    Util_Common.toasterError("External Resource Deleted", "External Resource not deleted.");
                });
            }
        }

        // External Resource Section Ends
    }
})();

(function () {
    'use strict';
    angular
        .module('app')
        .controller('EditMultipleChoiceController', EditMultipleChoiceController)

    EditMultipleChoiceController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', '$sce', 'Url', 'fileUpload'];
    function EditMultipleChoiceController($scope, $state, Util_Common, commonService, $sce, Url, fileUpload) {
        var vm = this;

        Util_Common.bindCommonFunctions(vm);

        vm.topic = {};

        initRequest();

        function initRequest() {
            vm.topicId = $state.params.topicId || -1;
            getQuestions();
        }

        // Question Section Starts
        function getQuestions() {
            commonService.getOnlyMultipleChoiceQuestions(vm.topicId).then(function (data) {
                vm.multipleChoiceList = data.result;
            }, function () {
                Util_Common.toasterError("Error Loading Multiple Choice", "Some error occured while loading multiple choice questions.");
            });
        }

        vm.editMultipleChoice = function (id) {
            if (id === 0) {
                vm.mc = { Language: vm.topicId, ID: 0 };
                return;
            }
            commonService.getMultipleChoiceQuestionDetail(id).then(function (data) {
                vm.mc = data.result;
            }).finally(function () {
            });
        }

        vm.saveMultipleChoice = function () {

            var inputDTO = {
                ID: vm.mc.ID,
                Question: vm.mc.Question,
                Answer: vm.mc.Answer,
                Option1: vm.mc.Option1,
                Option2: vm.mc.Option2,
                Option3: vm.mc.Option3,
                Option4: vm.mc.Option4,
                Option5: vm.mc.Option5,
                Language: vm.topicId
            };

            commonService.saveMultipleChoice(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("Multiple Choice Saved", "Multiple Choice added/updated successfully.");
                $('#myModal').modal('toggle');
                getQuestions();
            }, function (err) {
                Util_Common.toasterError("Multiple Choice Not Saved", "Multiple choice not added/updated.");
            });
        }

        vm.deleteMultipleChoice = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete question?");
            if (resp) {
                commonService.deleteMultipleChoice(ID).then(function (data) {
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

    }
})();

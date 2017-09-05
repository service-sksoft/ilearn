
(function () {
    'use strict';

    angular
        .module('app')
        .controller('InterviewQuestionController', InterviewQuestionController);

    InterviewQuestionController.$inject = ['$scope', '$state', 'commonService', '$sce', 'Util_Common'];
    function InterviewQuestionController($scope, $state, commonService, $sce, Util_Common) {
        var vm = this;
        vm.$sce = $sce;
        vm.topicList = [];

        Util_Common.bindCommonFunctions(vm, initRequest);

        function initRequest() {
            vm.url = $state.params.url || '';
            vm.topicList = Util_Common.topicList;

            vm.selectedTopic = _.filter(Util_Common.topicList, function (e) { return e.URL == vm.url })[0];

            vm.id = vm.selectedTopic ? vm.selectedTopic.ID : 1;

            vm.pageNo = $state.params.page || 1;
            getQuestions();

            vm.scrollTop();
        }

        function getQuestions() {
            var questions = Util_Common.question.getList(vm.id, vm.pageNo, Util_Common.QUESTYPE.INTERVIEW);
            if (questions.ques && questions.ques.length > 0) {
                vm.questionList = questions;

                return;
            }

            commonService.getAllQuestionsOfTopic(vm.id, vm.pageNo).then(function (data) {
                vm.questionList = data;
                Util_Common.question.setList(vm.id, vm.pageNo, Util_Common.QUESTYPE.INTERVIEW, data);

            });
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExternalResourceController', ExternalResourceController);

    ExternalResourceController.$inject = ['$scope', '$state', 'commonService', '$sce', 'Util_Common'];
    function ExternalResourceController($scope, $state, commonService, $sce, Util_Common) {
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
            var questions = Util_Common.question.getList(vm.id, vm.pageNo, Util_Common.QUESTYPE.EXTERNAL_RESOURCE);
            if (questions.ques && questions.ques.length > 0) {
                vm.questionList = questions;

                return;
            }

            commonService.getAllExternalResources(vm.id, vm.pageNo).then(function (data) {
                vm.questionList = data;
                Util_Common.question.setList(vm.id, vm.pageNo, Util_Common.QUESTYPE.EXTERNAL_RESOURCE, data);

            });
        }

        vm.contentImage = function (type) {
            switch (type) {
                case vm.ER_CONTENT_TYPE.VIDEO: return 'images/video.png'; break;
                case vm.ER_CONTENT_TYPE.PDF: return 'images/pdf.png'; break;
                case vm.ER_CONTENT_TYPE.DOCUMENT: return 'images/document.png'; break;
            }
            return 'images/document.png';
        };
    }
})();

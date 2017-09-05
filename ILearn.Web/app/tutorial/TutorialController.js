
(function () {
    'use strict';

    angular
        .module('app')
        .controller('TutorialController', TutorialController);

    TutorialController.$inject = ['$scope', '$state', 'commonService', '$sce', 'Util_Common'];
    function TutorialController($scope, $state, commonService, $sce, Util_Common) {
        var vm = this;
        vm.$sce = $sce;
        vm.topicList = [];

        Util_Common.bindCommonFunctions(vm, initRequest);

        function initRequest() {
            
            vm.url = $state.params.url || '';
            vm.topicList = Util_Common.topicList;

            vm.selectedTopic = _.filter(Util_Common.topicList, function (e) { return e.URL == vm.url })[0];

            vm.id = vm.selectedTopic ? vm.selectedTopic.ID : 1;

            vm.subtitle = $state.params.subtitle || null;

            getTitle();

            vm.scrollTop();
        }

        function getTitle() {
            //var questions = Util_Common.question.getList(vm.id, vm.subtitle, Util_Common.QUESTYPE.TUTORIAL);
            //if (questions.ques && questions.ques.length > 0) {
            //    vm.questionList = questions;
            //    getTutorialContent();
            //    return;
            //}

            commonService.getAllTutorialTitle(vm.id, vm.subtitle).then(function (data) {
                vm.questionList = data;
                //Util_Common.question.setList(vm.id, vm.subtitle, Util_Common.QUESTYPE.TUTORIAL, data);
                getTutorialContent();
            });
        }

        function getTutorialContent() {
            if (vm.subtitle == null) {
                if (vm.questionList.titles.length > 0 && vm.questionList.titles[0].subtitles.length > 0)
                    vm.subtitle = vm.questionList.titles[0].subtitles[0].id;
            }

            if (!vm.subtitle) {
                return;
            }

            commonService.getTutorialContent(vm.subtitle).then(function (data) {
                vm.tutorialContents = data.result;
            });
        }

        vm.highlightedContent = function (q) {
            return vm.$sce.trustAsHtml(Prism.highlight(q.desc, Prism.languages.javascript));
        };
        }
    }) ();

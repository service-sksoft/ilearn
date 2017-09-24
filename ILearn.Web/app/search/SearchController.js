
(function () {
    'use strict';

    angular
        .module('app')
        .controller('SearchController', SearchController);

    SearchController.$inject = ['$scope', '$state', 'commonService', '$sce', 'Url', 'Util_Common'];
    function SearchController($scope, $state, commonService, $sce, Url, Util_Common) {
        var vm = this;
        vm.$sce = $sce;
        vm.questionList = [];
        Util_Common.bindCommonFunctions(vm, initRequest);

        function initRequest() {
            vm.id = ($state.params.term || '').trim();
            if (vm.id.length > 0) {
                search(vm.id);
            }
            else { vm.questionList = []; }
            vm.scrollTop();
        }

        function search(searchTerm) {
            commonService.searchQuestions(searchTerm).then(function (data) {
                angular.forEach(data.result, function (elm) {
                    elm.topic = _.filter(Util_Common.topicList, function (e) { return e.ID == elm.t })[0];
                });
                vm.questionList = data.result;
            }, function () { });
        };
    }
})();

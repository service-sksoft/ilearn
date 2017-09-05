(function () {
    'use strict';
    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', '$state', 'Util_Common'];
    function HomeController($scope, $state, Util_Common) {
        var vm = this;
        Util_Common.bindCommonFunctions(vm, initRequest);

        function initRequest() {
            vm.topicList = Util_Common.topicList;
        }
    }
})();

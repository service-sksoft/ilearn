
(function () {
    'use strict';

    angular
        .module('app')
        .controller('HeaderController', HeaderController);

    HeaderController.$inject = ['$scope', '$rootScope', '$sce', '$state', 'commonService','cacheFactory'];
    function HeaderController($scope, $rootScope, $sce, $state, commonService, cacheFactory) {
        var vm = this;

        vm.menuRow1 = [
            { 'label': 'Tutorials', state: 'default' },
            { 'label': 'Web Tools', state: 'default' }
        ];

        vm.showModel = function () {
            vm.modalTemplate = 'app/layout/dir-menu.view.html';
            $('#myModal').modal({
                keyboard: false
            });
        }

        vm.reloadData = function () {
            commonService.reloadData().then(function (data) {
                cacheFactory.clear();
                location.reload();
            })
        };
    }
})();

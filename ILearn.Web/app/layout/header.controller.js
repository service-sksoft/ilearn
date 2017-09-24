
(function () {
    'use strict';

    angular
        .module('app')
        .controller('HeaderController', HeaderController);

    HeaderController.$inject = ['$scope', '$rootScope', '$sce', '$state', 'commonService', 'cacheFactory', 'Util_Common'];
    function HeaderController($scope, $rootScope, $sce, $state, commonService, cacheFactory, Util_Common) {
        var vm = this;
        vm.logoImage = Util_Common.logoImage;
        vm.menuRow1 = [
            { 'label': 'Tutorials', state: 'default' },
            { 'label': 'Web Tools', state: 'web-tools' }
        ];


        vm.currentState = $state.current.name;
        var isMenuRowMatching = _.filter(vm.menuRow1, function (e) { return e.state == vm.currentState })[0];
        if (!isMenuRowMatching) {
            vm.currentState = vm.menuRow1[0].state;
        }
        
        //highlight menu item based on page type
        vm.setActivePage = function (currentstate) {
            vm.currentState = currentstate;
        };

        vm.showModel = function () {
            vm.modalTemplate = 'app/layout/dir-menu.view.html';
            $('#myModal').modal({
                keyboard: false
            });
        }

        //vm.reloadData = function () {
        //    commonService.reloadData().then(function (data) {
        //        cacheFactory.clear();
        //        location.reload();
        //    })
        //};
    }
})();

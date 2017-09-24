(function () {
    'use strict';
    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', 'cacheFactory'];
    function HomeController($scope, $state, Util_Common, commonService, cacheFactory) {
        var vm = this;
        Util_Common.bindCommonFunctions(vm, initRequest);

        vm.homeBannerImage = Util_Common.homeBannerImage;

        vm.carousalQuestions = [];

        function initRequest() {
            vm.topicList = Util_Common.topicList;
            getCarousalQuestions();
        }

        function getCarousalQuestions() {
            if ($(window).width() < 753) {
                $(".home-scroller").hide();
                return;
            }

            var buildCarousal = function (list) {
                vm.carousalQuestions = [];
                var l = list.length;
                for (var i = 0; i < l; i = i + 3) {
                    if (i + 2 < l) {
                        vm.carousalQuestions.push(
                            {
                                q1: list[i].q, a1: $(list[i].a).text(),
                                q2: list[i + 1].q, a2: $(list[i + 1].a).text(),
                                q3: list[i + 2].q, a3: $(list[i + 2].a).text()
                            });
                    }
                }

                window.setTimeout(function () {
                    $(".home-scroller").show();
                    $('.carousel').carousel();
                }, 200);
            };

            var carousalInCache = cacheFactory.get('carousalcontent', []);
            if (carousalInCache.length > 0) {
                window.setTimeout(function () {
                    buildCarousal(carousalInCache);
                });
                return;
            }

            commonService.getQuestionsForCarousal().then(function (resp) {
                if (resp.result.length > 0) {
                    cacheFactory.set('carousalcontent', resp.result);
                    buildCarousal(resp.result);
                }
                else {
                    $(".home-scroller").hide();
                }
            }, function () {
                $(".home-scroller").hide();
            });
        }

        vm.searchByEnterKey = function (evt) {
            if ((evt.keyCode || evt.which) === 13) {
                vm.search(evt.target.value);
            }
        };

        vm.search = function (searchTerm) {
            $state.go('search', { term: searchTerm });
        };
    }
})();

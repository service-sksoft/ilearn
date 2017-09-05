(function () {
    'use strict';
    angular.module('app', ['ui.router', 'oc.lazyLoad', 'toaster', 'chieffancypants.loadingBar'])
    .config(config);
    config.$inject = ['$httpProvider', '$stateProvider', '$urlRouterProvider', '$provide', '$sceDelegateProvider', '$ocLazyLoadProvider', 'cfpLoadingBarProvider'];
    function config($httpProvider, $stateProvider, $urlRouterProvider, $provide, $sceDelegateProvider, $ocLazyLoadProvider, cfpLoadingBarProvider) {

        cfpLoadingBarProvider.includeSpinner = true;

        $ocLazyLoadProvider.config({
            debug: true,
            jsLoader: requirejs
        });

        $stateProvider
            .state('header', {
                templateUrl: 'app/layout/header.view.html',
                controller: 'HeaderController',
                controllerAs: 'vm',
                abstract: true,
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/layout/header.controller.js",
                        "app/services/app.urls.js",
                        "app/services/common.service.js",
                        "app/services/lookup.service.js",
                        "app/services/utility.common.js",
                        "app/services/cacheFactory.js",
                        "app/directives/utility.directives.js"
                        ]);
                    }]
                }
            })
            .state('default', {
                url: '/',
                parent: 'header',
                templateUrl: 'app/home/home.html',
                controller: 'HomeController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/home/HomeController.js"]);
                    }]
                }
            })
            .state('edit', {
                url: '/edit',
                parent: 'header',
                templateUrl: 'app/edit/edit.html',
                controller: 'EditController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/edit/EditController.js"]);
                    }]
                }
            })
            .state('edit-topic', {
                url: '/edit-topic',
                parent: 'edit',
                templateUrl: 'app/edit/edit-topic.html',
                controller: 'EditController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/edit/EditController.js"]);
                    }]
                }
            })
            .state('edit-interview', {
                url: '/edit-interview/:topicId',
                parent: 'edit',
                templateUrl: 'app/edit/interview/edit-interview.html',
                controller: 'EditInterviewController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/edit/interview/EditInterviewController.js"]);
                    }]
                }
            })
            .state('edit-multiple-choice', {
                url: '/edit-multiple-choice/:topicId',
                parent: 'edit',
                templateUrl: 'app/edit/multiple-choice/edit-multiple-choice.html',
                controller: 'EditMultipleChoiceController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/edit/multiple-choice/EditMultipleChoiceController.js"]);
                    }]
                }
            })
            .state('edit-tutorial', {
                url: '/edit-tutorial/:topicId',
                parent: 'edit',
                templateUrl: 'app/edit/tutorial/edit-tutorial.html',
                controller: 'EditTutorialController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load(["app/edit/tutorial/EditTutorialController.js"]);
                    }]
                }
            })
            .state('interview', {
                url: '/interview/:url/:page',
                parent: 'header',
                templateUrl: 'app/interview-question/interview-question.html',
                controller: 'InterviewQuestionController',
                controllerAs: 'vm',
                resolve: {
                    loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                        return $ocLazyLoad.load([
                        "app/interview-question/InterviewQuestionController.js"
                        ]);
                    }]
                }
            })
        .state('multiple-choice', {
            url: '/multiple-choice/:url/:page',
            parent: 'header',
            templateUrl: 'app/multiple-choice/multiple-choice.html',
            controller: 'MultipleChoiceController',
            controllerAs: 'vm',
            resolve: {
                loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                    return $ocLazyLoad.load([
                    "app/multiple-choice/MultipleChoiceController.js"
                    ]);
                }]
            }
        })
        .state('external-resource', {
            url: '/external-resource/:url/:page',
            parent: 'header',
            templateUrl: 'app/external-resource/external-resource.html',
            controller: 'ExternalResourceController',
            controllerAs: 'vm',
            resolve: {
                loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                    return $ocLazyLoad.load([
                    "app/external-resource/ExternalResourceController.js"
                    ]);
                }]
            }
        })
        .state('tutorial', {
            url: '/tutorial/:url/:subtitle',
            parent: 'header',
            templateUrl: 'app/tutorial/tutorial.html',
            controller: 'TutorialController',
            controllerAs: 'vm',
            resolve: {
                loadMyCtrl: ["$ocLazyLoad", "$rootScope", function ($ocLazyLoad, $rootScope) {
                    return $ocLazyLoad.load([
                    "app/tutorial/TutorialController.js",
                    "js/prism.js",
                    "css/prism.css"
                    ]);
                }]
            }
        });

        $urlRouterProvider.otherwise('/');

        $sceDelegateProvider.resourceUrlWhitelist(['self']);

        $httpProvider.interceptors.push(['$q', '$rootScope',
        function ($q, $rootScope) {
            return {
                response: function (response) {
                    return $q.resolve(response);
                },
                responseError: function (response) {
                    return $q.reject(response);
                }
            }
        }]);

        $provide.decorator('$exceptionHandler', globalExceptionHandler);

        globalExceptionHandler.$inject = ['$delegate', '$injector'];
        function globalExceptionHandler($delegate, $injector) {
            return function (exception, cause) {
                $delegate(exception, cause);
                var errorData = {
                    exception: exception,
                };
            };
        }

        run.$inject = ['$rootScope', '$location', '$http', '$state'];
        function run($rootScope, $location, $http, $state) {
            //$http.defaults.headers.common['XSRF-TOKEN'] = $rootScope.xsrf;
        }
    }
})();


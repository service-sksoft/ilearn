(function () {
    'use strict';
    angular.module('app')
        .factory('Util_Common', Util_Common);

    Util_Common.$inject = ['$http', 'Url', '$state', '$rootScope', '$q','$sce', 'cacheFactory', 'toaster'];
    function Util_Common($http, Url, $state, $rootScope, $q, $sce, cacheFactory, toaster) {
        var common = {};

        common.topicList = [];
        common.questionList = [];

        common.QUESTYPE = {
            INTERVIEW: 0,
            MULTIPLE_CHOICE: 1,
            EXTERNAL_RESOURCE: 2,
            TUTORIAL: 3
        };

        common.ER_CONTENT_TYPE = {
            PDF: 1,
            VIDEO: 2,
            DOCUMENT: 3
        };


        common.bindCommonFunctions = function (vm, cb) {
            vm.getImageUrl = common.getImageUrl;
            vm.$sce = $sce;
            vm.API_BASE = Url.API_BASE;
            vm.BACK_END = Url.BACK_END;
            vm.scrollTop = common.scrollTop;
            vm.ER_CONTENT_TYPE = common.ER_CONTENT_TYPE;
            common.topic.load(cb);
        };

        common.scrollTop = function () {
            $("html, body").stop().animate({ scrollTop: 100 }, 500, 'swing', function () { });
        };

        common.topic = {
            load: function (callback) {
                common.topicList = cacheFactory.get('topics', []);

                if (common.topicList.length === 0) {
                    common.topic.getList(function () {
                        (callback || angular.noop)();
                    });
                }
                else 
                    (callback || angular.noop)();
            },
            setList: function (lst) {
                common.topicList = lst;
                cacheFactory.set('topics', lst);
            },
            getList: function (callback) {
                common.callWebApi(Url.API_BASE + '/' + Url.TOPIC + '/alltopics').then(function (data) {
                    var lst = data.result
                    common.topicList = lst;
                    cacheFactory.set('topics', lst);
                    (callback || angular.noop)();
                }, function () {
                    (callback || angular.noop)();
                });
            }
        };

        common.question = {
            key: function (url, pageNo, quesType) {
                return 'questions-' + url + "-" + pageNo + "-" + quesType;
            },
            setList: function (url, pageNo, quesType, lst) {

                if (common.questionList.length > 25) {
                    common.questionList.splice(0, 20);
                }

                var key = this.key(url, pageNo, quesType);
                common.questionList[key] = lst;
                cacheFactory.set(key, lst);
            },
            getList: function (url, pageNo, quesType) {
                var key = this.key(url, pageNo, quesType);
                return cacheFactory.get(key, []);
            }
        };

        common.isNullEmptyOrWhiteSpace = function (txt) {
            if (txt === null || txt === undefined)
                return true;

            return (txt || '').length === 0;
        };

        common.getImageUrl = function (imagePath) {
            if (!common.isNullEmptyOrWhiteSpace(imagePath)) {
                return Url.BACK_END + imagePath;
            }
            return "";
        };

        common.callWebApi = function (url, method, data) {
            /* RESULT - Calls WEB API. This is common function to hit API from anywhere.
    
             url - URL of the API to hit
             method - method type of request (GET/POST/PUT/DELETE)
             data - data to be posted with the request
             */


            method = method || 'GET';
            data = data || '';

            var deferred = $q.defer();
            var req = {
                method: method, url: url, data: data
            };

            $rootScope.promise = $http(req).success(function (response) {

                deferred.resolve(response);
            }).error(function (response, statusCode) {
                switch (statusCode) {
                    case 404:
                        response = {
                            items: []
                        };
                        deferred.resolve(response);
                        break;
                    default:
                        //if (response)
                        //    common.showAlert("Error", response.message || '');

                        deferred.reject(response);
                        break;
                }
            });
            return deferred.promise;
        }

        common.showToast = function (type, title, txt) {
            toaster.pop({
                type: type,
                title: title,
                body: txt,
                showCloseButton: true
            });
        };

        common.toasterSuccess = function (title, txt) {
            common.showToast('success', title, txt);
        };
        common.toasterError = function (title, txt) {
            common.showToast('error', title, txt);
        };
        common.toasterInfo = function (title, txt) {
            common.showToast('info', title, txt);
        };
        common.toasterWarning = function (title, txt) {
            common.showToast('warning', title, txt);
        };

        common.initCleditor = function (id) {
            var HTMLEDITCONT = "bold italic underline strikethrough subscript superscript | font size style | color highlight removeformat | undo redo | link unlink | cut copy paste pastetext | print source";
            try {
                $("#" + id).cleditor({
                    width: '100%', height: 300, useCSS: true
                });
                $(window).resize();
            }
            catch (e) {
            }
        }

        return common;
    }
})();




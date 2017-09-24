(function () {
    'use strict';
    angular.module('app')
        .directive('topicList', function (Url, $state) {
            return {
                restrict: 'E',
                scope: {
                    topics: '=',
                    selectedTopic: '=',
                    type: '@'
                },
                link: function (scope, element, attrs) {
                    scope.BACK_END = Url.BACK_END;
                    scope.$state = $state;
                },
                templateUrl: "app/directives/topicList.html"
            };
        })
        .directive('contentInfo', function (Url) {
            return {
                restrict: 'E',
                scope: {
                    topic: '=',
                    currentpage: '@',
                },
                link: function (scope, element, attrs) {
                    scope.BACK_END = Url.BACK_END;
                },
                templateUrl: "app/directives/content-info.html"
            };
        })
        .directive('webToolBox', function (Util_Common) {
            return {
                restrict: 'E',
                scope: {
                    inputtext: '='
                },
                link: function (scope, element, attrs) {
                    scope.copyToClipboard = Util_Common.copyToClipboard;
                    scope.openInNewWindow = Util_Common.openInNewWindow;
                    scope.printText = Util_Common.printText;
                },
                templateUrl: "app/directives/wt-toolbox.html"
            };
        })
      .directive('dirPagination', function ($state) {
          return {
              restrict: 'E',
              scope: {
                  total: '@',
                  perpage: '=',
                  currentpage: '@',
                  topicurl: '@',
                  pagetype: '@'
              },
              link: function (scope, element, attrs) {
                  scope.$state = $state;

                  attrs.$observe('total', function (val) {
                      scope.currentpage = parseInt(scope.currentpage);
                      scope.url = scope.topicurl;
                      scope.pgs = parseInt(scope.total / scope.perpage) + 1;
                      scope.nextpage = scope.currentpage < scope.pgs ? scope.currentpage + 1 : scope.currentpage;
                  });
              },
              templateUrl: "/app/directives/dir_Pagination.html"
          };
      })
      .directive('postRepeatDirective', function () {
          return {
              scope: {
                  islast: '=',
                  postaction: '&'
              },
              link: function (scope, element, attrs) {

                  if (scope.islast) {
                      if (scope.postaction && typeof scope.postaction === "function") {
                          var timer = window.setTimeout(function () {
                              scope.postaction();
                              window.clearTimeout(timer);

                          }, 500);
                      }
                  }
              }
          };
      })
      .directive('fileModel', ['$parse', function ($parse) {
          return {
              restrict: 'A',
              scope:
                  {
                      callback: "=",
                      postedFile: "="
                  },
              link: function (scope, element, attrs) {
                  var model = $parse(attrs.fileModel);
                  var modelSetter = model.assign;
                  element.bind('change', function () {
                      scope.$apply(function () {
                          modelSetter(scope, element[0].files[0]);
                          scope.postedFile = element[0].files[0];
                          if (scope.callback !== undefined)
                              scope.callback(element[0].files[0]);
                      });
                  });
              }
          };
      }])
      .service('fileUpload', ['$http', function ($http) {
          this.uploadFileToUrl = function (file, uploadUrl, callback) {
              var fd = new FormData();
              if (file instanceof Array) {
                  for (var i = 0; i < file.length; i++)
                      fd.append(file[i].actualName, file[i].file);
              }
              else
                  fd.append('file', file);

              $http.post(uploadUrl, fd,
                  {
                      transformRequest: angular.identity,
                      headers: { 'Content-Type': undefined }
                  })
              .success(function (resp) {
                  callback(resp);
              })
              .error(function (err) {
                  callback(false, err);
              });
          }
      }])
})();
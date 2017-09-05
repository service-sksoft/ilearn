(function () {
    'use strict';
    angular
        .module('app')
        .controller('EditController', EditController)

    EditController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', '$sce', 'Url', 'fileUpload'];
    function EditController($scope, $state, Util_Common, commonService, $sce, Url, fileUpload) {
        var vm = this;

        Util_Common.bindCommonFunctions(vm);

        vm.topicList = [];

        initRequest();

        function initRequest() {
            getTopics();
        }

        function getTopics() {
            commonService.getTopics().then(function (data) {
                vm.topicList = data.result;
                vm.inputTopic = vm.topicList[0].ID;
            }).finally(function () {
                callback(lst);
            });
        }

        vm.saveTopic = function () {
            var inputDTO = {
                ID: vm.topic.ID || 0,
                Name: vm.topic.Name,
                DisplayName: vm.topic.DisplayName,
                Description: vm.topic.Description
            };

            commonService.saveTopic(inputDTO).then(function (data) {
                if (data.result) {
                    Util_Common.toasterSuccess("Topic Save", "Topic added/updated successfully.");

                    if (vm.topic.image) {
                        uploadImage(vm.topic.image, 'topic', vm.topic.ID, function () {
                            $("#myModal").modal('toggle');
                        });
                    }
                    else {
                        $("#myModal").modal('toggle');
                    }
                }
                else {
                    Util_Common.toasterError("Topic Save Failed", "Topic add/update failed.");
                }
            });
        }

        vm.editTopic = function (topic) {
            if (topic) {
                vm.topic = topic;
                vm.topic.Description = (vm.topic.Description || '').trim();
            }
        }
        
        function uploadImage(file, type, id, cb) {
            var uploadUrl = Url.API_BASE + Url.UPLOAD_FILE + "/uploadfile/" + id + "/?Type=" + type;
            fileUpload.uploadFileToUrl(file, uploadUrl, function (data) {
                if (data.result)
                    Util_Common.toasterSuccess("Topic Image Upload", "Topic image uploaded successfully.");
                else
                    Util_Common.toasterError("Topic Image Upload Failed", "Topic image upload failed.");

                if (cb !== undefined)
                    cb();
            });
        }
    }
})();

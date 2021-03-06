﻿(function () {
    'use strict';
    angular
        .module('app')
        .controller('EditTutorialController', EditTutorialController)

    EditTutorialController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', '$sce', 'Url', 'fileUpload'];
    function EditTutorialController($scope, $state, Util_Common, commonService, $sce, Url, fileUpload) {
        var vm = this;

        Util_Common.bindCommonFunctions(vm);

        vm.tutorialTitleList = [];

        vm.tutorialContentTypes = [
            { ID: 1, Name: 'Content' },
            { ID: 2, Name: 'Code' },
        ];

        initRequest();

        function initRequest() {
            vm.topicId = $state.params.topicId || -1;
            getTutorialTitles();
        }

        function getTutorialTitles() {
            commonService.getAllTutorialTitles(vm.topicId).then(function (data) {
                vm.tutorialTitleList = data.result;
            }, function () {
                Util_Common.toasterError("Error Loading Tutorial Titles", "Some error occured while loading tutorial titles.");
            });
        }

        vm.editTutorialTitle = function (ID) {
            if (ID === 0) {
                vm.tt = { TopicID: vm.topicId, ID: 0, Seq: vm.tutorialTitleList.length + 1 };
                return;
            }
            vm.tt = angular.copy(_.filter(vm.tutorialTitleList, function (o) { return o.ID === ID; })[0]);
        }

        vm.saveTutorialTitle = function () {
            var inputDTO = {
                ID: vm.tt.ID,
                TopicID: vm.topicId,
                Title: vm.tt.Title,
                Seq: vm.tt.Seq
            };

            commonService.saveTutorialTitle(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("Tutorial Title Saved", "Tutorial title added/updated successfully.");
                $('#titleModal').modal('toggle');
                getTutorialTitles();
            }, function (err) {
                Util_Common.toasterError("Tutorial Title Not Saved", "Tutorial title not added/updated.");
            });
        }

        vm.deleteTutorialTitle = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete tutorial title?");
            if (resp) {
                commonService.deleteTutorialTitle(ID).then(function (data) {
                    if (data.result) {
                        Util_Common.toasterSuccess("Tutorial Title Deleted", "Tutorial title deleted successfully.");
                        getTutorialTitles();
                    }
                    else
                        Util_Common.toasterError("Tutorial Title Not Deleted", "Tutorial title not deleted.");
                }, function () {
                    Util_Common.toasterError("Tutorial Title Not Deleted", "Tutorial title not deleted.");
                });
            }
        }

        //Tutorial Subtitle Starts
        vm.loadTutorialSubtitles = function (selectedTitleID) {
            vm.selectedSubtitle = null;
            vm.selectedTitle = _.filter(vm.tutorialTitleList, function (o) { return o.ID === selectedTitleID; })[0];
            getTutorialSubtitles(selectedTitleID);
        }

        function getTutorialSubtitles(titleID) {
            commonService.getTutorialSubtitles(titleID).then(function (data) {
                vm.tutorialSubtitleList = data.result;
            }, function () {
                Util_Common.toasterError("Error Loading Tutorial Subtitles", "Some error occured while loading tutorial subtitles.");
            });
        }

        vm.editTutorialSubtitle = function (ID) {
            if (!vm.selectedTitle) {
                Util_Common.toasterWarning("No Title Selected", "Please select title first.");
                return;
            }

            if (ID === 0) {
                vm.tst = { ParentID: vm.selectedTitle.ID, ID: 0, Seq: vm.tutorialSubtitleList.length + 1 };
                return;
            }
            vm.tst = angular.copy(_.filter(vm.tutorialSubtitleList, function (o) { return o.ID === ID; })[0]);
        }

        vm.saveTutorialSubtitle = function () {
            var inputDTO = {
                ID: vm.tst.ID,
                ParentID: vm.selectedTitle.ID,
                Title: vm.tst.Title,
                Seq: vm.tst.Seq
            };

            commonService.saveTutorialSubtitle(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("Tutorial Subtitle Saved", "Tutorial subtitle added/updated successfully.");
                $('#subtitleModal').modal('toggle');
                getTutorialSubtitles(vm.selectedTitle.ID);
            }, function (err) {
                Util_Common.toasterError("Tutorial Subtitle Not Saved", "Tutorial subtitle not added/updated.");
            });
        }

        vm.deleteTutorialSubtitle = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete tutorial subtitle?");
            if (resp) {
                commonService.deleteTutorialSubtitle(ID).then(function (data) {
                    if (data.result) {
                        Util_Common.toasterSuccess("Tutorial Subtitle Deleted", "Tutorial subtitle deleted successfully.");
                        getTutorialSubtitles(vm.selectedTitle.ID);
                    }
                    else
                        Util_Common.toasterError("Tutorial Subtitle Not Deleted", "Tutorial subtitle not deleted.");
                }, function () {
                    Util_Common.toasterError("Tutorial Subtitle Not Deleted", "Tutorial subtitle not deleted.");
                });
            }
        }

        //Tutorial Subtitle Ends

        //Tutorial Content Starts
        vm.loadTutorialContent = function (selectedSubTitleID) {
            vm.selectedSubtitle = angular.copy(_.filter(vm.tutorialSubtitleList, function (o) { return o.ID === selectedSubTitleID; })[0]);
            getTutorialContents(selectedSubTitleID);
        }

        function getTutorialContents(titleID) {
            commonService.getTutorialContents(titleID).then(function (data) {
                vm.tutorialContentList = data.result;
            }, function () {
                Util_Common.toasterError("Error Loading Tutorial Contents", "Some error occured while loading tutorial contents.");
            });
        }

        vm.editTutorialContent = function (ID) {
            if (!vm.selectedSubtitle) {
                Util_Common.toasterWarning("No Subtitle Selected", "Please select subtitle first.");
                return;
            }

            if (ID === 0) {
                $("#txtTutorialDescription").val('');
                vm.tc = { ParentID: vm.selectedSubtitle.ID, ID: 0, Seq: vm.tutorialContentList.length + 1, Type: 1 };
            }
            else {
                vm.tc = angular.copy(_.filter(vm.tutorialContentList, function (o) { return o.ID === ID; })[0]);
            }
            window.setTimeout(function () { Util_Common.initCleditor('txtTutorialDescription'); }, 200)
        }

        vm.saveTutorialContent = function () {
            var inputDTO = {
                ID: vm.tc.ID,
                ParentID: vm.selectedSubtitle.ID,
                Title: vm.tc.Title,
                Description: vm.tc.Type === 1 ? $("#txtTutorialDescription").val() : $("#txtTutorialCode").val(),
                Type: vm.tc.Type,
                Seq: vm.tc.Seq
            };

            commonService.saveTutorialContent(inputDTO).then(function (data) {
                Util_Common.toasterSuccess("Tutorial Content Saved", "Tutorial content added/updated successfully.");
                $('#contentModal').modal('toggle');
                getTutorialContents(vm.selectedSubtitle.ID);
            }, function (err) {
                Util_Common.toasterError("Tutorial Content Not Saved", "Tutorial content not added/updated.");
            });
        }

        vm.deleteTutorialContent = function (ID) {
            var resp = window.confirm("Are you sure, you want to delete tutorial content?");
            if (resp) {
                commonService.deleteTutorialContent(ID).then(function (data) {
                    if (data.result) {
                        Util_Common.toasterSuccess("Tutorial Content Deleted", "Tutorial content deleted successfully.");
                        getTutorialSubtitles(vm.selectedSubtitle.ID);
                    }
                    else
                        Util_Common.toasterError("Tutorial Content Not Deleted", "Tutorial content not deleted.");
                }, function () {
                    Util_Common.toasterError("Tutorial Content Not Deleted", "Tutorial content not deleted.");
                });
            }
        }

        //Tutorial Subtitle Ends

        vm.handleContentTypeChange = function () {
            if (vm.tc.Type === 1) {
                window.setTimeout(function () { Util_Common.initCleditor('txtTutorialDescription'); }, 200)
            }
        };

    }
})();

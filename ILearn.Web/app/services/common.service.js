(function () {
    'use strict';

    angular
        .module('app')
        .factory('commonService', commonService);

    commonService.$inject = ['$http', '$rootScope', '$q', 'Url', 'Util_Common'];
    function commonService($http, $rootScope, $q, Url, Util_Common) {

        var common =
            {
                reloadData: function () {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TOPIC + '/reload');
                },
                getItems: function () {
                    return Util_Common.callWebApi(Url.API_BASE + Url.ITEMS);
                },
                getTopics: function () {
                    return Util_Common.callWebApi(Url.API_BASE + '/' + Url.TOPIC + '/topics');
                },
                getQuestionDetail: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + "/" + Url.QUESTION_DETAIL + '/' + ID);
                },
                getAllQuestionsOfTopic: function (topicId, pageNo) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + "/questions/" + topicId + '/' + pageNo);
                },
                getMultipleChoiceQuestions: function (topicId, pageNo) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE + "/questions/" + topicId + '/' + pageNo);
                },
                getAllExternalResources: function (topicId, pageNo) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.EXTERNAL_RESOURCE + "/questions/" + topicId + '/' + pageNo);
                },
                getAllTutorialTitle: function (topicId, pageNo) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_TITLE + "/questions/" + topicId);
                },
                getTutorialContent: function (titleID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_CONTENT + "/tutorialcontent/" + titleID);
                },
                getAllQuestions: function (topicId) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + "/" + Url.ONLY_QUESTION + '/' + topicId);
                },
                saveQuestion: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + "/" + Url.QUESTION_DETAIL, "POST", inputDTO);
                },
                deleteQuestion: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + "/" + Url.DELETE_QUESTION + '/' + ID, "POST");
                },
                saveTopic: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.SAVE_TOPIC, "POST", inputDTO);
                },
                getExternalResources: function (topicID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.EXTERNAL_RESOURCE + '/' + topicID);
                },
                saveExternalResource: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.EXTERNAL_RESOURCE, "POST", inputDTO);
                },
                deleteExternalResource: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.EXTERNAL_RESOURCE + '/delete/' + ID, "POST");
                },
                getAllMultipleChoice: function (topicID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE + '/' + topicID);
                },
                getOnlyMultipleChoiceQuestions: function (topicID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE + '/' + Url.MULTIPLE_CHOICE_QUESTION_ONLY + '/' + topicID);
                },
                getMultipleChoiceQuestionDetail: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE + '/' + Url.MULTIPLE_CHOICE_DETAIL + '/' + ID);
                },
                saveMultipleChoice: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE, "POST", inputDTO);
                },
                deleteMultipleChoice: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.MULTIPLE_CHOICE + '/delete/' + ID, "POST");
                },
                getAllTutorialTitles: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_TITLE + '/gettutorialtitle/' + ID);
                },
                saveTutorialTitle: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_TITLE + '/savetutorialtitle', "POST", inputDTO);
                },
                deleteTutorialTitle: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_TITLE + '/deletetitle/' + ID, "POST");
                },
                getTutorialSubtitles: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_SUBTITLE + '/gettutorialsubtitle/' + ID);
                },
                saveTutorialSubtitle: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_SUBTITLE + '/savetutorialsubtitle', "POST", inputDTO);
                },
                deleteTutorialSubtitle: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_SUBTITLE + '/deletesubtitle/' + ID, "POST");
                },
                getTutorialContents: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_CONTENT + '/gettutorialcontent/' + ID);
                },
                saveTutorialContent: function (inputDTO) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_CONTENT + '/savetutorialcontent', "POST", inputDTO);
                },
                deleteTutorialContent: function (ID) {
                    return Util_Common.callWebApi(Url.API_BASE + Url.TUTORIAL_CONTENT + '/deletecontent/' + ID, "POST");
                },
                getQuestionsForCarousal: function () {
                    return Util_Common.callWebApi(Url.API_BASE + Url.QUESTION + '/questionsforcarousal');
                }


            };

        return common;









    }
})();
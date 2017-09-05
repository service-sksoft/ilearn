(function () {
    'use strict';

    angular
        .module('app')
        .constant('Url', {
            BACK_END: 'http://localhost/ILearn.WebApi/',
            //API_BASE: '/learningzone/query.ashx/',
            API_BASE: 'http://localhost/ILearn.WebApi/api/',
            BASIC_INFO: 'topics',
            //BASIC_INFO: '?action=basicinfo',
            HOME_CONTENT: "topics",
            GET_QUESTIONS: "?action=GetQuestions",
            GET_ALL_QUESTIONS: "?action=GetAllQuestions",
            SAVE_QUESTION: "question/SaveQuestion",
            SAVE_TOPIC: "topic/SaveTopic",
            UPLOAD_FILE: "fileupload",
            QUESTION: 'QuesList',
            ONLY_QUESTION: 'onlyquestions',
            QUESTION_DETAIL: 'question',
            DELETE_QUESTION: 'deletequestion',
            EXTERNAL_RESOURCE: 'externalresource',
            MULTIPLE_CHOICE: 'multiplechoice',
            MULTIPLE_CHOICE_DETAIL: 'questiondetail',
            MULTIPLE_CHOICE_QUESTION_ONLY: 'questionsonly',
            TUTORIAL_TITLE: 'tutorialtitle',
            TUTORIAL_SUBTITLE: 'tutorialsubtitle',
            TUTORIAL_CONTENT: 'tutorialcontent',
            TOPIC: 'topic'
        });
})();
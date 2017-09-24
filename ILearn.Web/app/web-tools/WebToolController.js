(function () {
    'use strict';
    angular
        .module('app')
        .controller('WebToolController', WebToolController);

    WebToolController.$inject = ['$scope', '$state', 'Util_Common', 'commonService', 'cacheFactory'];
    function WebToolController($scope, $state, Util_Common, commonService, cacheFactory) {
        var vm = this;
        vm.windowWidth = $(window).width();

        $(window).resize(function () {
            $scope.$apply(function () { vm.windowWidth = $(window).width(); });
        });

        vm.webToolList = [
            { label: 'Byte Conversion', type: 'byte-conversion', page: 'app/web-tools/pages/byte-conversion.html', desc: 'This tool will help understand bytes, kilobytes and several other unit of digital storage.' },
            { label: 'String to Base64', type: 'string-to-base64', page: 'app/web-tools/pages/string-to-base.html', desc: 'This tool encodes string content to its base64 value' },
            { label: 'Base64 to String', type: 'base64-to-string', page: 'app/web-tools/pages/base-to-string.html', desc: 'This tool decodes base64 content to its string value' },
            { label: 'Word Count', type: 'word-count', page: 'app/web-tools/pages/word-count.html', desc: 'Tool to count number of words, characters.' },
            { label: 'Transform Text', type: 'transform-text', page: 'app/web-tools/pages/transform-text.html', desc: 'Tool can transform text to UPPERCASE, lowercase or Titlecase.' },
            { label: 'Convert file to Byte Array', type: 'file-to-byte-array', page: 'app/web-tools/pages/file-to-byte-array.html', desc: 'Convert file to byte array' }
        ];



        Util_Common.bindCommonFunctions(vm, initRequest);

        function initRequest() {
            vm.selectedToolType = $state.params.type;
            if (!Util_Common.isNullEmptyOrWhiteSpace(vm.selectedToolType)) {
                vm.selectedTool = _.filter(vm.webToolList, function (e) { return e.type == vm.selectedToolType })[0];
            }
        }

        vm.reCalculateBytes = function (unit) {

            if (unit != 'byte') {
                switch (unit) {
                    case 'kb': vm.bytes = vm.kiloBytes * 1024; break;
                    case 'mb': vm.bytes = vm.megaBytes * 1024 * 1024; break;
                    case 'gb': vm.bytes = vm.gigaBytes * 1024 * 1024 * 1024; break;
                    case 'tb': vm.bytes = vm.teraBytes * 1024 * 1024 * 1024 * 1024; break;
                    case 'pb': vm.bytes = vm.petaBytes * 1024 * 1024 * 1024 * 1024 * 1024; break;
                }
            }
            vm.kiloBytes = vm.bytes / 1024;
            vm.megaBytes = vm.bytes / 1024 / 1024;
            vm.gigaBytes = vm.bytes / 1024 / 1024 / 1024;
            vm.teraBytes = vm.bytes / 1024 / 1024 / 1024 / 1024;
            vm.petaBytes = vm.bytes / 1024 / 1024 / 1024 / 1024 / 1024;
        };

        vm.countWords = function () {
            var txt = (vm.wordCountBox || '');
            if (Util_Common.isNullEmptyOrWhiteSpace(txt)) {
                vm.charCount = 0;
                vm.wordCount = 0;
                vm.paraCount = 0;
                return;
            }

            vm.charCount = txt.length;
            vm.wordCount = txt.split(' ').length;
            vm.paraCount = parseInt(txt.split('\n').length / 2) + 1;
        };

        vm.stringToBase = function (txt) {
            txt = txt || '';
            return btoa(txt)
        };

        vm.baseToString = function (txt) {
            txt = txt || '';
            var val = '';

            try { val = atob(txt); }
            catch (e) { val = 'The string to be decoded is not correctly encoded.' }

            return val;
        };

        vm.fileToByteArray = function (file) {
            vm.errorMessage = '';

            if (!file) {
                vm.errorMessage = 'Please select file!!!';
                return;
            }
            try {
                var fileReader = new FileReader();
                if (fileReader && file) {
                    vm.loadingContent = true;
                    fileReader.readAsArrayBuffer(file);
                    fileReader.onload = function (e) {
                        var arrayBuffer = e.target.result
                        $scope.$apply(function () {
                            vm.loadingContent = false;
                            vm.byteArrayOfFile = new Uint8Array(arrayBuffer);
                        });
                    };
                }
            }
            catch (e) {
                vm.loadingContent = false;
            }
        };

        vm.transformText = function (type) {
            var txt = vm.inputText || '';
            switch (type) {
                case "l": vm.transformedText = txt.toLowerCase(); break;
                case "u": vm.transformedText = txt.toUpperCase(); break;
                case "t": vm.transformedText = txt.toProperCase(); break;
            }
        };
    }
})();

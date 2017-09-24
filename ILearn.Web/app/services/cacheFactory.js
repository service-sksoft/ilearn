(function () {
    "use strict";
    angular.module('app').factory("cacheFactory", function () {
        var cacheStorage =
            {
                cacheEntity: {},
                storageLocation: 'localStorage',
                get: function (key, defaultValue) {
                    if (Object.keys(this.cacheEntity).length === 0) {
                        this.load();
                    }

                    var val = this.cacheEntity[key];
                    if (val) {
                        //remove cache if 5 days old
                        if ((new Date().getDate() - new Date(val.date).getDate()) < 5)
                            return val.val;
                    }
                    return defaultValue || null;
                },
                set: function (key, value) {
                    this.cacheEntity[key] = { date: new Date(), val: value };
                    this.save();
                },
                append: function (key, value) {
                    var currentValue = this.cacheEntity[key] || [];
                    angular.forEach(value, function (val) {
                        if (!cacheStorage.containsObject(val, currentValue)) {
                            currentValue.push(val);
                        }
                    });

                    this.set(key, currentValue);
                },
                save: function () {
                    if (window[this.storageLocation]) {
                        window[this.storageLocation].setItem('ilearnCache', JSON.stringify(this.cacheEntity));
                        this.load();
                    }
                },
                load: function () {
                    if (window.sessionStorage) {
                        this.cacheEntity = JSON.parse(window[this.storageLocation].getItem('ilearnCache')) || {};
                    }
                },
                containsObject: function (obj, list) {
                    return list.filter(function (listItem) {
                        return angular.equals(listItem, obj)
                    }).length > 0;
                },
                clear: function () {
                    if (window.sessionStorage) {
                        window[this.storageLocation].removeItem('ilearnCache');
                    }
                },
            };
        return cacheStorage;
    });
}());
var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.service('AlbumService', function ($http) {

    this.retrieveCompleteAlbumList = function () {
        return $http({
            method: 'GET',
            url: '/Album/GetAlbumList'
        })
    };
});
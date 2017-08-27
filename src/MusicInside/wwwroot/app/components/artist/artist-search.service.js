var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.service('ArtistService', function ($http) {

    this.retrieveCompleteArtistList = function () {
        return $http({
            method: 'GET',
            url: '/Artist/GetArtistList'
        })
    };
});
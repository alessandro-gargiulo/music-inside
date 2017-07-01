var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.service('SongService', function ($http) {

    this.retrieveSongList = function (letter) {
        return $http({
            method: 'GET',
            url: '/Song/GetSongListByLetter',
            params: { firstLetter: letter } 
        })
    };

});
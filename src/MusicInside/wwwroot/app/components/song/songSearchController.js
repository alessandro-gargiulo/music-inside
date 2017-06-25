var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('SongSearchController', ['$scope', function ($scope) {

    // Initializing function, it take the ASP MVC model and initialize angular variable
    $scope.init = function (songModelList) {
        $scope.songList = songModelList;
        for (var i = 0; i < $scope.songList.length; i++) {
            $scope.songList[i].songDetailLink = '/Song/Detail/' + $scope.songList[i].SongId;
            $scope.songList[i].artistDetailLink = '/Album/Detail/' + $scope.songList[i].ArtistId;
            $scope.songList[i].albumDetailLink = '/Artist/Detail/' + $scope.songList[i].AlbumId; 
        }
    };

}]);
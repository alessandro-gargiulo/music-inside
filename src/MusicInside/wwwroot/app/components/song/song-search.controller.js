var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('SongSearchController', ['$scope', 'SongService', function ($scope, $songService) {

    /*
     * ANGULAR JS FUNCTIONS
     */

    // Routine called when user click on letter on the top of the page
    $scope.clickOnLetter = function (letter) {
        // Clean all input filters
        $scope.userInputFilterTitle = '';
        // Retrieve songs
        $songService.retrieveSongList(letter).then(function (response) {
            // Success callback
            if (response.data !== -1) {
                $scope.songList = createDetailLinks(response.data);
            } else {
                // Something goes wrong on serve side
            }
        }, function (error) {
            // Error callback
        });
    };

    /*
     * INITIALIZATION CODE
     */
    $scope.clickOnLetter("A");

    /*
     * OTHER JS FUNCTIONS
     */

    // Take a list and create the fields for linking details based on ids
    var createDetailLinks = function (songList) {
        for (var i = 0; i < songList.length; i++) {
            songList[i].songDetailLink = '/Song/Detail/' + songList[i].songId;
            songList[i].albumDetailLink = '/Album/Detail/' + songList[i].albumId;
            songList[i].artistDetailLink = '/Artist/Detail/' + songList[i].artistId;
        }
        return songList;
    }

    // On DOM ready function
    $(function () {
        // Call isotope
        $('.music-grid').isotope({
            // options
            itemSelector: '.music-grid-item',
            layoutMode: 'fitRows'
        });
    });
}]);
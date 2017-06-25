var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('SongSearchController', ['$scope', 'SongService', function ($scope, $songService) {

    /*
     * ANGULAR JS FUNCTIONS
     */

    // Routine called when user click on letter on the top of the page
    $scope.clickOnLetter = function (letter) {
        // Clean all input filters
        $scope.userInputFilterTitle = '';
        $scope.userInputFilterArtist = '';
        $scope.userInputFilterAlbum = '';
        $scope.userInputFilterGenre = '';
        $scope.userInputFilterYear = '';
        // Retrieve songs
        $songService.retrieveSongList(letter).then(function (response) {
            // Success callback
            $scope.songList = createDetailLinks(response.data);
        }, function (error) {
            // Error callback
        });
    };

    /*
     * OTHER JS FUNCTIONS
     */

    // Take a list and create the fields for linking details based on ids
    var createDetailLinks = function (songList) {
        for (var i = 0; i < songList.length; i++) {
            songList[i].songDetailLink = '/Song/Detail/' + songList[i].songId;
            songList[i].artistDetailLink = '/Album/Detail/' + songList[i].albumId;
            songList[i].albumDetailLink = '/Artist/Detail/' + songList[i].artistId;
        }
        return songList;
    }

    // Change the selector if needed
    var $table = $('table.table-scroll'),
        $bodyCells = $table.find('tbody tr:first').children(),
        colWidth;

    // Adjust the width of thead cells when window resizes
    $(window).resize(function () {
        // Get the tbody columns width array
        colWidth = $bodyCells.map(function () {
            return $(this).width();
        }).get();

        // Set the width of thead columns
        $table.find('thead tr').children().each(function (i, v) {
            $(v).width(colWidth[i]);
        });
    }).resize(); // Trigger resize handler

}]);
var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('AlbumSearchController', ['$scope', 'AlbumService', function ($scope, $albumService) {

    /*
     * ANGULAR JS FUNCTIONS
     */


    /*
     * INITIALIZATION CODE
     */
    // Retrieve artists
    $albumService.retrieveCompleteAlbumList().then(function (response) {
        // Success callback
        if (response.data !== -1) {
            $scope.albumList = createDetailLinks(response.data);
        } else {
            // Something goes wrong on server side
        }
    }, function (error) {
        // Error callback
    });

    /*
     * OTHER JS FUNCTIONS
     */
    // Take a list and create the fields for linking details based on ids
    var createDetailLinks = function (albumList) {
        for (var i = 0; i < albumList.length; i++) {
            albumList[i].albumDetailLink = '/Album/Detail/' + albumList[i].albumId;
        }
        return albumList;
    }

    // On DOM ready function
    $(function () {
        // Call isotope
        $('.artist-grid').isotope({
            // options
            itemSelector: '.artist-grid-item',
            layoutMode: 'fitRows'
        });
    });
}]);
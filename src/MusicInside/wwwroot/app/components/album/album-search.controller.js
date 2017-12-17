var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('AlbumSearchController', ['$scope', 'AlbumService', function ($scope, albumService) {

    /*
     * ANGULAR JS FUNCTIONS
     */


    /*
     * INITIALIZATION CODE
     */
    // Retrieve artists
    $artistService.retrieveCompleteArtistList().then(function (response) {
        // Success callback
        if (response.data !== -1) {
            $scope.artistList = createDetailLinks(response.data);
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
    var createDetailLinks = function (artistList) {
        for (var i = 0; i < artistList.length; i++) {
            artistList[i].artistDetailLink = '/Album/Detail/' + artistList[i].artistId;
        }
        return artistList;
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
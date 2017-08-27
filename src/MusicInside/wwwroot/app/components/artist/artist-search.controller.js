var MusicInsideApp = angular.module('MusicInsideApp');

MusicInsideApp.controller('ArtistSearchController', ['$scope', 'ArtistService', function ($scope, $artistService) {

    /*
     * ANGULAR JS FUNCTIONS
     */


    /*
     * OTHER JS FUNCTIONS
     */

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
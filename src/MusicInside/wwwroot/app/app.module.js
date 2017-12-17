var MusicInsideApp = angular.module('MusicInsideApp', []);

MusicInsideApp.controller('AppController', ['$scope', function ($scope) {

    /*
     * ANGULAR JS FUNCTIONS
     */


    /*
     * INITIALIZATION CODE
     */


    /*
     * OTHER JS FUNCTIONS
     */

}]);

// Sidebar Link Selection
$(function () {
    var links = $('.sidebar-links > a');
    links.on('click', function () {
        links.removeClass('selected');
        $(this).addClass('selected');
    });
    initAudioBar();
});


// Initialize Audio Bar
function initAudioBar() {
    var current = 0;
    var audio = $('#audio');
    var playlist = $('#list');
    var tracks = playlist.find('li a');
    var len = tracks.length - 1;
    //audio[0].volume = .10;
    //audio[0].play();
    playlist.find('a').click(function (e) {
        e.preventDefault();
        link = $(this);
        current = link.parent().index();
        run(link, audio[0]);
    });
    audio[0].addEventListener('ended', function (e) {
        current++;
        if (current === len) {
            current = 0;
            link = playlist.find('a')[0];
        } else {
            link = playlist.find('a')[current];
        }
        run($(link), audio[0]);
    });
}

// Service function for Audio Bar
function run(link, player) {
    player.src = link.attr('href');
    par = link.parent();
    par.addClass('active').siblings().removeClass('active');
    player.load();
    player.play();
}

var status = 0; // 0 = Expand, 1 = Compress
// Handler for display/hide playlist
function displayHidePlaylist() {
    if (status === 0) {
        $('#playlist-opener').removeClass("fa-expand").addClass("fa-compress");
        status = 1;
    } else {
        $('#playlist-opener').removeClass("fa-compress").addClass("fa-expand");
        status = 0;
    }
}
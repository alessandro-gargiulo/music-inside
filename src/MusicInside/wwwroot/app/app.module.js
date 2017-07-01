var MusicInsideApp = angular.module('MusicInsideApp', []);

// Sidebar Link Selection
$(function () {
    var links = $('.sidebar-links > a');
    links.on('click', function () {
        links.removeClass('selected');
        $(this).addClass('selected');
    })
});
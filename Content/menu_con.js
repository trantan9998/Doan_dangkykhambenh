﻿$(function () {
    $('#navigation a').stop().animate({ 'marginLeft': '-45px' }, 1000);

    $('#navigation > li').hover(
        function () {
            $('a', $(this)).stop().animate({ 'marginLeft': '-2px' }, 200);
        },
        function () {
            $('a', $(this)).stop().animate({ 'marginLeft': '-55px' }, 200);
        }
    );
});
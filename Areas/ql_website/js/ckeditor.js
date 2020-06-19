var app = angular.module('editorApp', []);
app.controller('editorCtrl', function ($scope) {
    DecoupledEditor
        .create(document.querySelector('#editor'), {
            ckfinder: {
                uploadUrl: 'http://httpbin.org/post'
            }
        })
        .then(editor => {
            const toolbarContainer = document.querySelector('#toolbar-container');
            toolbarContainer.appendChild(editor.ui.view.toolbar.element);
        })
        .catch(error => {
            console.error(error);
        });
});
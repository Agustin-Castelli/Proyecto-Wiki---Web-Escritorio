window.initTinyMCE = function (selector) {
    if (!window.tinymce) {
        const script = document.createElement('script');
        script.src = 'https://cdn.tiny.cloud/1/2oo9d9lnv74nlp2diosz7gfrgmx39oet2jetjwsewobi7na3/tinymce/7/tinymce.min.js';
        script.referrerPolicy = 'origin';
        script.onload = () => {
            tinymce.init({
                selector: selector,
                plugins: 'image media link code preview fullscreen',
                toolbar: 'undo redo | styles | bold italic underline | alignleft aligncenter alignright | bullist numlist outdent indent | link image media | code preview fullscreen',
                menubar: 'file edit insert view format table tools help',
                height: 500,
                branding: false,
                setup: function (editor) {
                    editor.on('change', function () {
                        editor.save();
                    });
                },
                media_live_embeds: true
            });
        };
        document.head.appendChild(script);
    } else {
        tinymce.init({
            selector: selector,
            plugins: 'image media link code preview fullscreen',
            toolbar: 'undo redo | styles | bold italic underline | alignleft aligncenter alignright | bullist numlist outdent indent | link image media | code preview fullscreen',
            menubar: 'file edit insert view format table tools help',
            height: 500,
            branding: false,
            setup: function (editor) {
                editor.on('change', function () {
                    editor.save();
                });
            },
            media_live_embeds: true
        });
    }
};

window.saveTinyMCE = function (id) {
    const editor = tinymce.get(id);
    if (editor) {
        editor.save();
    }
};

window.getTinyMCEContent = function (id) {
    const editor = tinymce.get(id);
    return editor ? editor.getContent() : '';
};

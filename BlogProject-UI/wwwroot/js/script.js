(function () {
    var scripts = [
        {
            src: 'https://cdnjs.cloudflare.com/ajax/libs/Trumbowyg/2.27.3/trumbowyg.min.js',
            integrity: 'sha512-YJgZG+6o3xSc0k5wv774GS+W1gx0vuSI/kr0E0UylL/Qg/noNspPtYwHPN9q6n59CTR/uhgXfjDXLTRI+uIryg=='
        },
        {
            src: 'https://cdnjs.cloudflare.com/ajax/libs/Trumbowyg/2.27.3/langs/tr.min.js',
            integrity: 'sha512-4Lri9C2eYjnoar37EumYfA8RgOZoMBOXTzkpSgkdvbRH7ts8OP/vsXnoouBAXdrU2x5NxUkdQGe4fBxSjvFiGA=='
        },
        {
            src: 'https://cdnjs.cloudflare.com/ajax/libs/Trumbowyg/2.27.3/plugins/colors/trumbowyg.colors.min.js',
            integrity: 'sha512-SHpxBJFbCaHlqGpH13FqtSA+QQkQfdgwtpmcWedAXFCDxAYMgrqj9wbVfwgp9+HgIT6TdozNh2UlyWaXRkiurw=='
        },
        {
            src: 'https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js',
            integrity: 'sha512-4MvcHwcbqXKUHB6Lx3Zb5CEAVoE9u84qN+ZSMM6s7z8IeJriExrV3ND5zRze9mxNlABJ6k864P/Vl8m0Sd3DtQ=='
        },
        {
            src: 'https://cdn.jsdelivr.net/npm/simple-datatables@latest',
            crossorigin: 'anonymous'
        }
    ];

    scripts.forEach(function (script) {
        var scriptElement = document.createElement('script');
        scriptElement.src = script.src;
        scriptElement.integrity = script.integrity || '';
        scriptElement.crossOrigin = 'anonymous';
        scriptElement.referrerPolicy = 'no-referrer';

        scriptElement.onload = function () {
            console.log('Script loaded:', script.src);
        };

        scriptElement.onerror = function () {
            console.error('Failed to load script:', script.src);
            alert('Y�kleme s�ras�nda bir hata olu�tu. L�tfen daha sonra tekrar deneyin.');
        };

        document.head.appendChild(scriptElement);
    });

    document.addEventListener('DOMContentLoaded', function () {
        // Trumbowyg initialization
        $('#text-editor').trumbowyg({
            lang: 'tr',
            btns: [
                ['viewHTML'],
                ['undo', 'redo'],
                ['formatting'],
                ['strong', 'em', 'del'],
                ['superscript', 'subscript'],
                ['link'],
                ['insertImage'],
                ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
                ['unorderedList', 'orderedList'],
                ['horizontalRule'],
                ['removeformat'],
                ['foreColor', 'backColor'],
                ['fontfamily'],
                ['fontsize'],
                ['fullscreen']
            ],
            plugins: {
                colors: {
                    foreColorList: ['ff0000', '00ff00', '0000ff', '161D6F', '0B2F9F'],
                    backColorList: ['000', '333', '555'],
                    displayAsList: false
                }
            }
        });

        // Select2 initialization
        $('#categoryList').select2({
            theme: 'bootstrap4'
        });

        // Initialize Simple Datatables
        const datatablesSimple = document.getElementById('userArticlesTable');
        if (datatablesSimple) {
            new simpleDatatables.DataTable(datatablesSimple);
        }
    });
})();

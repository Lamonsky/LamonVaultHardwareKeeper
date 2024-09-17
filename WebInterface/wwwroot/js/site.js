// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<<<<<<< HEAD
    tinymce.init({
        selector: 'textarea#tiny',
    plugins: [
    'a11ychecker','advlist','advcode','advtable','autolink','checklist','markdown',
    'lists','link','image','charmap','preview','anchor','searchreplace','visualblocks',
    'powerpaste','fullscreen','formatpainter','insertdatetime','media','table','help','wordcount'
    ],
    toolbar: 'undo redo | a11ycheck casechange blocks | bold italic backcolor | alignleft aligncenter alignright alignjustify |' +
    'bullist numlist checklist outdent indent | removeformat | code table help',
    language: 'pl',
      })

    function generatePDF(){
        var templateContent = tinymce.get('tiny').getContent();
        var printWindow = window.open('','','height=800,width=800');
        printWindow.document.write(templateContent);
        printWindow.document.close();
        printWindow.print();
    }
=======


tinymce.init({
    selector: 'textarea#tiny',
    plugins: [
        'a11ychecker', 'advlist', 'advcode', 'advtable', 'autolink', 'checklist', 'markdown',
        'lists', 'link', 'image', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',
        'powerpaste', 'fullscreen', 'formatpainter', 'insertdatetime', 'media', 'table', 'help', 'wordcount'
    ],
    toolbar: 'undo redo | a11ycheck casechange blocks | bold italic backcolor | alignleft aligncenter alignright alignjustify |' +
        'bullist numlist checklist outdent indent | removeformat | code table help',
    language: 'pl',
})


function generatePDF() {
    var templateContent = tinymce.get('tiny').getContent();
    var printWindow = window.open('', '', 'height=800,width=800');
    printWindow.document.write(templateContent);
    printWindow.document.close();
    printWindow.print();

}
>>>>>>> f4e4131 (DZIAŁA WYDRUK)

// Write your JavaScript code.

//Display a modal popup screen for scene image
    function getModalPopup() {
        // Initialize numeric spinner input boxes
        //$(".numeric-spinner").spinedit();
        // Initialize modal dialog
        // attach modal-container bootstrap attributes to links with .modal-link class.
        // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
        $('body').on('click', '.modal-link', function (e) {
            e.preventDefault();
            $(this).attr('data-target', '#modal-container');
            $(this).attr('data-toggle', 'modal');
        });
    // Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });
        //clear modal cache, so that new content can be loaded
        $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });
        $('#CancelModal').on('click', function () {
            return false;
        });
};

//Not used
function getImageOrientation() {
    $(document).ready(function () {
        $("img").each(function () {
            // Calculate aspect ratio and store it in HTML data- attribute
            var aspectRatio = $(this).width() / $(this).height();
            $(this).data("aspect-ratio", aspectRatio);

            // Conditional statement
            if (aspectRatio > 1) {
                // Image is landscape
                $(this).css({
                    width: "100%",
                    height: "auto"
                });
            } else if (aspectRatio < 1) {
                // Image is portrait
                $(this).css({
                    maxWidth: "100%"
                });
            } else {
                // Image is square
                $(this).css({
                    maxWidth: "100%",
                    height: "auto"
                });
            }
        });
    });
}// End getImageOrientation()


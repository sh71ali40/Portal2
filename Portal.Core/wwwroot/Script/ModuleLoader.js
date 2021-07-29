$(document).ready(function () {
    $('.module').each(function () {
        let moduleId = $(this).attr("data-moduleId");
        let homeController = $(this).attr("data-homeController");
        let url = `module-${moduleId}/${homeController}`;
        $(this).load(url, function (response, status, xhr) {
            
            $(this).html(response);
        });

    });
});
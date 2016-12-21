// Starting fct
$(document).ready(function () {
    var length = $("ul#CommentList li").length;
    $("ul#CommentList li").hide();

    if (length < 4) {
        $("#ShowMoreBtn").hide();
        $("ul#CommentList li").show();
    }
    else {
        $("#ShowMoreBtn").show();
        for (var i = 0; i < 3; i++) {
            $("ul#CommentList li:eq("+i+")").show();
        }
    }
});

// Fct pour envoyer les information générées
$("form").submit(function () {
    $("#mark").val($('#ReceipeRanking').val());
    $("#recetteId").val($("#recette_id").val());
});

// Fct pour afficher plus de commentaires
$("#ShowMoreBtn").click(function () {
    $("ul#CommentList li").show();
    $("#ShowMoreBtn").hide();
});
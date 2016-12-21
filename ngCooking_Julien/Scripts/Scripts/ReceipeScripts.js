// Starting fct
$(document).ready(function () {
    $("#ReceipeCalo").hide();
    $("#CreationSucess").hide();
    $("#CreationFailed").hide();
    $("#MyReceipe").hide();
});

// Affiche la liste des catégories
$("#CategorieList").change(function () {
    var sel = $(this).val();
    var ingInCat = JSON.parse($("#ingInCat").val());

    $("#recIngredients").empty();
    $.each(ingInCat, function (key, value) {
        if (key == sel) {
            if (value.length < 1) {
                $("#recIngredients").append('<option value="" id="empty" name="empty">Vide</option>');
            }
            else {
                $.each(value, function () {
                    $("#recIngredients").append("<option value=" + this.id + " name=" + this.name + ">" + this.name + "</option>");
                });
            }
        }
    });
});

//// Affiche la liste des catégories
$("#CategorieList").change(function () {
    var sel = $(this).val();
    var ingInCat = JSON.parse($("#ingInCat").val());

    $("#ReceipeIngr").empty();
    $.each(ingInCat, function (key, value) {
        if (key == sel) {
            if (value.length < 1) {
                $("#ReceipeIngr").append('<option value="" id="empty" name="empty">Vide</option>');
            }
            else {
                $.each(value, function () {
                    $("#ReceipeIngr").append("<option value=" + this.id + " name=" + this.name + ">" + this.name + "</option>");
                });
            }
        }
    });
});

//// Affiche la liste des ingrédients
$("#IngAddBtn").click(function () {
    var sel = $("#CategorieList").val();
    var ing = $("#ReceipeIngr").val();
    var ingInCat = JSON.parse($("#ingInCat").val());

    if (ing != "empty") {
        $.each(ingInCat, function (key, value) {
            if (key == sel) {
                $.each(value, function () {
                    if ($('#ingVisu').find("#" + this.id).length < 1) {
                        if (this.id == ing) {
                            var src = "../Content/img/ingredients/" + this.picture;
                            $("#ingVisu").append('  <li calories="' + this.calories + '" class="item" id="' + this.id + '">\
                                                        <img class="item-img" src="'+ src + '" alt="' + this.name + '">\
                                                        <p class="item-name" title="' + this.name + '">' + this.name + '</p>\
                                                        <button type="button" class="remove_ingredient" id="' + this.id + '" name="' + this.name + '">\
                                                            <span class="glyphicon glyphicon-remove"></span>\
                                                        </button>\
                                                    </li>');
                            var tCal = $("#totalCal").text().substring(0, $("#totalCal").text().length - 4); // 150kCal -> 150
                            $("#totalCal").text(parseInt(tCal) + parseInt(this.calories) + "kCal");
                            $("#ReceipeCalo").show();
                        }
                    }
                });
            }
        });
    }
});

// Fct pour enlever un ingrédient
$('#ingVisu').on("click", "li", function () {
    var tCal = $(this).attr("calories").substring(0, $("#totalCal").text().length - 4);
    var cal = parseInt($("#totalCal").text().substring(0, $("#totalCal").text().length - 4)) - parseInt(tCal);
    $("#totalCal").text(cal + "kCal");

    if (cal == 0) {
        $("#ReceipeCalo").hide();
    }
    $(this).remove();
});

// Fct pour envoyer les information générées (calories, ingrédients, image)
$("form").submit(function () {
    $("#recCalories").val(parseInt($("#totalCal").text().substring(0, $("#totalCal").text().length - 4)));

    var csv = "";
    $("#ingVisu li").each(function (index, value) {
        if (index == 0) {
            csv = this.id;
        }
        else {
            csv += ',' + this.id;
        }
    });
    $("#recIdIngredients").val(csv);
});

// Starting fct
$(document).ready(function () {
    $.ajax({
        url: "/Community/ComDisplay",
        type: 'GET',
        data: { from: 0, selectorValue: "az" },
        dataType: 'html',
        success: function (data) {
            $('#CommunityDisplay').html(data);
            $("#ShowLess_btn").hide();
        }
    });
});

// Show less button
$("#ShowLess_btn").click(function () {
    var fr = parseInt($("#fromIndex").val());
    var dis = parseInt($("#display").val());

    $.ajax({
        url: "Community/ComDisplay",
        type: 'GET',
        data: { from: fr - dis, selectorValue: $("#orderSelect").val() },
        dataType: 'html',
        success: function (data) {
            $('#CommunityDisplay').html(data);
            $("#ShowMore_btn").show();
            if ((fr - dis) <= 0) {
                $("#fromIndex").val(0);
                $("#ShowLess_btn").hide();
            }
            else {
                $("#fromIndex").val(fr - dis);
            }
        }
    });
});

// Show more button
$("#ShowMore_btn").click(function () {
    var fr = parseInt($("#fromIndex").val());
    var dis = parseInt($("#display").val());
    var dbS = parseInt($("#dbSize").val());

    $.ajax({
        url: "Community/ComDisplay",
        type: 'GET',
        data: { from: fr + dis, selectorValue: $("#orderSelect").val() },
        dataType: 'html',
        success: function (data) {
            $('#CommunityDisplay').html(data);
            $("#ShowLess_btn").show();
            if ((fr + dis + dis) > dbS) {
                $("#fromIndex").val(dbS - dis);
                $("#ShowMore_btn").hide();
            }
            else {
                $("#fromIndex").val(fr + dis);
            }
        }
    });
});

// Action appelé pour le changement de la liste + titre
$("#orderSelect").change(function () {
    var fr = parseInt($("#fromIndex").val());
    var sel = $(this).val();

    $.ajax({
        url: "Community/ComDisplay",
        type: 'GET',
        data: { from: 0, selectorValue: sel },
        dataType: 'html',
        success: function (data) {
            $('#CommunityDisplay').html(data);
            $("#ShowMore_btn").show();
            $("#ShowLess_btn").hide();
            switch (sel) {
                case "az":
                    $('#CommunitySearchTitle').html("Nos cuistots de A à Z");
                    break;
                case "za":
                    $('#CommunitySearchTitle').html("Nos cuistots de Z à A");
                    break;
                case "exp":
                    $('#CommunitySearchTitle').html("Nos cuistots les mieux notés");
                    break;
                case "nov":
                    $('#CommunitySearchTitle').html("Nos cuistots novices");
                    break;
                case "prod":
                    $('#CommunitySearchTitle').html("Nos cuistots présents au fourneaux");
                    break;
                case "prod_desc":
                    $('#CommunitySearchTitle').html("Nos cuistots en repos");
                    break;
                default:
                    $('#CommunitySearchTitle').html("Nos cuistots !");
                    break;
            }
        }
    });
});
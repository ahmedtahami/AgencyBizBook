
var stock = 0;

function showDefaultModal(clickevent) {
    var url = $('#expenseModal').attr("data-request-url");
    $.get(url, function (data) {
        $("#expenseModal").html(data);
        $("#expenseModal").modal('show');
    }).fail(function () { alert("Error!") });
}

function showStockInModal(clickevent) {
    var url = $('#myModal').attr("data-request-url");
    $.get(url, function (data) {
        $("#myModal").html(data);
        $("#myModal").modal('show');
    }).fail(function () { alert("Error!") });
}

function showStockOutModal(clickevent) {
    var url = $('#myModal1').attr("data-request-url");
    $.get(url, function (data) {
        $("#myModal1").html(data);
        $("#myModal1").modal('show');
    }).fail(function () { alert("Error!") });
}

function showDriverStockOutModal(clickevent) {
    var url = $('#myModal2').attr("data-request-url");
    $.get(url, function (data) {
        $("#myModal2").html(data);
        $("#myModal2").modal('show');
    }).fail(function () { alert("Error!") });
}

function showLiabilityStockInModal(clickevent) {
    var url = $('#myModal3').attr("data-request-url");
    $.get(url, function (data) {
        $("#myModal3").html(data);
        $("#myModal3").modal('show');
    }).fail(function () { alert("Error!") });
}

function showLiabilityStockOutModal(clickevent) {
    var url = $('#myModal4').attr("data-request-url");
    $.get(url, function (data) {
        $("#myModal4").html(data);
        $("#myModal4").modal('show');
    }).fail(function () { alert("Error!") });
}

function showCashOutModal(clickevent) {
    var url = $('#cashOutModal').attr("data-request-url");
    $.get(url, function (data) {
        $("#cashOutModal").html(data);
        $("#cashOutModal").modal('show');
    }).fail(function () { alert("Error!") });
}

function showCashInModal(clickevent) {
    var url = $('#cashInModal').attr("data-request-url");
    $.get(url, function (data) {
        $("#cashInModal").html(data);
        $("#cashInModal").modal('show');
    }).fail(function () { alert("Error!") });
}


function AddToCartValidator() {
    var qty = $('#Quantity').val();
    var productId = $('#ProductId').val();
    var rate = $('#Rate').val();

    if (productId == 0 || qty == 0 || rate == 0) {
        alert("Invalid Entry");
        return false;
    }

    if (qty > stock && window.location.href.includes("Sale")) {
        alert("Available Stock is : " + stock);
        $('#Quantity').focus();
    }

    return true;
}

if (window.location.href.includes("Sale")) {
    $('#ProductId').change(function () {
        var id = $("#ProductId").val();
        $.ajax({
            url: '/Sale/GetProductInfo/' + id,
            dataType: 'Json',
            type: 'GET',
            success: function (data) {
                stock = data.ProductStock;
                document.getElementById('AvailableStock').value = stock;
                if (stock == 0) {
                    alert("Stock Not Available");
                }
            },
            error: function (err) {
                document.getElementById('AvailableStock').value = 0;
            }
        });
    });

    $('#Quantity').blur(function () {
        var qty = $('#Quantity').val();

        window.location.href.includes("Sale")
        if (stock < qty && window.location.href.includes("Sale")) {
            alert("Available Stock is : " + stock);
            $('#Quantity').filter(":first").focus();
        }

    });
}


$(function () {

    $.ajaxSetup({ cache: false });

    $("#modalBtn").on("click", function (e) {
        $('#loginmodel').load(this.href, function () {
            $('#myModal').modal({
            }, 'show');
        });
        return false;
    });
});




$(document).on('click', 'button.btn-air-warning', function () {
    $(this).closest('tr').remove();
    reindex();
    return false;
});
$('#addRow').on('click', function () {
    var t = $('.tblProducts');
    var index = $('#index').val();
    var result = AddToCartValidator();

    if(result)
    {
        t.append("<tr> <td><input type='hidden' class='txtname' name='Products[" + $('#index').val() + "].ProductId' value='" + $('#ProductId').val() + "' />" + $('#ProductId option:selected').text() + "</td>" +
            " <td><input type='hidden' class='txtquantity' name='Products[" + $('#index').val() + "].Quantity' value='" + $('#Quantity').val() + "'/>" + $('#Quantity').val() + "</td>" +
            " <td><input type='hidden' class='txtrate' name='Products[" + $('#index').val() + "].Rate' value='" + $('#Rate').val() + "'/>" + $('#Rate').val() + "</td>" +
            " <td><button type='button' class='btn btn-air-warning btn-sm' style = 'border-radius : 30px''>DELETE</button></td></tr>");

        index = parseInt(index) + 1;
        $('#index').val(index);
        reindex();
    }
});
function reindex() {
    $('.tblProducts').each(function (index1, index2, index3) {
        index1 = 0;
        index2 = 0;
        index3 = 0;

        $(this).find(".txtname").each(function () {
            var prefixName = "Products[" + index1 + "].ProductId";
            this.name = this.name.replace(/Products\[\d+\].ProductId/, prefixName);
            index1++;
        });
        $(this).find(".txtquantity").each(function () {
            var prefixQuantity = "Products[" + index2 + "].Quantity";
            this.name = this.name.replace(/Products\[\d+\].Quantity/, prefixQuantity);
            index2++;
        });
        $(this).find(".txtrate").each(function () {
            var prefixRate = "Products[" + index3 + "].Rate";
            this.name = this.name.replace(/Products\[\d+\].Rate/, prefixRate);
            index3++;
        });
    });
}

!function($) {
    "use strict";

    var MainApp = function () {
        this.$body = $("body"),
            this.$wrapper = $("#wrapper"),
            this.$btnFullScreen = $("#btn-fullscreen"),
            this.$leftMenuButton = $('.button-menu-mobile'),
            this.$menuItem = $('.has_sub > a')
    };

    MainApp.prototype.intSlimscrollmenu = function () {
        $('.slimscroll-menu').slimscroll({
            height: 'auto',
            position: 'right',
            size: "7px",
            color: '#9ea5ab',
            wheelStep: 5,
            touchScrollStep: 50
        });
    },
    MainApp.prototype.initSlimscroll = function () {
        $('.slimscroll').slimscroll({
            height: 'auto',
            position: 'right',
            size: "7px",
            color: '#9ea5ab',
            touchScrollStep: 50
        });
    },

    MainApp.prototype.initMetisMenu = function () {
        //metis menu
        $("#side-menu").metisMenu();
    },

    MainApp.prototype.initLeftMenuCollapse = function () {
        // Left menu collapse
        $('.button-menu-mobile').on('click', function (event) {
            event.preventDefault();
            $("body").toggleClass("enlarged");
        });
    },

    MainApp.prototype.initEnlarge = function () {
        if ($(window).width() < 1025) {
            $('body').addClass('enlarged');
        } else {
            if ($('body').data('keep-enlarged') != true)
                $('body').removeClass('enlarged');
        }
    },

    MainApp.prototype.initActiveMenu = function () {
        // === following js will activate the menu in left side bar based on url ====
        $("#sidebar-menu a").each(function () {
            var pageUrl = window.location.href.split(/[?#]/)[0];
            if (this.href == pageUrl) {
                $(this).addClass("mm-active");
                $(this).parent().addClass("mm-active"); // add active to li of the current link
                $(this).parent().parent().addClass("mm-show");
                $(this).parent().parent().prev().addClass("mm-active"); // add active class to an anchor
                $(this).parent().parent().parent().addClass("mm-active");
                $(this).parent().parent().parent().parent().addClass("mm-show"); // add active to li of the current link
                $(this).parent().parent().parent().parent().parent().addClass("mm-active");
            }
        });
    },

    MainApp.prototype.initComponents = function () {
        $('[data-toggle="tooltip"]').tooltip();
        $('[data-toggle="popover"]').popover();
    },

    //full screen
    MainApp.prototype.initFullScreen = function () {
        var $this = this;
        $this.$btnFullScreen.on("click", function (e) {
            e.preventDefault();

            if (!document.fullscreenElement && /* alternative standard method */ !document.mozFullScreenElement && !document.webkitFullscreenElement) {  // current working methods
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                }
            } else {
                if (document.cancelFullScreen) {
                    document.cancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                }
            }
        });
    },



    MainApp.prototype.init = function () {
        this.intSlimscrollmenu();
        this.initSlimscroll();
        this.initMetisMenu();
        this.initLeftMenuCollapse();
        this.initEnlarge();
        this.initActiveMenu();
        this.initComponents();
        this.initFullScreen();
        Waves.init();
    },

    //init
    $.MainApp = new MainApp, $.MainApp.Constructor = MainApp
}(window.jQuery),

//initializing
function ($) {
    "use strict";
    $.MainApp.init();
    }(window.jQuery);

//$('#createBtn').click(function () {
//    location.href = 'Create';
//});
//$('#editBtn').click(function () {
//    var dataId = $(this).attr("data-id");
//    location.href = 'Edit/' + dataId;
//});


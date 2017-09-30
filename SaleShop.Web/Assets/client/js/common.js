var common = {
    init:function() {
        common.registerEvents();
    },
    registerEvents: function () {


        $("#txtKeyword").autocomplete({
                minLength: 0,
                source: function(request, response) {
                    $.ajax({
                        url: "/Product/GetListProductByName",
                        dataType: "json",
                        data: {
                            keyword: request.term
                        },
                        success: function (result) {
                            response(result.data);
                        }
                    });
                },
                focus: function (event, ui) {
                    $("#txtKeyword").val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $("#txtKeyword").val(ui.item.label);
                    return false;
                }
            }).autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "</div>")
                    .appendTo(ul);
         };

        $('#btnLogOut').on('click', function (e) {
            e.preventDefault(); // xóa # mặc định của thẻ a
            $('#frmLogout').submit();

        });
    }
}
common.init(); // khi khởi tạo tương ứng với document ready
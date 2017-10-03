var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id')); // parse data-id của nút      
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id')); // parse data-id của nút      
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            } else {

                $('#amount_' + productId).text(0);
            }
            //Update lại thành tiền
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        });
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (index, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    }
    ,
    loadData: function () {
        $.ajax({
            url: "/ShoppingCart/GetAll",
            type: "GET",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    $.each(res.data,
                        function (index, item) {
                            html += Mustache.render(template,
                                {
                                    ProductID: item.ProductID,
                                    ProductName: item.Product.ProductName,
                                    Image: item.Product.Image,
                                    Price: item.Product.Price,
                                    PriceF: numeral(item.Product.Price).format('0,0'),
                                    Quantity: item.Quantity,
                                    Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                                });
                        });
                    $('#cartBody').html(html);
                    cart.registerEvent();
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                }
            }
        });

    },
    addItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productID: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    alert('Thêm sản phẩm vào giỏ hàng thành công');
                }
            }
        });
    },
    deleteItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productID: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    }
}
cart.init();
var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('.btnAddToCart').off('click').on('click', function (e) {
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

            //Update lại session 
            cart.updateAll();
        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        }); 
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        }); 
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
            $('#frmPayment').valid();
        }); 
        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });
        $('#btnCreateOrder').off('click').on('click',function(e) {
            e.preventDefault();

            var isValid = $('#frmPayment').valid();

            if (isValid) {
                cart.createOrder();
            }
        });
        $('#frmPayment').validate({
            rules: {
                name: {
                    required: true
                },
                address: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: 'Yêu cầu nhập tên',
                address: 'Yêu cầu nhập địa chỉ',
                email: {
                    required: 'Yêu cầu nhập Email',
                    email:'Định dạng email chưa đúng'
                },
                phone: {
                    required: 'Yêu cầu nhập số điện thoại',
                    number: 'Số điện thoại phải là số'
                }
            }
        });
    },
    getLoginUser:function() {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var user = res.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    createOrder: function () {

        var order = {
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMessage: $('#txtMessage').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerName: $('#txtName').val(),
            PaymentMethod:'Thanh toán tiền mặt',
            Status:false
        };

        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            data: {
                orderViewModel:JSON.stringify(order)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    $('#divCheckout').hide();
                    cart.deleteAll();

                    setTimeout(function() {
                            $('#cartContent').html('Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất');
                        },
                        2000);
                } else {
                    alert(res.message);
                }
            }
        });
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (index, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
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

                    console.log(html);
                    if (html == '') {
                        $('#cartContent').html('Không có sản phẩm nào trong giỏ hàng');
                    }

                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        });

    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    updateAll: function () {

        var cartList = [];
        $.each($('.txtQuantity'),function(i,item) {
            cartList.push({
                ProductID: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
 
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                    console.log('Update ok');
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
                } else {
                    alert(res.message);
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
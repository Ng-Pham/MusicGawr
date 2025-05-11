$(function () { 
    

    $('.play-left-arrow').click(function (e) { 
        e.preventDefault();

        $('.player_left').toggleClass('open-list');

        const isOpen = $('.player_left').hasClass('open-list');

        $('.type_playlist').toggleClass('reordered', isOpen);
        
    });

    $('.queue .que_button').click(function (e) { 
        e.preventDefault();
        
        $('.playlist-wrap').addClass('queue-open');
    });

    $('.queue-top .queue-close i').click(function (e) { 
        e.preventDefault();
        
        $('.playlist-wrap').removeClass('queue-open');
    });

    $('.player_close').click(function (e) { 
        e.preventDefault();
        
        $('.player_wrapper').toggleClass('player_down');
    });

    $('.nav-close').click(function (e) { 
        e.preventDefault();
        
        $('.side-menu').toggleClass('open_nav');
    });

    $('.right-button .close-sidebar').click(function (e) { 
        e.preventDefault();
        
        $('.side-menu').toggleClass('open_nav');
    });
    $('.more_icon').click(function (e) {
        e.preventDefault();

        // Lấy thẻ <ul> tương ứng với nút được click
        var $currentOption = $(this).siblings('.more-option');

        // Đóng tất cả các danh sách đang mở, trừ danh sách tương ứng
        $('.more-option').not($currentOption).removeClass('xh-block-more');

        // Toggle danh sách tương ứng
        $currentOption.toggleClass('xh-block-more');
    });


    $('.album_btn .play_btn').click(function (e) { 
        e.preventDefault();
        
        $('.play-song').toggleClass('pause-song');
    });


    // $('.nav-wrapper ul li').click(function (e) { 
    //     e.preventDefault();
        
    //     $('.nav-wrapper li').removeClass('active');
    //     $(this).addClass('active');

    // });
    const toggleButton = document.getElementById('themeToggle');
    const currentTheme = localStorage.getItem('theme');

    if (currentTheme === 'dark') {
        $('body').addClass('dark-mode');
    }

    toggleButton.addEventListener('click', function () {
        $('body').toggleClass('dark-mode');
        const theme = $('body').hasClass('dark-mode') ? 'dark' : 'light';
        localStorage.setItem('theme', theme);
    });
});
$(document).ready(function () {
    // Xử lý click trên thẻ a thay vì submit form
    $('#loginBtn').click(function (e) {
        e.preventDefault();

        var form = $('#loginForm');
        var data = form.serialize(); // đã chứa __RequestVerificationToken

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    $('#loginError').text(response.message).show();
                }
            },
            error: function () {
                $('#loginError').text('Lỗi hệ thống.').show();
            }
        });
    });

    // Cho phép submit form khi nhấn Enter
    $('#loginForm').keypress(function (e) {
        if (e.which === 13) {
            $('#loginBtn').click();
            return false;
        }
    });

    // Xử lý click nút đăng ký
    $('#registerBtn').click(function (e) {
        e.preventDefault();

        var form = $('#MyFormRegister');
        var data = form.serialize(); // Bao gồm cả __RequestVerificationToken

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    var errorMessage = response.message || (response.errors ? response.errors.join('<br>') : 'Đã xảy ra lỗi.');
                    $('#registerError').html(errorMessage).show();
                }
            },
            error: function () {
                $('#registerError').text('Lỗi hệ thống.').show();
            }
        });
    });

    // Cho phép submit form khi nhấn Enter
    $('#MyFormRegister').keypress(function (e) {
        if (e.which === 13) {
            $('#registerBtn').click();
            return false;
        }
    });

    // Xử lý click nút quên mật khẩu
    $('#forgotBtn').click(function (e) {
        e.preventDefault();

        var form = $('#FormForgotPassword');
        $('#forgotPasswordError').hide();
        $('#fullScreenLoading').addClass('active');
        var data = form.serialize(); // Bao gồm cả __RequestVerificationToken

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: data,
            success: function (response) {
                if (response.success) {
                    $('#fullScreenLoading').removeClass('active')
                    // Hiển thị thông báo thành công
                    $('#overlayText').text(response.message);
                    $('#overlayMessage').fadeIn();

                } else {
                    var errorMessage = response.message || (response.errors ? response.errors.join('<br>') : 'Đã xảy ra lỗi.');
                    $('#forgotPasswordError').html(errorMessage).show();
                }
            },
            error: function () {
                $('#fullScreenLoading').removeClass('active')
                $('#forgotPasswordError').text('Lỗi hệ thống.').show();
            }
        });
    });

    // Cho phép submit form khi nhấn Enter
    $('#FormForgotPassword').keypress(function (e) {
        if (e.which === 13) {
            $('#forgotBtn').click();
            return false;
        }
    });

    // Xử lý click nút xác reset mật khẩu
    $(document).ready(function () {
        var $form = $('#myFormResetPassword');
        var $resetBtn = $('.reset-password-btn');
        var $errorBox = $('#resetPasswordError');
        // Xử lý click nút Reset Password
        $resetBtn.click(function (e) {
            e.preventDefault();

            var data = $form.serialize();
            $('#fullScreenLoading').addClass('active');

            $.ajax({
                url: $form.attr('action'),
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response.success) {  
                        $('#fullScreenLoading').removeClass('active');
                        // Hiển thị thông báo thành công
                        $('#overlayText').text(response.message);
                        $('#overlayMessage').fadeIn();
                        setTimeout(function () {
                            window.location.href = 'https://localhost:44381/';
                        }, 2000);
                    } else {
                        var errorMessage = response.message || (response.errors ? response.errors.join('<br>') : 'Đã xảy ra lỗi.');
                        $errorBox.html(errorMessage).css('color', 'red').show();
                    }
                },
                error: function () {
                    $('#fullScreenLoading').removeClass('active');
                    $errorBox.text('Lỗi hệ thống.').css('color', 'red').show();
                }
            });
        });

        // Cho phép submit khi nhấn Enter
        $form.keypress(function (e) {
            if (e.which === 13) {
                $resetBtn.click();
                return false;
            }
        });
    });

});
 
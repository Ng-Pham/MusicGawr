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
 
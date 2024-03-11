$(document).ready(function () {
    $("a").on('click', function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800
            , function () {
                window.location.hash = "";
            }
            );
        }
    });
    $(document).on('click', '.navbar-toggler', function (e) {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $('body').addClass("overflow-hidden")
            $('.menu-overlay').addClass('active');
            $this.children('.animated-icon').addClass('open');
            $this.addClass('active');
            $('.navbar-collapse').removeClass('collapse').addClass('active');
        } else {
            $('body').removeClass("overflow-hidden")
            $this.children('.animated-icon').removeClass('open');
            $this.removeClass('active');
            $this.siblings('.navbar-collapse').removeClass("active").removeClass("collapse");
            $('.menu-overlay').removeClass("active");
        }
    })

    $('.menu-overlay').click(function () {
        var $this = $(this);
        $('body').removeClass("overflow-hidden")
        $('.navbar-toggler').children('.animated-icon').removeClass('open');
        $('.navbar-toggler').removeClass('active');
        $('.navbar-toggler').siblings('.navbar-collapse').removeClass("active").removeClass("collapse");
        $this.removeClass("active");
    })

});

AOS.init({
    once: true
})


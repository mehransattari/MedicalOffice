/*
Author       : Dreamguys
Template Name: Doccure - Vetinary Bootstrap Template
Version      : 1.0
*/



// Stick Sidebar

if ($(window).width() > 767) {
    if ($('.theiaStickySidebar').length > 0) {
        $('.theiaStickySidebar').theiaStickySidebar({
            // Settings
            additionalMarginTop: 30
        });
    }
}

// Sidebar

if ($(window).width() <= 991) {
    var Sidemenu = function () {
        this.$menuItem = $('.main-nav a');
    };

    function init() {
        var $this = Sidemenu;
        $('.main-nav a').on('click', function (e) {
            if ($(this).parent().hasClass('has-submenu')) {
                e.preventDefault();
            }
            if (!$(this).hasClass('submenu')) {
                $('ul', $(this).parents('ul:first')).slideUp(350);
                $('a', $(this).parents('ul:first')).removeClass('submenu');
                $(this).next('ul').slideDown(350);
                $(this).addClass('submenu');
            } else if ($(this).hasClass('submenu')) {
                $(this).removeClass('submenu');
                $(this).next('ul').slideUp(350);
            }
        });
    }

    // Sidebar Initiate
    init();
}

// Textarea Text Count

var maxLength = 100;
$('#review_desc').on('keyup change', function () {
    var length = $(this).val().length;
    length = maxLength - length;
    $('#chars').text(length);
});

// Select 2

if ($('.select').length > 0) {
    $('.select').select2({
        minimumResultsForSearch: -1,
        width: '100%'
    });
}

// Date Time Picker

if ($('.datetimepicker').length > 0) {
    $('.datetimepicker').datetimepicker({
        format: 'DD/MM/YYYY',
        icons: {
            up: "fas fa-chevron-up",
            down: "fas fa-chevron-down",
            next: 'fas fa-chevron-right',
            previous: 'fas fa-chevron-left'
        }
    });
}

// Floating Label

if ($('.floating').length > 0) {
    $('.floating').on('focus blur', function (e) {
        $(this).parents('.form-focus').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
    }).trigger('blur');
}

// Mobile menu sidebar overlay

$('body').append('<div class="sidebar-overlay"></div>');
$(document).on('click', '#mobile_btn', function () {
    $('main-wrapper').toggleClass('slide-nav');
    $('.sidebar-overlay').toggleClass('opened');
    $('html').addClass('menu-opened');
    return false;
});

$(document).on('click', '.sidebar-overlay', function () {
    $('html').removeClass('menu-opened');
    $(this).removeClass('opened');
    $('main-wrapper').removeClass('slide-nav');
});

$(document).on('click', '#menu_close', function () {
    $('html').removeClass('menu-opened');
    $('.sidebar-overlay').removeClass('opened');
    $('main-wrapper').removeClass('slide-nav');
});

// Tooltip

if ($('[data-bs-toggle="tooltip"]').length > 0) {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
}

// Add More Hours

$(".hours-info").on('click', '.trash', function () {
    $(this).closest('.hours-cont').remove();
    return false;
});

$(".add-hours").on('click', function () {

    var hourscontent = '<div class="row form-row hours-cont">' +
        '<div class="col-12 col-md-10">' +
        '<div class="row form-row">' +
        '<div class="col-12 col-md-6">' +
        '<div class="form-group">' +
        '<label>زمان شروع</label>' +
        '<select class="form-select form-control">' +
        '<option>-</option>' +
        '<option>12.00 صبح</option>' +
        '<option>12.30 صبح</option>' +
        '<option>1.00 صبح</option>' +
        '<option>1.30 صبح</option>' +
        '</select>' +
        '</div>' +
        '</div>' +
        '<div class="col-12 col-md-6">' +
        '<div class="form-group">' +
        '<label>زمان پایان</label>' +
        '<select class="form-select form-control">' +
        '<option>-</option>' +
        '<option>12.00 صبح</option>' +
        '<option>12.30 صبح</option>' +
        '<option>1.00 صبح</option>' +
        '<option>1.30 صبح</option>' +
        '</select>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="col-12 col-md-2"><label class="d-md-block d-sm-none d-none">&nbsp;</label><a href="#" class="btn btn-danger trash"><i class="far fa-trash-alt"></i></a></div>' +
        '</div>';

    $(".hours-info").append(hourscontent);
    return false;
});

// Content div min height set

function resizeInnerDiv() {
    var height = $(window).height();
    var header_height = $(".header").height();
    var footer_height = $(".footer").height();
    var setheight = height - header_height;
    var trueheight = setheight - footer_height;
    $(".content").css("min-height", trueheight);
}

if ($('.content').length > 0) {
    resizeInnerDiv();
}

$(window).resize(function () {
    if ($('.content').length > 0) {
        resizeInnerDiv();
    }
});

// Slick Slider





function our_service_slider(count) {
    if (count > 0) {
        $('.our-service-slider').slick({
            dots: false,
            autoplay: false,
            infinite: true,
            rtl: true,
            variableWidth: true,
            responsive: {
                0: {
                    items: 1,
                    nav: true
                },
                600: {
                    items: 2,
                    nav: true
                },
                1000: {
                    items: 3,
                    nav: true,
                    loop: false
                }
            }
        });
    }
}
function testimonial_slider() {
    if ($('.testimonial-slider').length > 0) {
        $('.testimonial-slider').slick({
            dots: true,
            autoplay: false,
            infinite: true,
            rtl: true,
            prevArrow: false,
            nextArrow: false,
            slidesToShow: 3,
            slidesToScroll: 1,
            responsive: [{
                breakpoint: 992,
                settings: {
                    slidesToShow: 3
                }
            },
            {
                breakpoint: 776,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 567,
                settings: {
                    slidesToShow: 1
                }
            }]
        });
    }
}
function gallery_Slider(count) {
    if (count > 0) {
        $('.gallery-slider').slick({
            dots: false,
            autoplay: false,
            infinite: true,
            rtl: true,
            variableWidth: true,
            responsive: {
                0: {
                    items: 1,
                    nav: true
                },
                600: {
                    items: 2,
                    nav: true
                },
                1000: {
                    items: 3,
                    nav: true,
                    loop: false
                }
            }
        });
    }
}


// Date Range Picker
if ($('.bookingrange').length > 0) {
    var start = moment().subtract(6, 'days');
    var end = moment();

    function booking_range(start, end) {
        var start = moment(start); // pass your date obj here.


        var end = moment(end); // pass your date obj here.


        $('.bookingrange span').html(start.format('jYYYY/jM/jD') + ' - ' + end.format('jYYYY/jM/jD'));
    }

    $('.bookingrange').daterangepicker({
        months: ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'],
        startDate: start,
        endDate: end,
        ranges: {
            'امروز': [moment(), moment()],
            'دیروز': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'هفته گذشته': [moment().subtract(6, 'days'), moment()],
            'ماه گذشته': [moment().subtract(29, 'days'), moment()],
            'این ماه': [moment().startOf('month'), moment().endOf('month')],
            'ماه بعد': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        "locale": {
            "format": "YYYY/M/D",
            "separator": " - ",
            "applyLabel": "اعمال",
            "cancelLabel": "انصراف",
            "fromLabel": "از",
            "toLabel": "تا",
            "customRangeLabel": "سفارشی",
            "weekLabel": "هف",
            "daysOfWeek": [
                "ی",
                "د",
                "س",
                "چ",
                "پ",
                "ج",
                "ش"
            ],
            "monthNames": [
                "ژانویه",
                "فوریه",
                "مارس",
                "آوریل",
                "می",
                "ژوئن",
                "جولای",
                "آگوست",
                "سپتامبر",
                "اکتبر",
                "نوامبر",
                "دسامبر"
            ],
            "firstDay": 6
        }
    }, booking_range);

    booking_range(start, end);
}

// Chat

var chatAppTarget = $('.chat-window');
(function () {
    if ($(window).width() > 991)
        chatAppTarget.removeClass('chat-slide');

    $(document).on("click", ".chat-window .chat-users-list a.media", function () {
        if ($(window).width() <= 991) {
            chatAppTarget.addClass('chat-slide');
        }
        return false;
    });
    $(document).on("click", "#back_user_list", function () {
        if ($(window).width() <= 991) {
            chatAppTarget.removeClass('chat-slide');
        }
        return false;
    });
})();

// Circle Progress Bar

function animateElements() {
    $('.circle-bar1').each(function () {
        var elementPos = $(this).offset().top;
        var topOfWindow = $(window).scrollTop();
        var percent = $(this).find('.circle-graph1').attr('data-percent');
        var animate = $(this).data('animate');
        if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
            $(this).data('animate', true);
            $(this).find('.circle-graph1').circleProgress({
                value: percent / 100,
                size: 400,
                thickness: 30,
                fill: {
                    color: '#da3f81'
                }
            });
        }
    });
    $('.circle-bar2').each(function () {
        var elementPos = $(this).offset().top;
        var topOfWindow = $(window).scrollTop();
        var percent = $(this).find('.circle-graph2').attr('data-percent');
        var animate = $(this).data('animate');
        if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
            $(this).data('animate', true);
            $(this).find('.circle-graph2').circleProgress({
                value: percent / 100,
                size: 400,
                thickness: 30,
                fill: {
                    color: '#68dda9'
                }
            });
        }
    });
    $('.circle-bar3').each(function () {
        var elementPos = $(this).offset().top;
        var topOfWindow = $(window).scrollTop();
        var percent = $(this).find('.circle-graph3').attr('data-percent');
        var animate = $(this).data('animate');
        if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
            $(this).data('animate', true);
            $(this).find('.circle-graph3').circleProgress({
                value: percent / 100,
                size: 400,
                thickness: 30,
                fill: {
                    color: '#1b5a90'
                }
            });
        }
    });
}

if ($('.circle-bar').length > 0) {
    animateElements();
}
$(window).scroll(animateElements);

// Preloader

$(window).on('load', function () {
    if ($('#loader').length > 0) {
        $('#loader').delay(350).fadeOut('slow');
        $('body').delay(350).css({ 'overflow': 'visible' });
    }
})

// Inspect keyCode

$(window).on("load", function () {
    document.onkeydown = function (e) {
        if (e.keyCode == 123) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
            return false;
        }

        if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
            return false;
        }
    };

});

	//document.oncontextmenu = function() {return false;};
	//	$(document).mousedown(function(e){
	//	if( e.button == 2 ) {
	//		return false;
	//	}
	//	return true;
	//});

